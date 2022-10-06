using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Puremilk.Status
{
    public class PuremilkStatusWindow : EditorWindow
    {
        [MenuItem("Window/PureMilk/Status")]
        private static void Init()
        {
            PuremilkStatusWindow window = GetWindow<PuremilkStatusWindow>("PureMilk Status", true);
            window.Show();
        }

        private void OnGUI()
        {
            
        }


    }
}

