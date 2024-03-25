using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Airplane_Tachometer : MonoBehaviour, IAirplaneUI
{
    #region Variables
    [Header("Tachometer Properties")]
    public RectTransform pointer;
    public Airplane_Engine engine;
    public float maxRPM = 3500f;
    public float maxRotation = 312f;
    public float pointerSpeed = 2f;

    private float finalRotation;
    #endregion

    #region Interface Methods
    public void HandleAirplaneUI()
    {
        if (engine && pointer)
        {
            float normalizedRPM = Mathf.InverseLerp(0f, maxRPM, engine.CurrentRPM); 

            float wantedRotation = maxRotation * -normalizedRPM;
            finalRotation = Mathf.Lerp(finalRotation, wantedRotation, Time.deltaTime * pointerSpeed);
            pointer.rotation = Quaternion.Euler(0f, 0f, finalRotation);  
        }
    }
    #endregion

    #region Custom Methods
    #endregion
}
