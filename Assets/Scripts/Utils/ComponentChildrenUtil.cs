using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class ComponentChildrenUtil
    {

        public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component
        {
            Transform t = parent.transform;
            foreach (Transform tr in t)
            {
                if (tr.tag == tag)
                {
                    return tr.GetComponent<T>();
                }
            }
            return null;
        }
         
        public static T[] FindComponentInChildsWithTag<T>(this GameObject parent, string tag) where T : Component
        {
            Transform t = parent.transform;
            List<T> list = new List<T>();
            foreach (Transform tr in t)
            {
                if (tr.tag == tag)
                {
                    list.Add(tr.GetComponent<T>());
                }
            }
            return list.ToArray();
        }

        public static T FindComponentInChildWithName<T>(this GameObject parent, string name) where T : Component
        {
            Transform t = parent.transform;
            foreach (Transform tr in t)
            {
                if (tr.name == name)
                {
                    return tr.GetComponent<T>();
                }
            }
            return null;
        }

    }
}