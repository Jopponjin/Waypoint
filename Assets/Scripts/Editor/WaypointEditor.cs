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
                Vector3 m_tempPos;

                if (waypointSystem.points.Length >= 2)
                {
                    Vector3 direction = (waypointSystem.points[waypointSystem.points.Length -1].position - waypointSystem[0].position).normalized;
                    m_tempPos = waypointSystem.points[waypointSystem.points.Length - 1].position + direction;

                    

                    Debug.Log("position to put new point on " + m_tempPos);

                     var m_newPoint = waypointSystem.AddWaypoint(new Point()).m_name = "Waypoint";

                    if (waypointSystem.last != null)
                    {
                        if (waypointSystem.points[waypointSystem.length - 1].position == Vector3.zero)
                        {
                            waypointSystem.points[waypointSystem.length - 1].position = m_tempPos;
                        }
                    }
                }
                else
                {
                    waypointSystem.AddWaypoint(new Point()).m_name = "Waypoint";
                }
                EditorGUI.EndChangeCheck();
            }

            if (GUILayout.Button(EditorGUIUtility.IconContent("d_Toolbar Minus"), GUILayout.ExpandWidth(true)))
            {
                waypointSystem.RemovePoint(waypointSystem.points.Length -1);
            }

            GUILayout.EndHorizontal();

            for (int i = 0; i < waypointSystem.points.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUI.BeginChangeCheck();
                serializedObj.ApplyModifiedProperties();

                if (waypointSystem.points[i] != null)
                {
                    EditorGUILayout.Vector3Field(string.Empty, waypointSystem.points[i].position, GUILayout.ExpandWidth(false));
                }

                serializedObj.ApplyModifiedProperties();
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();
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


