using Assets.Scripts.Chess;
using Assets.Scripts.General;
using Assets.Scripts.Profile;
using System;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class Finder
    {
		public static GameObject RayHitFromScreen(Vector3 hitPosition)
		{
			RaycastHit hit;

			if (Physics.Raycast(Camera.main.ScreenPointToRay(hitPosition), out hit, 50.0f, LayerMask.GetMask("CheesPlane")))
			{
				int x = (int)hit.point.x;
				int y = (int)hit.point.z;
				if (ChessBoarderManager.Instance.ChessTable[x, y] != null)
				{
					return ChessBoarderManager.Instance.ChessTable[x, y].gameObject;
				}
			}
			return null;
		}

		public static Node RayHitNodeFromScreen(Vector3 hitPosition)
		{
			RaycastHit hit;

			if (Physics.Raycast(Camera.main.ScreenPointToRay(hitPosition), out hit, 50.0f, LayerMask.GetMask("CheesPlane")))
			{
				int x = (int)hit.point.x;
				int y = (int)hit.point.z;
				return BoardGrid.Instance.GetNodeAt(y, x);
			}
			return null;
		}

		public static T RayHitFromScreen<T>(Vector3 hitPosition)
		{
			GameObject go = RayHitFromScreen(hitPosition);
			if (go == null) return default(T);
			object o = go.GetComponentInChildren(typeof(T));
			if (o == null) return default(T);
			return (T)Convert.ChangeType(o, typeof(T));
		}

		public static IClickable IClickableRayHitFromScreen(Vector3 hitPosition)
		{
			Ray ray = Converter.ScreenPointToRay(hitPosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, float.PositiveInfinity, GameManager.Instance.CLickableMask))
			{
				return hit.transform.gameObject.GetComponent(typeof(IClickable)) as IClickable;
			}

			return null;
		}


		public static GameObject FindGameProfile()
		{
			GameObject objectG = null;
			if (GameObject.FindObjectOfType<BaseProfile>() != null) {

				objectG = GameObject.FindObjectOfType<BaseProfile>().gameObject;
			}
			return objectG;
		}
	}
}
