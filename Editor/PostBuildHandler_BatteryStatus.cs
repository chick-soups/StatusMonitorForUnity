using System.Xml;

namespace Puremilk.Status.Editor
{
    [PostBuild(typeof(BatteryStatus))]
    public class PostBuildHandler_BatteryStatus : IPostBuildHander
    {
        public  void HandlePostBuildAndroid(XmlDocument androidManifest)
        {
            return;
        }
        public void HandleIOS()
        {

        }
    }

}
