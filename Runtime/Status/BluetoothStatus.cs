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
                #if UNITY_ANDROID
                AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.puremilk.status.BluetoothStatusHelper");
                int statusIndex = androidJavaClass.CallStatic<int>("CurStatus");
                return ParseInt(statusIndex);
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
#if UNITY_ANDROID
        private AndroidJavaObject m_Helper;

        public BluetoothStatus()
        {
#if UNITY_EDITOR
            string label = typeof(BatteryStatus).ToString();
            UnityEditor.EditorPrefs.SetBool(label, true);
#endif
            BluetoothStatusCallback callback = new BluetoothStatusCallback(m_StatusChanged);
            m_Helper = new AndroidJavaObject("com.puremilk.status.BluetoothStatusHelper", callback);
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
        internal static Status ParseInt(int status){
            switch(status){
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
#endif
    }
#if UNITY_ANDROID
    public class BluetoothStatusCallback : AndroidJavaProxy, ICallback_Int
    {

        private const int STATE_OFF = 10;
        private const int STATE_TURNING_ON = 11;
        private const int STATE_ON = 12;
        private const int STATE_TURNING_OFF =13;

        private UnityEvent<BluetoothStatus.Status> m_Callback;
        public BluetoothStatusCallback(UnityEvent<BluetoothStatus.Status> callback) : base("com.puremilk.status.ICallback_Int")
        {
            m_Callback = callback;
        }

        public void Callback(int status)
        {
             BluetoothStatus.Status curStatus =  BluetoothStatus.ParseInt(status);
             m_Callback.Invoke(curStatus);
        }
    }
#endif
}
