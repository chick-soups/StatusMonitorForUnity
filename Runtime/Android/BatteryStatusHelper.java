package com.puremilk.status;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.BatteryManager;
import com.unity3d.player.UnityPlayer;

public class BatteryStatusHelper {
    private BroadcastReceiver m_BroadcastRecevier;
    public BatteryStatusHelper(ICallback_Int_Float callback){
       m_BroadcastRecevier=new BroadcastReceiver() {
           @Override
           public void onReceive(Context context, Intent intent) {
             int status = intent.getIntExtra(BatteryManager.EXTRA_STATUS,-1);
             int level =  intent.getIntExtra(BatteryManager.EXTRA_LEVEL,-1);
             int scale = intent.getIntExtra(BatteryManager.EXTRA_SCALE,1);
             float  level01=level/(float)scale;
             callback.Callback(status,level01);
           }
       };
    }

    public  void Register()
    {
        IntentFilter intentFilter=new IntentFilter(Intent.ACTION_BATTERY_CHANGED);
        UnityPlayer.currentActivity.registerReceiver(m_BroadcastRecevier,intentFilter);
    }
    public  void UnRegister(){
        UnityPlayer.currentActivity.unregisterReceiver(m_BroadcastRecevier);
    }
}
