using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedometerPointer : MonoBehaviour
{
    private static float minAngle = 210f;
    private static float maxAngle = -30f;
    static SpeedometerPointer thisSpeedomter;

    private void Start()
    {
        thisSpeedomter = this;
    }

    public static void UpdateSpeed(float speed, float minVelocity, float maxVelocity)
    {
        //scale the speed two times.
        float angle = Mathf.Lerp(minAngle, maxAngle, Mathf.InverseLerp(minVelocity, maxVelocity, speed));
        thisSpeedomter.transform.eulerAngles = new Vector3(0f, 0f, angle);
    }
}
