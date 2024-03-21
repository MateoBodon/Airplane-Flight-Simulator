using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane_GroundEffect : MonoBehaviour
{
    #region Variables
    public float maxGroundDistance = 3f;
    public float liftForce = 100f;
    public float maxSpeed = 15f;

    private Rigidbody rb;
    #endregion

    #region Builtin Methods
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb)
        {
            HandleGroundEffect();
        }
    }
    #endregion

    #region Custom Methods
    protected virtual void HandleGroundEffect()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.transform.tag == "Ground" && hit.distance < maxGroundDistance)
            {
                float currentSpeed = rb.velocity.magnitude;
                float normalizedSpeed = currentSpeed / maxSpeed;
                normalizedSpeed = Mathf.Clamp01(normalizedSpeed);


                float distance = maxGroundDistance - hit.distance;
                float finalForce = distance * liftForce * normalizedSpeed;
                rb.AddForce(Vector3.up * finalForce);
            }
        }
    }
    #endregion
}
