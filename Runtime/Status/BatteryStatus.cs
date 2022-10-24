using UnityEngine;
using UnityEngine.Events;
using System;

namespace Puremilk.Status
{
    public class BatteryStatus : IDisposable
    {
        public static float BatteryLevel
        {
            get
            {
                return SystemInfo.batteryLevel;
            }
        }
        public static UnityEngine.BatteryStatus CurState
        {
            get
            {
                return SystemInfo.batteryStatus;
            }
        }

        public UnityEvent<UnityEngine.BatteryStatus> BatteryStatusChanged
        {
            get
            {
                return m_BatteryStatusChanged;
            }
        }

        public UnityEvent<float> BatteryLevelChanged
        {
            get
            {
                return m_BatteryLevelChanged;
            }
        }

        private UnityEvent<UnityEngine.BatteryStatus> m_BatteryStatusChanged = new UnityEvent<UnityEngine.BatteryStatus>();
        private UnityEvent<float> m_BatteryLevelChanged = new UnityEvent<float>();
#if UNITY_ANDROID&&!UNITY_EDITOR
        private AndroidJavaObject m_Helper;
#endif

        public BatteryStatus()
        {
#if UNITY_EDITOR
            string label = typeof(BatteryStatus).ToString();
            UnityEditor.EditorPrefs.SetBool(label, true);
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
            UnityEvent<int> unityEvent=new UnityEvent<int>();
            unityEvent.AddListener((Value)=>{
               UnityEngine.BatteryStatus batteryStatus=ParseIndex(Value);
               m_BatteryStatusChanged.Invoke(batteryStatus);
            });
            ProxyCallback_Int_Float callback = new ProxyCallback_Int_Float(unityEvent, m_BatteryLevelChanged);
            m_Helper = new AndroidJavaObject("com.puremilk.status.BatteryStatusHelper", callback);
#endif
        }

        public void Register()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            m_Helper.Call("Register");
#endif
        }

        public void UnRegister()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            m_Helper.Call("UnRegister");
#endif
        }

        public void Dispose()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            m_Helper.Dispose();
#endif
        }

        private UnityEngine.BatteryStatus ParseIndex(int index)
        {
            switch (index)
            {
                case 1:
                    return UnityEngine.BatteryStatus.Unknown;
                case 2:
                    return UnityEngine.BatteryStatus.Charging;
                case 3:
                    return UnityEngine.BatteryStatus.Discharging;
                case 4:
                    return UnityEngine.BatteryStatus.NotCharging;
                case 5:
                    return UnityEngine.BatteryStatus.Full;
                default:
                    throw new System.NotImplementedException();

            }
        }

    }
}
