using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class BaseRigidbody_Controller : MonoBehaviour
{
    #region Variables
    protected Rigidbody rb;
    protected AudioSource aSource;
    #endregion

    #region Builtin Methods
    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        aSource = GetComponent<AudioSource>();
        if (aSource)
        {
            aSource.playOnAwake = false;
        }
    }

    void FixedUpdate()
    {
        if(rb)
        {
            HandlePhysics();
        }
        
    }
    #endregion

    #region Custom Methods
    protected virtual void HandlePhysics()
    {
        //Handle Physics
    }
    #endregion
}
