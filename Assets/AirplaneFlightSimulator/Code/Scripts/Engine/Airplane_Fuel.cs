using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane_Fuel : MonoBehaviour
{
    #region Variables
    [Header("Fuel Properties")]
    [Tooltip("The total number of gallons in the fuel tank.")]
    public float fuelCapacity = 26f;
    [Tooltip("The average fuel burn rate in gallons per hour.")]
    public float fuelBurnRate = 6.1f; 
    #endregion

    #region Properties
    private float currentFuel;
    public float CurrentFuel
    {
        get { return currentFuel; }
    }

    private float normalizedFuel;
    public float NormalizedFuel
    {
        get { return normalizedFuel; }
    }
    #endregion

    #region Custom Methods
    public void InitFuel()
    {
        currentFuel = fuelCapacity;
    }
    public void UpdateFuel(float aPercentage)
    {
        print("Fuel Percentage: " + aPercentage);
        float currentBurn = (fuelBurnRate * aPercentage) / 3600; //Convert to seconds
        print("Current Burn: " + currentBurn);
        currentFuel -= currentBurn;
        currentFuel = Mathf.Clamp(currentFuel, 0f, fuelCapacity);

        normalizedFuel = currentFuel / fuelCapacity;
    }
    #endregion
}
