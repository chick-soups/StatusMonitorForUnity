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

using UnityEngine;
using System.Text;

namespace Puremilk.Broadcast
{
    public class OnRecieveCallback : AndroidJavaProxy, IBroadCastReceiver
    {

        public OnRecieveCallback() : base("com.puremilk.broadcast.IBroadcastReceiver")
        {
        }

        public virtual void OnReceive(string action, string[] keys, string[] values)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("action:" + action);
            for (int i = 0; i < keys.Length; i++)
            {
                stringBuilder.AppendLine(string.Format("key:{0} value:{1}", keys[i], values[i]));
            }
            Debug.Log(stringBuilder.ToString());
        }
    }

}

