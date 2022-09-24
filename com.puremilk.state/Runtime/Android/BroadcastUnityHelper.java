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

package com.puremilk.broadcast;
import android.app.Activity;
import android.content.BroadcastReceiver;
import android.content.IntentFilter;
import com.unity3d.player.UnityPlayer;

public class BroadcastUnityHelper {
    private BroadcastReceiver m_BroadReceiver;
    private String[] m_Actions;
    public BroadcastUnityHelper(String[] actions,IBroadcastReceiver broadcastReciever){
        this.m_Actions=actions;
        this.m_BroadReceiver=new UnityBroadcastReceiver(broadcastReciever);

    }

    public void Register() {
        Activity currentActivity = UnityPlayer.currentActivity;
        if (currentActivity != null) {
            IntentFilter intentFilter=new IntentFilter();
            for (int i = 0; i < m_Actions.length; i++) {
                intentFilter.addAction(m_Actions[i]);
            }
            currentActivity.registerReceiver(m_BroadReceiver, intentFilter);
        }
    }

    public  void UnRegister()
    {
        Activity currentActivity = UnityPlayer.currentActivity;
        if (currentActivity != null) {
            currentActivity.unregisterReceiver(m_BroadReceiver);
        }
    }
}