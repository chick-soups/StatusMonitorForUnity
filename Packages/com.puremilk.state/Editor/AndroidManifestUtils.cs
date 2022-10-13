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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                        xmlElement.SetAttribute("android:name", item);
                        manifestElement.AppendChild(xmlElement);
                    }

                }
            }
        }
    }
}
