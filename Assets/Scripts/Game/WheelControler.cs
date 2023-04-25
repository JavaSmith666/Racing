using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelControler : MonoBehaviour
{
	//public
	[Range(40, 80)] public float maxSpeed;
	[Range(50, 100)] public float angleSpeed;
	//private
	private Transform LF_Wheel, RF_Wheel, LB_Wheel, RB_Wheel;
	private Rigidbody body;
	private Vector3 lastPos;
	private float velocity = 0;
	private float rotation = 0;
	private float dynamicFriction;
	private float moveForce;
	private float moveForceDrift;
	private bool drifting = false;
	private enum KeyVertical { W, S, None };
	private enum KeyHorizontal { A, D, None };
	//readonly
	private readonly float maxSpeedometer = 240;
	private readonly float wheelRadius = 0.4f * 0.6f; //0.4 init radius and 0.6 scale of the car.
	private readonly float maxAngle = 35;
	private readonly float rotateSpeed = 20;
	private readonly float rotateSpeedDrift = 50;
	private readonly float driftThreshold = 10;

	private void Start()
	{
		Transform wheels = transform.Find("Wheels");
		LF_Wheel = wheels.Find("LF_Wheel");
		RF_Wheel = wheels.Find("RF_Wheel");
		LB_Wheel = wheels.Find("LB_Wheel");
		RB_Wheel = wheels.Find("RB_Wheel");
		body = gameObject.GetComponent<Rigidbody>();
		body.centerOfMass = Vector3.zero;
		lastPos = transform.position;
		dynamicFriction = body.mass * 11.5f;
		moveForce = dynamicFriction * 1.5f;
		moveForceDrift = dynamicFriction * 0.9f;
	}

	private void FixedUpdate()
	{
		velocity = (transform.position - lastPos).magnitude / Time.fixedDeltaTime * 3.6f;
		GetMyKey(out KeyVertical vkey, out KeyHorizontal hkey);
		AddVerticalForce(hkey, vkey);
		RollWheels();
		RotateWheels(hkey);
		RotateBody(hkey);
		SpeedometerPointer.UpdateSpeed(velocity, 0, maxSpeedometer);
		lastPos = transform.position;
	}

	private void GetMyKey(out KeyVertical verticalKey, out KeyHorizontal horizontalKey)
	{
		if (Input.GetKey(KeyCode.W) && !(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
		{
			verticalKey = KeyVertical.W;
		}
		else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !Input.GetKey(KeyCode.W))
		{
			verticalKey = KeyVertical.S;
		}
		else
		{
			verticalKey = KeyVertical.None;
		}
		if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
		{
			horizontalKey = KeyHorizontal.A;
		}
		else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
		{
			horizontalKey = KeyHorizontal.D;
		}
		else
		{
			horizontalKey = KeyHorizontal.None;
		}
	}

	private bool IsDrifting(KeyHorizontal hkey,  KeyVertical vkey)
	{
		if (Vector3.Dot(transform.forward, transform.position - lastPos) > 0 &&
			vkey == KeyVertical.S && hkey != KeyHorizontal.None)
		{
			drifting = velocity >= driftThreshold;
		}
		else
		{
			drifting = false;
		}
		return drifting;
	}

	private void AddVerticalForce(KeyHorizontal hkey, KeyVertical vkey)
	{
		drifting = IsDrifting(hkey, vkey);
		if (vkey == KeyVertical.W && velocity < maxSpeed)
		{
			body.AddForce(transform.forward * moveForce);
		}
		if (vkey == KeyVertical.S && velocity < maxSpeed)
		{
			if(drifting)
			{
				body.AddForce(transform.forward * moveForceDrift);
			}
			else
			{
				body.AddForce(transform.forward * -moveForce);
			}	
		}
	}

	private void RollWheels()
	{
		float angle = velocity * Time.fixedDeltaTime / (2 * Mathf.PI * wheelRadius) * 360;
		Vector3 rotateVec = angle * Vector3.right;
		if (Vector3.Dot(transform.position - lastPos, transform.forward) >= 0)
		{
			LF_Wheel.Rotate(rotateVec);
			RF_Wheel.Rotate(rotateVec);
			LB_Wheel.Rotate(rotateVec);
			RB_Wheel.Rotate(rotateVec);
		}
		else
		{
			LF_Wheel.Rotate(-rotateVec);
			RF_Wheel.Rotate(-rotateVec);
			LB_Wheel.Rotate(-rotateVec);
			RB_Wheel.Rotate(-rotateVec);
		}
	}
	
	private void RotateWheels(KeyHorizontal key)
	{
		float angle = angleSpeed * Time.fixedDeltaTime;
		Vector3 right = RF_Wheel.position - LF_Wheel.position;
		Vector3 up = Vector3.Cross(transform.forward, right);
		
		if (key == KeyHorizontal.A && rotation > -maxAngle)
		{
			LF_Wheel.Rotate(up, -angle, Space.World);
			RF_Wheel.Rotate(up, -angle, Space.World);
			rotation -= angle;
		}
		if (key == KeyHorizontal.D && rotation < maxAngle)
		{
			LF_Wheel.Rotate(up, angle, Space.World);
			RF_Wheel.Rotate(up, angle, Space.World);
			rotation += angle;
		}
		if (key == KeyHorizontal.None && rotation != 0)
		{
			if (rotation > 0)
			{
				LF_Wheel.Rotate(up, -angle, Space.World);
				RF_Wheel.Rotate(up, -angle, Space.World);
				rotation -= angle;
			}
			else
			{
				LF_Wheel.Rotate(up, angle, Space.World);
				RF_Wheel.Rotate(up, angle, Space.World);
				rotation += angle;
			}
		}
	}

	private void RotateBody(KeyHorizontal key)
	{
		if (velocity >= 1)
		{
			float angle = rotateSpeed * Time.fixedDeltaTime;
			float angleDrift = rotateSpeedDrift * Time.fixedDeltaTime;
			Vector3 right = RF_Wheel.position - LF_Wheel.position;
			Vector3 up = Vector3.Cross(transform.forward, right);
			if (Vector3.Dot(transform.forward, transform.position - lastPos) > 0)
			{
				if (key == KeyHorizontal.A)
				{
					transform.Rotate(up, (drifting ? -angleDrift : -angle), Space.World);
				}
				if (key == KeyHorizontal.D)
				{
					transform.Rotate(up, (drifting ? angleDrift : angle), Space.World);
				}
			}
			else
			{
				if (key == KeyHorizontal.A)
				{
					transform.Rotate(up, angle, Space.World);
				}
				if (key == KeyHorizontal.D)
				{
					transform.Rotate(up, -angle, Space.World);
				}
			}
		}
	}
}