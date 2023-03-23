using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelControler : MonoBehaviour
{
    public float maxAngle = 35f;
    public float angleSpeed = 50f;
    public float moveSpeed = 200;
    public float breakMove = (float)1e8;

    public Transform LF_Wheel, RF_Wheel;

    public WheelCollider LF_Collider, LB_Collider, RF_Collider, RB_Collider;

    // Update is called once per frame
    private void FixedUpdate()
    {
        float speed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = moveSpeed * 1.5f;
        }
        //³µÂÖÅ¤¾Ø
        if (Input.GetKey(KeyCode.W))
        {
            LB_Collider.motorTorque = RB_Collider.motorTorque = speed;
            LB_Collider.brakeTorque = RB_Collider.brakeTorque = .0f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            LB_Collider.motorTorque = RB_Collider.motorTorque = -speed;
            LB_Collider.brakeTorque = RB_Collider.brakeTorque = .0f;
        }
        else
        {
            LB_Collider.motorTorque = RB_Collider.motorTorque = .0f;
            LB_Collider.brakeTorque = RB_Collider.brakeTorque = breakMove;
        }

        //ÂÖÌ¥Æ«×ª
        if (Input.GetKey(KeyCode.A))
        {
            LF_Collider.steerAngle -= Time.deltaTime * angleSpeed;
            RF_Collider.steerAngle -= Time.deltaTime * angleSpeed;
            if (LF_Collider.steerAngle < -maxAngle || RF_Collider.steerAngle < -maxAngle)
            {
                LF_Collider.steerAngle = RF_Collider.steerAngle = -maxAngle;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            LF_Collider.steerAngle += Time.deltaTime * angleSpeed;
            RF_Collider.steerAngle += Time.deltaTime * angleSpeed;
            if (LF_Collider.steerAngle > maxAngle || RF_Collider.steerAngle > maxAngle)
            {
                LF_Collider.steerAngle = RF_Collider.steerAngle = maxAngle;
            }
        }
        else
        {
            LF_Collider.steerAngle = RF_Collider.steerAngle = .0f;
        }
    }

    //Ç°ÂÖ×·Ëæ³µÂÖÅö×²Æ÷Î»ÖÃ¼°Æ«×ª½Ç¶È
    private void WheelsModel_Update(Transform t, WheelCollider wheel)
    {
        wheel.GetWorldPose(out Vector3 pos, out Quaternion rot);
        t.SetPositionAndRotation(pos, rot);
    }

    private void Update()
    {
        WheelsModel_Update(LF_Wheel, LF_Collider);
        WheelsModel_Update(RF_Wheel, RF_Collider);
        float velocity = LF_Collider.rpm * 2 * Mathf.PI * LF_Collider.radius * 60 / 1000;
        Debug.LogFormat("current velocity: {0} km/h", velocity);
    }
}