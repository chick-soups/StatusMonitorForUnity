using System.Collections.Generic;
using System.Xml;

namespace Puremilk.Status.Editor
{
    internal class AndroidManifestUtils
    {
        public static void AddPermissions(XmlDocument androidManifest, List<string> permissions)
        {
            XmlElement manifestElement = androidManifest["manifest"];
            if (manifestElement != null)
            {
                XmlNodeList permissionNodeList = manifestElement.GetElementsByTagName("uses-permission");
                if (permissionNodeList.Count > 0)
                {
                    for (int i = 0; i < permissionNodeList.Count; i++)
                    {
                        XmlNode xmlNode = permissionNodeList[i];
                        XmlAttributeCollection attributeCollection = xmlNode.Attributes;
                        XmlAttribute xml = attributeCollection[0];
                        if (permissions.Contains(xml.InnerText))
                        {
                            permissions.Remove(xml.InnerText);
                        }

                    }
                }
                if (permissions.Count > 0)
                {
                    foreach (var item in permissions)
                    {
                        XmlElement xmlElement = androidManifest.CreateElement("uses-permission");
                        //xmlElement.SetAttribute("android:name", item);
                        xmlElement.SetAttribute("name","http://schemas.android.com/apk/res/android",item);
                        manifestElement.AppendChild(xmlElement);
                    }

                }
            }
        }
    }
}
