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

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;

public class UnityBroadcastReceiver extends BroadcastReceiver {
    

    private  IBroadcastReceiver m_IBroadcastReceiver;
    public UnityBroadcastReceiver(IBroadcastReceiver iBroadcastReceiver){
        super();
        this.m_IBroadcastReceiver=iBroadcastReceiver;
    }
    @Override
    public void onReceive(Context context, Intent intent) {
        
        String action = intent.getAction();
        Bundle bundle = intent.getExtras();
        int size=bundle.size();
        String[] keys=new String[size];
        String[] values=new String[size];
        int index=0;
        for (String key:bundle.keySet())
        {
            keys[index]=key;
            Object object = bundle.get(key);
            values[index]=object.toString();
            index++;

        }
        m_IBroadcastReceiver.OnReceive(action,keys,values);
    }
}
