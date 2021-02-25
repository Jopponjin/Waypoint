using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaypointEditorRev2 : EditorWindow
{
    [MenuItem("Window/Wayoint Editor")]
    static void OpenWindow()
    {
        WaypointEditorRev2 window = (WaypointEditorRev2)GetWindow(typeof(WaypointEditorRev2));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }
}
