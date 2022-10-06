using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Puremilk.Status;

public class NetworkReachabilitySample : MonoBehaviour
{
    public TextMeshProUGUI m_Text;

    // Start is called before the first frame update
    void Start()
    {
        NetworkStatus status=new NetworkStatus();
        status.Callback.AddListener(ShowStatus);
        status.Register();
    }

    private void ShowStatus(NetworkReachability intValue){
        m_Text.text=intValue.ToString();
    }
}
