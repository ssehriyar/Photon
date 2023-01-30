using UnityEditor;
using UnityEngine;

namespace NewUtils
{
    [CustomEditor(typeof(NumberManager))]
    public class NumberManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            NumberManager numManager = target as NumberManager;

            if (GUILayout.Button("Generate Numbers"))
            {
                numManager.Create();
            }
        }
    }
}