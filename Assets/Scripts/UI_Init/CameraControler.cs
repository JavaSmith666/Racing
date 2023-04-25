using UnityEngine;

public class CameraControler : MonoBehaviour
{
	public GameObject car;
	private Vector3 cameraPosInit;
	private Vector3 lastPos;
	private readonly float coe = 0.1f;
	private float verAngle; //angle in radians
	private float verAngleInit; //angle in radians

	private void Start()
	{
		cameraPosInit = transform.position;
		lastPos = Input.mousePosition;
		verAngle = verAngleInit = Mathf.Atan2(cameraPosInit.y, Mathf.Abs(cameraPosInit.z));
	}

	private void Update()
	{
		if (Input.GetMouseButton(0) && (UITool.Instance.GetActivePanel().name == "StartPanel" ||
			UITool.Instance.GetActivePanel().name == "GaragePanel"))
		{
			RotateCamera(Input.mousePosition - lastPos);
		}
		lastPos = Input.mousePosition;
	}

	private void RotateCamera(Vector3 vec)
	{
		transform.RotateAround(car.transform.position, Vector3.up, vec.x * coe);
		if (vec.y >= 0)
		{
			if(verAngle - vec.y * coe * Mathf.Deg2Rad >= verAngleInit)
			{
				transform.RotateAround(car.transform.position, transform.right, -vec.y * coe);
				verAngle -= vec.y * coe * Mathf.Deg2Rad;
			}
		}
		else
		{
			if (verAngle - vec.y * coe * Mathf.Deg2Rad <= Mathf.PI / 2)
			{
				transform.RotateAround(car.transform.position, transform.right, -vec.y * coe);
				verAngle += -vec.y * coe * Mathf.Deg2Rad; 
			}
		}
	}
}
