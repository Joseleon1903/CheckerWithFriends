using Assets.Scripts.Profile;
using UnityEngine;

namespace Assets.Scripts.Utils
{
	public struct HitPoint {
		public HitPoint(int x, int y)
		{
			positionX = x;
			positionY = y;
		}

		public int positionX { get; }
		public int positionY { get; }

		public override string ToString() => $"({positionX}, {positionY})";
	}

	public class Finder
    {

		public static HitPoint RayHitPointFromScreen(Vector3 hitPosition)
		{
			RaycastHit hit;

			if (Physics.Raycast(Camera.main.ScreenPointToRay(hitPosition), out hit, 50.0f, LayerMask.GetMask("CheesPlane")))
			{
				int x = (int)hit.point.x;
				int y = (int)hit.point.z;
				return new HitPoint(x, y);
			}
			return new HitPoint(-1, -1); ;
		}

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
