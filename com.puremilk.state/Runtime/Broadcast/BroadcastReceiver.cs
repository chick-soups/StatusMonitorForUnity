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
using System;

namespace Puremilk.Broadcast
{
    public class BroadcastReceiver : IDisposable
{
    protected AndroidJavaObject m_javaObject;
    private bool _disposed;
    public BroadcastReceiver(string[] actions,OnRecieveCallback callback)
    {
        m_javaObject = new AndroidJavaObject("com.puremilk.broadcast.BroadcastUnityHelper", actions,callback);
    }
    ~BroadcastReceiver()
    {
        Dispose(false);
    }
    public virtual void Register()
    {
        m_javaObject.Call("Register");
    }
    public virtual void UnRegister()
    {
        m_javaObject.Call("UnRegister");

    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }
        if (disposing)
        {
            m_javaObject.Dispose();
        }
        _disposed = true;
    }
}

}

