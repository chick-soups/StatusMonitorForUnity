using System.Xml;
namespace Puremilk.Status.Editor
{
    public interface IPostBuildHander{
    void HandlePostBuildAndroid(XmlDocument androidManifest);
    void HandleIOS();
}
}
