using System;
namespace Puremilk.Status.Editor
{
    public class PostBuildAttribute:Attribute
    {
        public Type Status{
            get;set;
        }
        public PostBuildAttribute(Type status){
               Status=status;
        }
    }
}
