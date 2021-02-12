using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[Serializable]
	public struct ZoomAverange {
		public float MaxZoom;
		public float MinZoom;
	}

	[Serializable]
	public struct OrbitAverange
	{
		public float MaxUp;
		public float MinDown;

	}


	[SerializeField] private GameObject targetGameObject;
	[SerializeField] private Vector3 targetPosOffset;
	[SerializeField] private float orbitSpeed;
	[SerializeField] private float orbitDistance;
	[SerializeField] private float zoomSpeed;

	[SerializeField] private ZoomAverange zoomAverange;

	[SerializeField] private OrbitAverange orbitAverange;

	private Vector3 targetPos;

	private bool mouseClickjudge;

	public bool ActiveOrbitation = false;

	private void Awake()
    {
		targetPos = targetGameObject.transform.position;
	}

    private void Update()
	{
		if (ActiveOrbitation) {

			Orbiting();
		}
		Zooming();
		transform.LookAt(targetPos, transform.up);
	}

	private void Orbiting()
	{

		if (Input.GetMouseButtonDown(0)) { mouseClickjudge = true; }
		if (Input.GetMouseButtonUp(0)) { mouseClickjudge = false; }
		if (mouseClickjudge) { transform.RotateAround(targetPos, Vector3.up, Input.GetAxis("Mouse X") * orbitSpeed); }

    }

	private void OrbitInDirection(Vector3 direction)
	{
		
		transform.RotateAround(targetGameObject.transform.position, direction, 50 * Time.deltaTime);
		//transform.Translate(orbitSpeed * Time.deltaTime * direction);
	}

	private void Zooming()
	{

		float mouseAxis = Input.GetAxis("Mouse ScrollWheel");
		//Debug.Log("Mouse :" + mouseAxis);
		orbitDistance += mouseAxis * zoomSpeed * -1;
        //	//-1 to inverse scrolling direction
        orbitDistance = Mathf.Abs(orbitDistance);

		if (orbitDistance > zoomAverange.MinZoom  && orbitDistance < zoomAverange.MaxZoom) {

			transform.position = (transform.position - targetPos).normalized
							 * orbitDistance + targetPos;
			return;
		}

		if (orbitDistance > zoomAverange.MaxZoom) {
			orbitDistance = zoomAverange.MaxZoom;
		}else if (orbitDistance < zoomAverange.MinZoom) {
			orbitDistance = zoomAverange.MinZoom;
		}
		
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, targetGameObject.transform.position);
		Gizmos.DrawSphere(targetGameObject.transform.position, 1);
	}
}
