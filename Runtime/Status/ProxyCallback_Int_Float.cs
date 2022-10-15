 
 using UnityEngine;
 using UnityEngine.Events;
 namespace Puremilk.Status
 {
     public class ProxyCallback_Int_Float : AndroidJavaProxy, ICallback_Int_Float
    {

        private UnityEvent<int> m_IntCallback;
        private UnityEvent<float> m_FloatCallback;
        public ProxyCallback_Int_Float(UnityEvent<int> intCallback, UnityEvent<float> floatCallback) : base("com.puremilk.status.ICallback_Int_Float")
        {
           m_IntCallback=intCallback;
           m_FloatCallback=floatCallback;
        }

        public void Callback(int x, float y)
        {
            if(x>=0){
                m_IntCallback.Invoke(x);
            }
            if(y>=0f){
                m_FloatCallback.Invoke(y);
            }
        }
    }
 }
