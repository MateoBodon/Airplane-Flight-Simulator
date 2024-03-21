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
    public float rbLerpSpeed = 0.01f;

    [Header("Lift Properties")]
    public float maxLiftPower = 800f; 
    public AnimationCurve liftCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Header("Drag Properties")]
    public float dragFactor = 0.01f;

    [Header("Control Properties")]
    public float pitchSpeed = 1000f;
    public float rollSpeed = 1000f;
    public float yawSpeed = 1000f;

    private BaseAirplane_Input input;
    private Rigidbody rb;
    private float startDrag;
    private float startAngularDrag;

    private float maxMPS;
    private float normalizeMPH;

    private float angleOfAttack;
    private float pitchAngle;
    private float rollAngle;
    #endregion

    #region Constants
    const float mpsToMph = 2.23694f;
    #endregion

    #region Builtin Methods
    #endregion

    #region Custom Methods
    public void InitCharacteristics(Rigidbody curRB, BaseAirplane_Input curInput)
    {
        //Basic Initialization
        input = curInput;
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
            HandlePitch();
            HandleRoll();
            HandleYaw();
            HandleBanking();

            HandleRigidBodyTransform();
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
        //Get the Angle of Attack
        angleOfAttack = Vector3.Dot(rb.velocity.normalized, transform.forward);
        angleOfAttack *= angleOfAttack;

        //Create the Lift Direction
        Vector3 liftDir = transform.up;
        float liftPower = liftCurve.Evaluate(normalizeMPH) * maxLiftPower;

        //Apply the final Lift Force to the Rigidbody
        Vector3 finalLiftForce = liftDir * liftPower * angleOfAttack;
        rb.AddForce(finalLiftForce);
    }

    void CalculateDrag()
    {
        //Speed Drag
        float speedDrag = forwardSpeed * dragFactor;

        float finalDrag = startDrag + speedDrag;

        rb.drag = finalDrag;
        rb.angularDrag = startAngularDrag * forwardSpeed;
    }

    void HandleRigidBodyTransform()
    {
        if (rb.velocity.magnitude > 1f)
        {
            Vector3 updatedVelocity = Vector3.Lerp(rb.velocity, transform.forward * forwardSpeed, forwardSpeed * angleOfAttack * Time.deltaTime * rbLerpSpeed);
            rb.velocity = updatedVelocity;

            Quaternion updatedRotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(rb.velocity.normalized, transform.up), forwardSpeed * Time.deltaTime * rbLerpSpeed);
            rb.MoveRotation(updatedRotation);
        }
    }

    void HandlePitch()
    {
        Vector3 flatForward = transform.forward;
        flatForward.y = 0f;
        flatForward = flatForward.normalized;
        pitchAngle = Vector3.Angle(transform.forward, flatForward);

        Vector3 pitchTorque = input.Pitch * pitchSpeed * transform.right;
        rb.AddTorque(pitchTorque);


    }

    void HandleRoll()
    {
        Vector3 flatRight = transform.right;
        flatRight.y = 0f;
        flatRight = flatRight.normalized;
        rollAngle = Vector3.SignedAngle(transform.right, flatRight, transform.forward);

        Vector3 rollTorque = -input.Roll * rollSpeed * transform.forward;
        rb.AddTorque(rollTorque);
    }

    void HandleYaw()
    {
        Vector3 yawTorque = input.Yaw * yawSpeed * transform.up;
        rb.AddTorque(yawTorque);
    }

    void HandleBanking()
    {
        float bankSide = Mathf.InverseLerp(-90f, 90f, rollAngle);
        float bankAmount = Mathf.Lerp(-1f, 1f, bankSide);
        Vector3 bankTorque = bankAmount * rollSpeed * transform.up;
        rb.AddTorque(bankTorque);
    }
    #endregion
}
