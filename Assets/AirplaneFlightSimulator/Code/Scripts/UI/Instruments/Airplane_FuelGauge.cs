using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane_FuelGauge : MonoBehaviour, IAirplaneUI
{
    #region Variables
    [Header("Fuel Gauge Properties")]
    public RectTransform pointer;
    public Airplane_Fuel fuel;
    public Vector2 minMaxRotation = new Vector2(-90f, 90f);
    #endregion

    #region Custom Methods
    public void HandleAirplaneUI()
    {
        if(fuel && pointer)
        {
            float wantedRotation = Mathf.Lerp(minMaxRotation.x, minMaxRotation.y, fuel.NormalizedFuel);
            pointer.rotation = Quaternion.Euler(0f, 0f, wantedRotation);
        }
    }
    #endregion
}
