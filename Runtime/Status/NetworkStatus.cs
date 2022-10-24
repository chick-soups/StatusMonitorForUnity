using UnityEngine;
using UnityEngine.Events;
using System;

namespace Puremilk.Status
{
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
#if UNITY_ANDROID && !UNITY_EDITOR
        private AndroidJavaObject m_Helper;
#endif

        public NetworkStatus()
        {
#if UNITY_EDITOR
            string label = typeof(NetworkStatus).ToString();
            UnityEditor.EditorPrefs.SetBool(label, true);
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
            UnityEvent<int> statusCallback = new UnityEvent<int>();
            statusCallback.AddListener((statusIndex) =>
            {
                m_StatusChanged.Invoke((NetworkReachability)statusIndex);
            });
            ProxyCallback_Int proxyCallback = new ProxyCallback_Int(statusCallback);
            m_Helper = new AndroidJavaObject("com.puremilk.status.NetworkReachablityHelper", proxyCallback);
#endif
        }

        ///If android,should add permissions android.permission.ACCESS_NETWORK_STATE android.permission.CHANGE_NETWORK_STATE android.permission.INTERNET
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



    }
}
