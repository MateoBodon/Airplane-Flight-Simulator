using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane_Engine : MonoBehaviour
{
    #region Variables
    [Header("Engine Properties")]
    public float maxForce = 200f;
    public float maxRPM = 2550f;

    public AnimationCurve powerCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    [Header("Propeller")]
    public List<Airplane_Propeller> propellers = new List<Airplane_Propeller>();    
    #endregion

    #region Buildin Methods
    #endregion

    #region Custom Methods
    public Vector3 CalculateForce(float throttle)
    {
        //Calculate Power
        float finalThrottle = Mathf.Clamp01(throttle);
        finalThrottle = powerCurve.Evaluate(finalThrottle);

        //Calculate RPM's
        float currentRPM = maxRPM * finalThrottle;

        //Handle Propellers
        if (propellers != null)
        {
            if (propellers.Count > 0)
            {
                foreach (Airplane_Propeller prop in propellers)
                {
                    prop.HandlePropeller(currentRPM);
                }
            }
        }

        //Create Force
        float finalPower = maxForce * finalThrottle;
        Vector3 finalForce = transform.forward * finalPower;

        return finalForce;
    }
    #endregion
}
