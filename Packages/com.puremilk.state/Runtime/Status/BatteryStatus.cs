// Copyright 2022 Xuhua Chow

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

//     http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


namespace Puremilk.Status
{
    [StatusAttribute()]
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
#if UNITY_ANDROID
        private AndroidJavaObject m_Helper;

        public BatteryStatus()
        {
#if UNITY_EDITOR
            string label = typeof(BatteryStatus).ToString();
            UnityEditor.EditorPrefs.SetBool(label, true);
#endif
            BatteryStateCallback callback = new BatteryStateCallback(m_BatteryStatusChanged, m_BatteryLevelChanged);
            m_Helper = new AndroidJavaObject("com.puremilk.status.BatteryStatusHelper", callback);
        }

        public void Register()
        {
            m_Helper.Call("Register");
        }

        public void UnRegister()
        {
            m_Helper.Call("UnRegister");
        }

        public void Dispose()
        {
            m_Helper.Dispose();
        }
#endif

#if UNITY_EDITOR
        private static void HandleBuildProcessAndroid(System.Xml.XmlDocument xmlDocument){
            Debug.Log(xmlDocument.Name);
            Debug.Log(typeof(BatteryStatus).ToString());
        }
#endif
    }
#if UNITY_ANDROID
    public class BatteryStateCallback : AndroidJavaProxy, ICallback_Int_Float
    {

        private const int BATTERY_STATUS_UNKNOWN = 1;
        private const int BATTERY_STATUS_CHARGING = 2;
        private const int BATTERY_STATUS_DISCHARGING = 3;
        private const int BATTERY_STATUS_NOT_CHARGING = 4;
        private const int BATTERY_STATUS_FULL = 5;

        private UnityEvent<UnityEngine.BatteryStatus> m_Callback;
        private UnityEvent<float> m_batteryLevelCallback;
        public BatteryStateCallback(UnityEvent<UnityEngine.BatteryStatus> callback, UnityEvent<float> batteryLevelCallback) : base("com.puremilk.status.ICallback_Int_Float")
        {
            m_Callback = callback;
            m_batteryLevelCallback = batteryLevelCallback;
        }

        public void Callback(int status, float batteryLevel)
        {
            switch (status)
            {
                case BATTERY_STATUS_DISCHARGING:
                    m_Callback?.Invoke(UnityEngine.BatteryStatus.Discharging);
                    break;
                case BATTERY_STATUS_CHARGING:
                    m_Callback?.Invoke(UnityEngine.BatteryStatus.Charging);
                    break;
                case BATTERY_STATUS_NOT_CHARGING:
                    m_Callback?.Invoke(UnityEngine.BatteryStatus.NotCharging);
                    break;
                case BATTERY_STATUS_FULL:
                    m_Callback?.Invoke(UnityEngine.BatteryStatus.Full);
                    break;
                default:
                    m_Callback.Invoke(UnityEngine.BatteryStatus.Unknown);
                    break;
            }
            if (batteryLevel >= 0)
            {
                m_batteryLevelCallback.Invoke(batteryLevel);
            }


        }
    }
#endif
}
