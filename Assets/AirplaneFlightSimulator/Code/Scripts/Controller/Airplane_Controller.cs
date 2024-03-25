using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Airplane_Characteristics))]
public class Airplane_Controller : BaseRigidbody_Controller
{
    #region Variables
    [Header("Base Airplane Properties")]
    public Airplane_Preset airplanePreset;
    public BaseAirplane_Input input;
    public Airplane_Characteristics characteristics;
    public Transform centerOfGravity;

    [Tooltip("Weight is in LBS")]
    public float airplaneWeight = 800f;

    [Header("Engines")]
    public List<Airplane_Engine> engines = new List<Airplane_Engine>();

    [Header("Wheels")]
    public List<Airplane_Wheel> wheels = new List<Airplane_Wheel>();

    [Header("Control Surfaces")]
    public List<Airplane_ControlSurface> controlSurfaces = new List<Airplane_ControlSurface>();
    #endregion

    #region Properties
    private float currentMSL;
    public float CurrentMSL
    {
        get{return currentMSL;}
    }

    private float currentAGL;
    public float CurrentAGL
    {
        get{return currentAGL;}
    }
    #endregion

    #region Constants
    const float poundsToKilos = 0.453592f;
    const float metersToFeet = 3.28084f;
    #endregion

    #region Builtin Methods
    public override void Start()
    {
        GetPresetInfo();

        base.Start();

        float finalMass = airplaneWeight * poundsToKilos;
        if (rb)
        {
            rb.mass = finalMass;
            if (centerOfGravity)
            {
                rb.centerOfMass = centerOfGravity.localPosition;
            }

            characteristics = GetComponent<Airplane_Characteristics>();
            if(characteristics)
            {
                characteristics.InitCharacteristics(rb, input);
            }
        }

        if(wheels != null)
        {
            if(wheels.Count > 0)
            {
                foreach(Airplane_Wheel wheel in wheels)
                {
                    wheel.InitWheel();
                }
            }
        }

    }
    #endregion

    #region Custom Methods
    protected override void HandlePhysics()
    {
        if (input)
        {
            HandleEngines();
            HandleCharacteristics();
            HandleControlSurfaces();
            HandleWheel();
            HandleAltitude();
        }
        
    }

    void HandleEngines()
    {
        if(engines != null)
        {
            if(engines.Count > 0)
            {
                foreach(Airplane_Engine engine in engines)
                {
                    rb.AddForce(engine.CalculateForce(input.StickyThrottle));
                }
            }
        }
    }

    void HandleCharacteristics()
    {
       if(characteristics)
       {
           characteristics.UpdateCharacteristics();
       }
    }

    void HandleControlSurfaces()
    {
        if(controlSurfaces.Count > 0)
        {
            foreach(Airplane_ControlSurface controlSurface in controlSurfaces)
            {
                controlSurface.HandleControlSurface(input);
            }
        }
    }

    void HandleWheel()
    {
        if(wheels.Count > 0)
        {
            foreach(Airplane_Wheel wheel in wheels)
            {
                wheel.HandleWheel(input);
            }
        }
    }
    
    void HandleAltitude()
    {
        currentMSL = transform.position.y * metersToFeet;
        
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.transform.tag == "Ground")
            {
                currentAGL = hit.distance * metersToFeet;
            }
        }
    }

    void GetPresetInfo()
    {
        if(airplanePreset)
        {
            airplaneWeight = airplanePreset.airplaneWeight;
            centerOfGravity.localPosition = airplanePreset.cogPosition;

            if(characteristics)
            {
                characteristics.maxMPH = airplanePreset.maxMPH;
                characteristics.rbLerpSpeed = airplanePreset.rbLerpSpeed;
                characteristics.maxLiftPower = airplanePreset.maxLiftPower;
                characteristics.liftCurve = airplanePreset.liftCurve;
                characteristics.dragFactor = airplanePreset.dragFactor;
                characteristics.flapDragFactor = airplanePreset.flapDragFactor;
                characteristics.pitchSpeed = airplanePreset.pitchSpeed;
                characteristics.rollSpeed = airplanePreset.rollSpeed;
                characteristics.yawSpeed = airplanePreset.yawSpeed;
            }
        }
    }
    #endregion
}
