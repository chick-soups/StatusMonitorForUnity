package com.puremilk.status;

import android.bluetooth.BluetoothAdapter;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import com.unity3d.player.UnityPlayer;

public class BluetoothStatusHelper {
    private  final String TAG="BluetoothStatusHelper";
    private final int MESSAGE_ID = 1;
    private Handler m_Handler;
    private BroadcastReceiver m_BroadcastReceiver;


    public  BluetoothStatusHelper(ICallback_Int callback)
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

        m_BroadcastReceiver=new BroadcastReceiver(){
            @Override
            public void onReceive(Context context, Intent intent){
                int state =  intent.getIntExtra(BluetoothAdapter.EXTRA_STATE,BluetoothAdapter.STATE_OFF);
                SendMessage(state);
            }
        };
    }

    public static int CurStatus()
    {
       return   BluetoothAdapter.getDefaultAdapter().getState();
    }
    public  void Register()
    {
        IntentFilter intentFilter=new IntentFilter(BluetoothAdapter.ACTION_STATE_CHANGED);
        UnityPlayer.currentActivity.registerReceiver(m_BroadcastReceiver,intentFilter);
    }

    public  void UnRegister(){
        UnityPlayer.currentActivity.unregisterReceiver(m_BroadcastReceiver);
    }

    private void SendMessage(int what) {
        Message message = new Message();
        message.what = MESSAGE_ID;
        message.obj = what;
        m_Handler.sendMessage(message);
    }
}
