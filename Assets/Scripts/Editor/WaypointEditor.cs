using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace WPEditor
{
    [CustomEditor(typeof(WaypointSystem))]
    public class waypointEditor : Editor
    {
        WaypointSystem waypointSystem;
        SerializedObject serializedObj;
        int editorIndex = -1;

        GUISkin sceneSkin;
        GUISkin inspectorSkin;

        private void OnEnable()
        {
            sceneSkin = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Scene);
            inspectorSkin = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector);
            Tools.hidden = false;
            waypointSystem = target as WaypointSystem;
            serializedObj = serializedObject;
        }

        private void OnDisable()
        {
            Tools.hidden = false;
            
        }

        private void OnSceneGUI() 
        {
            for (int i = 0; i < waypointSystem.length; i++)
            {
                Handles.Label(waypointSystem.points[i].position + Vector3.down, i.ToString(), sceneSkin.textField);

                EditorGUI.BeginChangeCheck();
                Vector3 pointVectorPos = Handles.PositionHandle(waypointSystem.points[i].position, Quaternion.identity);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(waypointSystem, "Moved Waypoints");
                    waypointSystem.points[i].position = pointVectorPos;
                }
            }
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            serializedObj.Update();

            if (GUILayout.Button(EditorGUIUtility.IconContent("d_Toolbar Plus"), GUILayout.ExpandWidth(true)))
            {
                EditorGUI.BeginChangeCheck();
                waypointSystem.AddWaypoint(new Point()).m_name = "Waypoint";
                EditorGUI.EndChangeCheck();
            }


            if (GUILayout.Button(EditorGUIUtility.IconContent("d_Toolbar Minus"), GUILayout.ExpandWidth(true)))
            {
                waypointSystem.RemovePoint(waypointSystem.points.Length -1);
            }

            GUILayout.EndHorizontal();
            
            EditorGUILayout.Space();

            

            for (int i = 0; i < waypointSystem.points.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUI.BeginChangeCheck();
                serializedObj.ApplyModifiedProperties();

                Vector3 m_waypointValue = Vector3.zero;

                m_waypointValue = waypointSystem.points[i].position;

                EditorGUILayout.Vector3Field(string.Empty, m_waypointValue, GUILayout.ExpandWidth(false));

                serializedObj.ApplyModifiedProperties();
                EditorGUILayout.EndHorizontal();
            }
        }

        void DrawLines()
        {
            int IndexID = 0;
            for (int i = 0; i < waypointSystem.points.Length - 1; i++)
            {
                IndexID++;
                Handles.DrawLine(waypointSystem.points[i].position, waypointSystem.points[i + 1].position);
                //Handles.SphereHandleCap(IndexID, waypointSystem.points[i].position, waypointSystem.points[i], 1f, EventType.Repaint);
            }       
        }
    }
}


