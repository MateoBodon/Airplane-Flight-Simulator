using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WheelCollider))]
public class Airplane_Wheel : MonoBehaviour
{
    #region Variables
    [Header("Wheel Properties")]
    public Transform wheelGraphic;
    public bool isBraking = false;
    public float brakePower = 5f;
    public bool isStrering = false;
    public float steeringAngle = 20f;
    public float streeingSmoothSpeed = 2f;

    private WheelCollider wheelCol;
    private Vector3 worldPos;
    private Quaternion worldRot;
    private float finalBrakeForce;
    private float finalSteeringAngle;
    #endregion

    #region Builtin Methods
    void Start()
    {
        wheelCol = GetComponent<WheelCollider>();
    }
    #endregion

    #region Custom Methods
    public void InitWheel()
    {
        wheelCol = GetComponent<WheelCollider>();

        if (wheelCol)
        {
            wheelCol.motorTorque = 0.000000000000000000001f;
        }
    }

    public void HandleWheel(BaseAirplane_Input input)
    {
        if (wheelCol)
        {
            wheelCol.GetWorldPose(out worldPos, out worldRot);
            if (wheelGraphic)
            {
                wheelGraphic.position = worldPos;
                wheelGraphic.rotation = worldRot;
            }

            if (isBraking)
            {
                if (input.Brake > 0.1f)
                {
                    finalBrakeForce = Mathf.Lerp(finalBrakeForce, input.Brake * brakePower, Time.deltaTime);
                    wheelCol.brakeTorque = finalBrakeForce;
                }
                else
                {
                    finalBrakeForce = 0f;
                    wheelCol.brakeTorque = 0f;
                    wheelCol.motorTorque = 0.000000000000000000001f;
                }
            }

            if (isStrering)
            {
                finalSteeringAngle = Mathf.Lerp(finalSteeringAngle, -input.Yaw * steeringAngle, Time.deltaTime * streeingSmoothSpeed);
                wheelCol.steerAngle = finalSteeringAngle;
            }
        }
    }
    #endregion
}
