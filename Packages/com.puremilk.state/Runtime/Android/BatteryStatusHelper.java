
// Copyright 2022 Xuhua Chow

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

//     http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
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
