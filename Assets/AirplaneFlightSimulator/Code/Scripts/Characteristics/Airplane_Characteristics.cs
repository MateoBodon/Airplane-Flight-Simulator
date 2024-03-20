using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane_Characteristics : MonoBehaviour
{
    #region Variables
    [Header("Characteristic Properties")]
    public float forwardSpeed;
    public float mph;
    public float maxMPH = 110f;

    [Header("Lift Properties")]
    public float maxLiftPower = 800f; 
    public AnimationCurve liftCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Header("Drag Properties")]
    public float dragFactor = 0.01f;

    private Rigidbody rb;
    private float startDrag;
    private float startAngularDrag;

    private float maxMPS;
    private float normalizeMPH;
    #endregion

    #region Constants
    const float mpsToMph = 2.23694f;
    #endregion

    #region Builtin Methods
    #endregion

    #region Custom Methods
    public void InitCharacteristics(Rigidbody curRB)
    {
        //Basic Initialization
        rb = curRB;
        startDrag = rb.drag;
        startAngularDrag = rb.angularDrag;

        //Find the max Meters per Second
        maxMPS = maxMPH / mpsToMph;
    }

    public void UpdateCharacteristics()
    {
        if(rb)
        {
            //Process the Flight Model
            CalculateForwardSpeed();
            CalculateLift();
            CalculateDrag();
        }
    }

    void CalculateForwardSpeed()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        forwardSpeed = localVelocity.z;
        forwardSpeed = Mathf.Clamp(forwardSpeed, 0f, maxMPS);

        mph = forwardSpeed * mpsToMph;
        mph = Mathf.Clamp(mph, 0f, maxMPH);
        normalizeMPH = Mathf.InverseLerp(0f, maxMPH, mph);
    }

    void CalculateLift()
    {
        Vector3 liftDir = transform.up;
        float liftPower = liftCurve.Evaluate(normalizeMPH) * maxLiftPower;

        Vector3 finalLiftForce = liftDir * liftPower;
        rb.AddForce(finalLiftForce);
    }

    void CalculateDrag()
    {
        float speedDrag = forwardSpeed * dragFactor;
        float finalDrag = startDrag + speedDrag;

        rb.drag = finalDrag;
        rb.angularDrag = startAngularDrag * forwardSpeed;
    }
    #endregion
}
