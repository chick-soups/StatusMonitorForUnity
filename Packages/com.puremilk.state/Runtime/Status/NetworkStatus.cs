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

using UnityEngine;
using UnityEngine.Events;
using System;


namespace Puremilk.Status
{
    [StatusAttribute()]
    public class NetworkStatus : IDisposable
    {
        public static NetworkReachability CurStatus
        {
            get
            {
                return Application.internetReachability;
            }
        }

        public UnityEvent<NetworkReachability> Callback
        {
            get
            {
                return m_StatusChanged;
            }
        }
        private UnityEvent<NetworkReachability> m_StatusChanged = new UnityEvent<NetworkReachability>();
#if UNITY_ANDROID
        private AndroidJavaObject m_Helper;

        public NetworkStatus()
        {
#if UNITY_EDITOR
            string label = typeof(NetworkStatus).ToString();
            UnityEditor.EditorPrefs.SetBool(label, true);
#endif
            NetworkStatusCallback callback = new NetworkStatusCallback(m_StatusChanged);
            m_Helper = new AndroidJavaObject("com.puremilk.status.NetworkReachablityHelper", callback);
        }

        ///If android,should add permissions android.permission.ACCESS_NETWORK_STATE android.permission.CHANGE_NETWORK_STATE android.permission.INTERNET
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
    }
#if UNITY_ANDROID
    public class NetworkStatusCallback : AndroidJavaProxy, ICallback_Int
    {
        private UnityEvent<NetworkReachability> m_Callback;
        public NetworkStatusCallback(UnityEvent<NetworkReachability> callback) : base("com.puremilk.status.ICallback_Int")
        {
            m_Callback = callback;
        }

        public void Callback(int status)
        {
            m_Callback.Invoke((NetworkReachability)status);
        }
    }
#endif
}
