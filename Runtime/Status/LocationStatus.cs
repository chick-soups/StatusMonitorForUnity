using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace Puremilk.Status
{
     public class LocationStatus : IDisposable
    {
        public static bool CurState
        {
            get
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.puremilk.status.LocationStatusHelper");
                int statusIndex = androidJavaClass.CallStatic<int>("CurStatus");
                return ParseIntStatus(statusIndex);
#else
                return true;
#endif
            }
        }

        public UnityEvent<bool> StatusChanged
        {
            get
            {
                return m_StatusChanged;
            }
        }

        private UnityEvent<bool> m_StatusChanged = new UnityEvent<bool>();
#if UNITY_ANDROID && !UNITY_EDITOR
        private AndroidJavaObject m_Helper;
#endif

        public LocationStatus()
        {
#if UNITY_EDITOR
            string label = typeof(LocationStatus).ToString();
            UnityEditor.EditorPrefs.SetBool(label, true);
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
            UnityEvent<int> intCallback=new UnityEvent<int>();
            intCallback.AddListener((status)=>{
                bool curStatus =   ParseIntStatus(status);
                m_StatusChanged.Invoke(curStatus);
            });
            ProxyCallback_Int proxyCallback=new ProxyCallback_Int(intCallback);
            m_Helper = new AndroidJavaObject("com.puremilk.status.LocationStatusHelper", proxyCallback);
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
        internal static bool ParseIntStatus(int status)
        {
            switch (status)
            {
                case 1:
                    return true;
                case 0:
                    return false;
                default:
                    throw new System.NotSupportedException();
            }
        }
    }
}
