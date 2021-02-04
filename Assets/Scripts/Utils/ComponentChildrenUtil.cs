using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class ComponentChildrenUtil
    {
        /// <summary>
        ///  method to search in component parent for a child using the tag
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
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

        /// <summary>
        ///  method to search in component parent all childs with the tag
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
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

        /// <summary>
        ///  method to search in component parent the child with the component name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
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