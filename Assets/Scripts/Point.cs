using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WPEditor
{
    [System.Serializable]
    public sealed class Point
    {
        public string m_name = default;
        public Vector3 position;
        public int id;

       public Point()
        {
            position = Vector3.zero;
        }

        public Point(Point m_wayPoint)
        {
            position = m_wayPoint.position;
        }

        public Point(Vector3 pos)
        {
            position = pos;
        }
    }
}

