using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Follow_Camera : MonoBehaviour
{
    #region Variables
    [Header("Basic Follow Camera Properties")]
    public Transform target;
    public float distance = 5f;
    public float height = 2f;
    public float smoothSpeed = 0.5f;

    private Vector3 smoothVelocity;
    protected float origHeight;
    #endregion

    #region BuiltIn Methods
    // Start is called before the first frame update
    void Start()
    {
        origHeight = height;
    }

    void FixedUpdate()
    {
        if(target)
        {
            HandleCamera();
        
        }
    }
    #endregion

    #region Custom Methods
    protected virtual void HandleCamera()
    {
        //follow Target
        Vector3 wantedPosition = target.position + (-target.forward * distance) + (Vector3.up * height);
        transform.position = Vector3.SmoothDamp(transform.position, wantedPosition,ref smoothVelocity, 0.5f);

        transform.LookAt(target);
    }
    #endregion
}
