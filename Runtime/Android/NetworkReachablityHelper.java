package com.puremilk.status;

import android.content.Context;
import android.net.ConnectivityManager;
import android.net.Network;
import android.net.NetworkCapabilities;
import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.util.Log;

import com.unity3d.player.UnityPlayer;


public class NetworkReachablityHelper
{
    private  final String TAG="NetworkReachablityHelper";
    private final int NETWORK_STATUS_MESSAGE_ID = 1;
    private ConnectivityManager.NetworkCallback networkCallback;
    private Handler m_Handler;
    private int curStatus = -1;
    public  NetworkReachablityHelper(ICallback_Int callback)
    {
        m_Handler = new Handler(Looper.getMainLooper()) {
            @Override
            public void handleMessage(Message msg) {
                super.handleMessage(msg);
                if (msg.what == NETWORK_STATUS_MESSAGE_ID) {
                   callback.Callback((int) msg.obj);
                }
            }
        };
        networkCallback = new ConnectivityManager.NetworkCallback() {
            @Override
            public void onLost(Network network) {
                super.onLost(network);
                if (curStatus != 0) {
                    curStatus = 0;
                    SendMessage(0);
                    Log.d(TAG, "onLost: " + 0);
                }

            }

            @Override
            public void onCapabilitiesChanged(Network network, NetworkCapabilities networkCapabilities) {
                super.onCapabilitiesChanged(network, networkCapabilities);
                if (networkCapabilities.hasCapability(NetworkCapabilities.NET_CAPABILITY_VALIDATED)) {
                    if (networkCapabilities.hasTransport(NetworkCapabilities.TRANSPORT_CELLULAR)&&curStatus != 1) {
                        curStatus = 1;
                        SendMessage(1);
                        Log.d(TAG, "onCapabilitiesChanged: " + 1);

                        return;
                    }
                    if (networkCapabilities.hasTransport(NetworkCapabilities.TRANSPORT_WIFI)&&curStatus != 2) {
                        curStatus = 2;
                        SendMessage(2);
                        Log.d(TAG, "onCapabilitiesChanged: " + 2);
                    }

                }
            }
        };
    }
    public  void Register(){
        ConnectivityManager connectivityManager =
                (ConnectivityManager) UnityPlayer.currentActivity.getSystemService(Context.CONNECTIVITY_SERVICE);
        connectivityManager.registerDefaultNetworkCallback(networkCallback);
    }

    public  void UnRegister(){
        ConnectivityManager connectivityManager =
                (ConnectivityManager) UnityPlayer.currentActivity.getSystemService(Context.CONNECTIVITY_SERVICE);
        connectivityManager.unregisterNetworkCallback(networkCallback);
    }

    private void SendMessage(int what) {
        Message message = new Message();
        message.what = NETWORK_STATUS_MESSAGE_ID;
        message.obj = what;
        m_Handler.sendMessage(message);
    }
}
