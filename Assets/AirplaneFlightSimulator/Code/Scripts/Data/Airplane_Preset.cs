using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Airplane Preset", menuName = "Airplane Tools/Crete Airplane Preset")]
public class Airplane_Preset : ScriptableObject
{
    #region Controller Properties
    [Header("Controller Properties")]
    public Vector3 cogPosition;
    public float airplaneWeight;
    #endregion

    #region Characteristic Properties
    [Header("Characteristic Properties")]
    public float maxMPH;
    public float rbLerpSpeed;
    public float maxLiftPower; 
    public AnimationCurve liftCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public float dragFactor;
    public float flapDragFactor;
    public float pitchSpeed;
    public float rollSpeed;
    public float yawSpeed;
    #endregion
}
