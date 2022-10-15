using UnityEngine;
using UnityEngine.Events;
namespace Puremilk.Status
{
    public class ProxyCallback_Int : AndroidJavaProxy, ICallback_Int
    {
        private UnityEvent<int> m_Callback;
        public ProxyCallback_Int(UnityEvent<int> callback) : base("com.puremilk.status.ICallback_Int")
        {
            m_Callback = callback;
        }

        public void Callback(int status)
        {
            m_Callback.Invoke(status);
        }
    }

}
