using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane_Engine : MonoBehaviour
{
    #region Variables
    [Header("Engine Properties")]
    public float maxForce = 200f;
    public float maxRPM = 2550f;
    public float shutoffSpeed = 2f;

    public AnimationCurve powerCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    [Header("Propeller")]
    public List<Airplane_Propeller> propellers = new List<Airplane_Propeller>();

    private bool isShuttoff = false;
    private float lastThrottleValue;
    private float finalShutoffThrottleValue;    
    #endregion

    #region Properties
    private float currentRPM;
    public float CurrentRPM
    {
        get { return currentRPM; }
    }
    #endregion

    #region Properties
    public bool ShutEngineOff
    {
        set { isShuttoff = value; }
    }
    #endregion

    #region Custom Methods
    public Vector3 CalculateForce(float throttle)
    {
        //Calculate Power
        float finalThrottle = Mathf.Clamp01(throttle);
        
        if (!isShuttoff)
        {
            finalThrottle = powerCurve.Evaluate(finalThrottle);
            lastThrottleValue = finalThrottle;
        }
        else
        {
            lastThrottleValue -= Time.deltaTime * shutoffSpeed;
            lastThrottleValue = Mathf.Clamp01(lastThrottleValue);
            finalShutoffThrottleValue = powerCurve.Evaluate(lastThrottleValue);
            finalThrottle = finalShutoffThrottleValue;
        }

        

        //Calculate RPM's
        currentRPM = maxRPM * finalThrottle;

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
