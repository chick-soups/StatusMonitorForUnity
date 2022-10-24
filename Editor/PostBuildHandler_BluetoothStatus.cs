using System.Xml;
namespace Puremilk.Status.Editor
{
    [PostBuild(typeof(BluetoothStatus))]
    public class PostBuildHandler_BluetoothStatus : IPostBuildHander
    {
        public void HandlePostBuildAndroid(XmlDocument androidManifest)
        {
            return;
        }
        public void HandleIOS()
        {

        }
    }

}
