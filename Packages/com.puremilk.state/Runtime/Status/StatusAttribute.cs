using System;

namespace Puremilk.Status
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Assembly|AttributeTargets.Module,AllowMultiple =true)]
    public class StatusAttribute : Attribute
    {
        
    }
}

