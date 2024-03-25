using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add namespace??
public class BaseAirplane_Input : MonoBehaviour
{
    #region Variables
    public float throttleSpeed = 0.1f;

    protected float stickyThrottle;
    protected float pitch = 0f;
    protected float roll = 0f;
    protected float yaw = 0f;
    protected float throttle = 0f;

    [SerializeField]
    private KeyCode brakeKey = KeyCode.Space;
    protected float brake = 0f;

    [SerializeField]
    protected KeyCode cameraKey = KeyCode.C;
    protected bool cameraSwitch = false;

    [SerializeField]
    public int maxFlapIncrements = 3;
    protected int flaps = 0;

    #endregion

    #region Properties
    public float StickyThrottle
    {
        get{return stickyThrottle;}
    }
    public float Pitch
    {
        get{return pitch;}
    }
    public float Roll
    {
        get{return roll;}
    }
    public float Yaw
    {
        get{return yaw;}
    }
    public float Throttle
    {
        get{return throttle;}
    }
    public int Flaps
    {
        get{return flaps;}
    }
    public float Brake
    {
        get{return brake;}
    }

    public bool CameraSwitch
    {
        get{return cameraSwitch;}
    }
    #endregion

    #region Builtin Methods
    // Update is called once per frame
    void Update()
    {
        HandleInput();
        StickyThrottleControl();
        ClamInputs();
    }
    #endregion

    #region Custom Methods
        protected virtual void HandleInput()
        {
            //Process Main Control Input
            pitch = Input.GetAxis("Vertical");
            roll = Input.GetAxis("Horizontal");
            yaw = Input.GetAxis("Yaw");
            throttle = Input.GetAxis("Throttle");

            //Process Brake Inputs
            brake = Input.GetKey(brakeKey)? 1f : 0f;

            //Process Flap Inputs
            if(Input.GetKeyDown(KeyCode.F))
            {
                flaps += 1;
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                flaps -= 1;
            }

            flaps = Mathf.Clamp(flaps, 0, maxFlapIncrements);

            //Camera Switch Key
            cameraSwitch = Input.GetKeyDown(cameraKey);
        }

        void StickyThrottleControl()
        {
            stickyThrottle = stickyThrottle + (throttle * throttleSpeed * Time.deltaTime);
            stickyThrottle = Mathf.Clamp01(stickyThrottle);
        }

        protected void ClamInputs()
        {
            pitch = Mathf.Clamp(pitch, -1f, 1f);
            roll = Mathf.Clamp(roll, -1f, 1f);
            yaw = Mathf.Clamp(yaw, -1f, 1f);
            throttle = Mathf.Clamp(throttle, -1f, 1f);
        }
    #endregion
}
