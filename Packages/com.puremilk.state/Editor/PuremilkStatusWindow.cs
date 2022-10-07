using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;

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
            window.Show();
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
            window.StatusTypes = statuses;


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
    }
}

