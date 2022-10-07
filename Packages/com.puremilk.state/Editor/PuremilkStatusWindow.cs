using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;
using UnityEditor.Callbacks;
using System.IO;
using System.Xml;

namespace Puremilk.Status
{
    public class PuremilkStatusWindow : EditorWindow
    {

        public IEnumerable<Type> StatusTypes
        {
            get
            {
                return m_StatusTypes;
            }
            set
            {
                m_StatusTypes = value;
            }
        }

        private IEnumerable<Type> m_StatusTypes;
        [MenuItem("Window/PureMilk/Status")]
        private static void Init()
        {
            PuremilkStatusWindow window = GetWindow<PuremilkStatusWindow>("PureMilk Status", true);
            window.StatusTypes = GetAllTypeWithStatusAttribute();
            window.Show();
            OnPostProcess(BuildTarget.Android,null);
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();
            foreach (var item in StatusTypes)
            {
              string label=item.ToString();
              EditorGUI.BeginChangeCheck();
              bool curToggle =   GUILayout.Toggle(EditorPrefs.GetBool(label,false),label);
              bool isChanged=EditorGUI.EndChangeCheck();
              if(isChanged){
                EditorPrefs.SetBool(label,curToggle);
              }
            }
            GUILayout.EndVertical();
        }

        private static IEnumerable<Type> GetAllTypeWithStatusAttribute(){
              Assembly assembly = Assembly.GetAssembly(typeof(StatusAttribute));
            Type[] types = assembly.GetExportedTypes();
            var statuses = types.Where(o =>
            {
                Attribute[] attributes = Attribute.GetCustomAttributes(o);
                foreach (var item in attributes)
                {
                    if (item is StatusAttribute)
                    {
                        return true;
                    }
                }
                return false;
            });
            return statuses;
        }

        [PostProcessBuildAttribute]
        private static void OnPostProcess(BuildTarget target, string pathToBuiltProject){
           IEnumerable<Type> statuses=GetAllTypeWithStatusAttribute();
            switch (target)
            {
                case BuildTarget.Android:
                string path=string.Join("/",Application.dataPath,"Plugins/Android/AndroidManifest.xml");
                if(File.Exists(path))
                {
                    XmlDocument xmlDocument=new XmlDocument();
                    xmlDocument.LoadXml(path);
                    foreach (var item in statuses)
                    {
                       MethodInfo methodInfo = item.GetMethod("HandleBuildProcessAndroid");
                       if(methodInfo!=null){
                        methodInfo.Invoke(null,new object[]{xmlDocument});
                       }
                    }
                    xmlDocument.Save(path);
                }
                else
                {
                    Debug.LogError("The AndroidManifest.xml is not exit.Please use the custom main manifest");
                }

                break;
                case BuildTarget.iOS:
                break;
            }

        }
    }
}

