using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add namespace??
public class BaseAirplane_Input : MonoBehaviour
{
    #region Variables
    protected float pitch = 0f;
    protected float roll = 0f;
    protected float yaw = 0f;
    protected float throttle = 0f;
    public KeyCode brakeKey = KeyCode.Space;
    protected float brake = 0f;
    public int maxFlapIncrements = 3;
    protected int flaps = 0;

    #endregion

    #region Properties
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
    #endregion

    #region Builtin Methods
    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update()
    {
        Debug.Log(brake);
    }
    #endregion

    #region Custom Methods
        void HandleInput()
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
        }
    #endregion
}
