using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelControler : MonoBehaviour
{
    public float angleSpeed = 100f;
    public float maxSpeed = 120f;
    public Transform LF_Wheel, RF_Wheel, LB_Wheel, RB_Wheel;
    public WheelCollider LF_Collider, LB_Collider, RF_Collider, RB_Collider;
    private float maxAngleInner = 39.6f;
    private float maxAngleOuter = 33.5f;
    private float driftThreshold = 30f;
    private float horizontalForceDrift;
    private float motorTorque;
    private float brakeTorque;
    private int velocity;
    private Vector3 lastPos;

    private void Start()
    {
        lastPos = gameObject.transform.position;
        Rigidbody carRigidBody = gameObject.GetComponent<Rigidbody>();
        horizontalForceDrift = carRigidBody.mass * 5f;
        motorTorque = carRigidBody.mass * 4f;
        brakeTorque = carRigidBody.mass * 4f;
        carRigidBody.centerOfMass = Vector3.zero;
    }

    private void FixedUpdate()
    {
        //Set torque of back wheels.
        WheelsTorqueUpdate();
        WheelsAngleUpdate();

        //Update the position of four wheels.
        WheelsModelUpdate(LF_Wheel, LF_Collider);
        WheelsModelUpdate(RF_Wheel, RF_Collider);
        WheelsModelUpdate(LB_Wheel, LB_Collider);
        WheelsModelUpdate(RB_Wheel, RB_Collider);

        //Velocity of car.
        velocity = (int)((transform.position - lastPos).magnitude / Time.fixedDeltaTime * 3.6f);
        Debug.LogFormat("current velocity: {0} km/h", velocity);

        //Update the position of lastPos at the end of function FixedUpdate.
        lastPos = transform.position;
    }

    //Update the torque of wheels.
    private void WheelsTorqueUpdate()
    {
        //Torque of wheels.
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            && !(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            DoTorqueUpdate(transform.position - lastPos);
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            DoTorqueUpdate(transform.position - lastPos);
        }
        else
        {
            LB_Collider.motorTorque = RB_Collider.motorTorque = 0f;
            LF_Collider.brakeTorque = RF_Collider.brakeTorque = 0.1f * brakeTorque;
            LB_Collider.brakeTorque = RB_Collider.brakeTorque = 0.1f * brakeTorque;
        }
        if (velocity >= maxSpeed)
        {
            LB_Collider.motorTorque = RB_Collider.motorTorque = 0f;
            LF_Collider.brakeTorque = RF_Collider.brakeTorque = brakeTorque;
            LB_Collider.brakeTorque = RB_Collider.brakeTorque = brakeTorque;
        }
    }

    private void AddHorizontalForce()
    {
        if(velocity >= driftThreshold)
        {
            Vector3 right = (transform.Find("Wheels").Find("RF_Wheel").position -
            transform.Find("Wheels").Find("LF_Wheel").position).normalized;
            Vector3 forcePos = transform.position + transform.forward * -1f;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                gameObject.GetComponent<Rigidbody>().AddForceAtPosition(right * horizontalForceDrift, forcePos);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-right * horizontalForceDrift, forcePos);
            }
        }
    }

    private void DoTorqueUpdate(Vector3 v_dir)
    {
        float fvVal = Vector3.Dot(transform.forward * Input.GetAxisRaw("Vertical"), v_dir);
        if (fvVal < 0f && Mathf.Abs(fvVal) > 1e-6f)
        {
            //braking
            AddHorizontalForce();
            LB_Collider.motorTorque = RB_Collider.motorTorque = 0f;
            LF_Collider.brakeTorque = RF_Collider.brakeTorque = brakeTorque;
            LB_Collider.brakeTorque = RB_Collider.brakeTorque = brakeTorque;
        }
        else
        {
            //accelerating
            LB_Collider.motorTorque = RB_Collider.motorTorque = Input.GetAxisRaw("Vertical") *  motorTorque;
            LF_Collider.brakeTorque = RF_Collider.brakeTorque = 0f;
            LB_Collider.brakeTorque = RB_Collider.brakeTorque = 0f;
        }
    }

    private void WheelsAngleUpdate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            LF_Collider.steerAngle -= Time.fixedDeltaTime * angleSpeed;
            RF_Collider.steerAngle -= Time.fixedDeltaTime * angleSpeed;
            if (LF_Collider.steerAngle < -maxAngleInner)
            {
                LF_Collider.steerAngle = -maxAngleInner;
            }
            if (RF_Collider.steerAngle < -maxAngleOuter)
            {
                RF_Collider.steerAngle = -maxAngleOuter;
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            LF_Collider.steerAngle += Time.fixedDeltaTime * angleSpeed;
            RF_Collider.steerAngle += Time.fixedDeltaTime * angleSpeed;
            if (LF_Collider.steerAngle > maxAngleOuter)
            {
                LF_Collider.steerAngle = maxAngleOuter;
            }
            if (RF_Collider.steerAngle > maxAngleInner)
            {
                RF_Collider.steerAngle = maxAngleInner;
            }
        }
        else
        {
            LF_Collider.steerAngle = RF_Collider.steerAngle = 0f;
        }
    }

    //Wheel models follow the position and rotation of wheel colliders.
    private void WheelsModelUpdate(Transform t, WheelCollider wheel)
    {
        wheel.GetWorldPose(out Vector3 pos, out Quaternion rot);
        t.SetPositionAndRotation(pos, rot);
    }
}