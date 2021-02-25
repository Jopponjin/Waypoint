using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace WPEditor
{
    public class WaypointSystem : MonoBehaviour
    {
        [HideInInspector]
        public Point[] points = new Point[0];
        private int lastId = 0;
        bool drawnDebugGizmos = true;

        public int length
        {
            get { return points.Length; }
        }

        public Point this[int index]
        {
            get { return points[index]; }
            set { points[index] = value; }
        }
        
        public Point last
        {
            get { return points[length - 1]; }
        }

        public Point AddWaypoint(Point m_NewPoint)
        {
            Array.Resize<Point>(ref points, length + 1);
            m_NewPoint.id = lastId++;
            return m_NewPoint;
        }

        public Point Duplicate(int m_index)
        {
            Point point = new Point(this.points[m_index]);

            point.id = lastId++;

            Point[] points = this.points;
            Array.Resize<Point>(ref points, length + 1);

            for (int i = 0; i < length; i++)
            {
                points[i + 1] = points[i];
            }
            points[m_index + 1] = point;
            return point;
        }

        public void RemovePointObject(Point m_point)
        {
            Point[] saved = points;
            int jump = 0;

            Array.Resize<Point>(ref points, length - 1);

            for (int i = 0; i < length; i++)
            {
                if (points[i].id == m_point.id)
                {
                    jump++;
                }
                points[i] = saved[i + jump];
            }
        }

        public void RemovePoint(int m_index)
        {
            Point[] m_saved = points;
            int m_jump = 0;

            Array.Resize<Point>(ref points, length - 1);
            for (int i = 0; i < length; i++)
            {
                if (i == m_index)
                {
                    m_jump++;
                }
                points[i] = m_saved[i + m_jump];
            }
        }

        public Vector3 GetClosestPoint(Vector3 m_position)
        {
            float m_closestDistance = float.MaxValue;
            int m_closestindex = -1;

            for (int i = 0; i < length; i++)
            {
                float m_distance = Vector3.Distance(m_position, points[i].position);

                if (m_distance < m_closestDistance + 2f)
                {
                    m_closestindex = i;
                }
            }

            Debug.Log("[WAYPOINT]: Closest point is: " + m_closestindex);
            return points[m_closestindex].position;
        }

        private void OnDrawGizmos()
        {
            if (drawnDebugGizmos)
            {
                float tTime = Time.deltaTime;
                if (points != null)
                {
                    for (int i = 0; i < points.Length - 1; i++)
                    {
                        Gizmos.color = Color.green;
                        Gizmos.DrawSphere(points[0].position, 1f);
                        Gizmos.color = Color.red;
                        Gizmos.DrawLine(points[i].position, points[i + 1].position);
                        Gizmos.color = Color.cyan;
                        Gizmos.DrawSphere(points[i].position, 1f);
                        Gizmos.color = Color.red;
                        Gizmos.DrawLine(points[points.Length - 1].position, points[0].position);
                        Gizmos.color = Color.cyan;
                        Gizmos.DrawSphere(points[points.Length - 1].position, 1f);
                    }
                }
            }
        }
    }
}

