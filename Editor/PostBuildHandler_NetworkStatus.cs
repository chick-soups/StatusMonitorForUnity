using System.Xml;
using System.Collections.Generic;
namespace Puremilk.Status.Editor
{
    [PostBuild(typeof(NetworkStatus))]
    public class PostBuildHandler_NetworkStatus : IPostBuildHander
    {
        public  void HandlePostBuildAndroid(XmlDocument androidManifest)
        {
            List<string> permissions = new List<string>{
            "android.permission.ACCESS_NETWORK_STATE",
            "android.permission.CHANGE_NETWORK_STATE",
            "android.permission.INTERNET"
            };
            AndroidManifestUtils.AddPermissions(androidManifest,permissions);
        

        }
        public void HandleIOS()
        {

        }
    }

}
