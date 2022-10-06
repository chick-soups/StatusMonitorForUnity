using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Puremilk.Status;
public class Sample : MonoBehaviour
{
    public TextMeshProUGUI m_Text;
    public TextMeshProUGUI m_TextLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        Puremilk.Status.BatteryStatus batteryState=new  Puremilk.Status.BatteryStatus();
        batteryState.BatteryStatusChanged.AddListener(ShowBatteryStatus);
        batteryState.BatteryLevelChanged.AddListener(ShowBatteryLevel);
        batteryState.Register();
        ShowBatteryStatus(Puremilk.Status.BatteryStatus.CurState);
    }
    private void ShowBatteryStatus(UnityEngine.BatteryStatus status){
        m_Text.text=status.ToString();
    }

    private void ShowBatteryLevel(float level){
        m_TextLevel.text=level.ToString();
    }

}
