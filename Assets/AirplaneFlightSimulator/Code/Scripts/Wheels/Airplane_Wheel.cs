using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WheelCollider))]
public class Airplane_Wheel : MonoBehaviour
{
    #region Variables
    private WheelCollider wheelCol;
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
    #endregion
}
