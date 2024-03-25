using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane_Altimeter : MonoBehaviour, IAirplaneUI
{
    #region Variables
    [Header("Altimeter Properties")]
    public Airplane_Controller airplane;
    public RectTransform hundredsPointer;
    public RectTransform thousandsPointer;
    #endregion

    #region Builtin Methods
    // Start is called before the first frame update
    void Start()
    {
    }
    #endregion

    #region Interface Methods
    public void HandleAirplaneUI()
    {
        if(airplane)
        {
            float currentAlt = airplane.CurrentMSL;
            float currentThousands = currentAlt / 1000f;
            currentThousands = Mathf.Clamp(currentThousands, 0f, 10f);

            float currentHundreds = currentAlt - (Mathf.Floor(currentThousands) * 1000f);
            currentHundreds = Mathf.Clamp(currentHundreds, 0f, 1000f);

            if (thousandsPointer)
            {
                float normalizedThousands = Mathf.InverseLerp(0f, 10f, currentThousands);
                float thousandsRotaiton = 360f * normalizedThousands;
                thousandsPointer.rotation = Quaternion.Euler(0f, 0f, -thousandsRotaiton);
            }

            if (hundredsPointer)
            {
                float normalizedHundreds = Mathf.InverseLerp(0f, 1000f, currentHundreds);
                float hundredsRotaiton = 360f * normalizedHundreds;
                hundredsPointer.rotation = Quaternion.Euler(0f, 0f, -hundredsRotaiton);
            }

        }
    }
    #endregion

    #region Custom Methods
    #endregion
}
