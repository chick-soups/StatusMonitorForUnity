using UnityEngine.Events;
using System;

namespace Puremilk.Status
{
    public class BluetoothStatus : IDisposable
    {
        public enum Status
        {
            OFF,
            TURNING_ON,
            ON,
            TURNING_OFF
        }
        public static Status CurState
        {
            get
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.puremilk.status.BluetoothStatusHelper");
                int statusIndex = androidJavaClass.CallStatic<int>("CurStatus");
                return ParseIntStatus(statusIndex);
#else
                return Status.ON;
#endif
            }
        }

        public UnityEvent<Status> StatusChanged
        {
            get
            {
                return m_StatusChanged;
            }
        }

        private UnityEvent<Status> m_StatusChanged = new UnityEvent<Status>();
#if UNITY_ANDROID && !UNITY_EDITOR
        private AndroidJavaObject m_Helper;
#endif

        public BluetoothStatus()
        {
#if UNITY_EDITOR
            string label = typeof(BluetoothStatus).ToString();
            UnityEditor.EditorPrefs.SetBool(label, true);
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
            UnityEvent<int> intCallback=new UnityEvent<int>();
            intCallback.AddListener((status)=>{
                Status curStatus =   ParseIntStatus(status);
                m_StatusChanged.Invoke(curStatus);
            });
            ProxyCallback_Int proxyCallback=new ProxyCallback_Int(intCallback);
            m_Helper = new AndroidJavaObject("com.puremilk.status.BluetoothStatusHelper", proxyCallback);
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
        internal static Status ParseIntStatus(int status)
        {
            switch (status)
            {
                case 10:
                    return Status.OFF;
                case 11:
                    return Status.TURNING_ON;
                case 12:
                    return Status.ON;
                case 13:
                    return Status.TURNING_OFF;
                default:
                    throw new System.NotSupportedException();
            }
        }
    }
}
