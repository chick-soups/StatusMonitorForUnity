package com.puremilk.status;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.location.LocationManager;
import android.os.Build;
import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.util.Log;

import com.unity3d.player.UnityPlayer;



public class LocationStatusHelper
{
    private  final String TAG="LocationStatusHelper";
    private final int MESSAGE_ID = 1;
    private Handler m_Handler;
    private int curStatus = -1;
    private BroadcastReceiver m_BroadcastReceiver;
    private  Context m_Context;


    public int CurStatus(){
       LocationManager lm = (LocationManager) UnityPlayer.currentActivity.getSystemService(Context.LOCATION_SERVICE);
        //LocationManager lm=(LocationManager)m_Context.getSystemService(Context.LOCATION_SERVICE);
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.P) {
            if(lm.isLocationEnabled()){
                return  1;
            }else{
                return  0;
            }
        }else{
          boolean gpsEnabled =  lm.isProviderEnabled(LocationManager.GPS_PROVIDER);
          boolean networkEnabled =lm.isProviderEnabled(LocationManager.NETWORK_PROVIDER);
          if(gpsEnabled||networkEnabled){
              return  1;
          }else{
              return 0;
          }
        }
    }




    public  LocationStatusHelper(ICallback_Int callback)
    {
        m_Handler = new Handler(Looper.getMainLooper()) {
            @Override
            public void handleMessage(Message msg) {
                super.handleMessage(msg);
                if (msg.what == MESSAGE_ID) {
                    callback.Callback((int) msg.obj);
                }
            }
        };

        m_BroadcastReceiver=new BroadcastReceiver() {
            @Override
            public void onReceive(Context context, Intent intent) {
                int status=0;
                if(Build.VERSION.SDK_INT>=Build.VERSION_CODES.R)
                {
                    boolean isEnabled = intent.getBooleanExtra(LocationManager.EXTRA_LOCATION_ENABLED,false);
                    status=isEnabled?1:0;

                }else
                {
                    status=CurStatus();

                }
                SendMessage(status);
            }
        };
    }
    public  void Register(){
        IntentFilter intentFilter=new IntentFilter();
        intentFilter.addAction(LocationManager.MODE_CHANGED_ACTION);
        UnityPlayer.currentActivity.registerReceiver(m_BroadcastReceiver,intentFilter);

    }

    public  void UnRegister(){

        UnityPlayer.currentActivity.unregisterReceiver(m_BroadcastReceiver);
    }

    private void SendMessage(int messageObj) {
        Message message = new Message();
        message.what = MESSAGE_ID;
        message.obj = messageObj;
        m_Handler.sendMessage(message);
    }
}
