using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Airplane_EngineCuttoff : MonoBehaviour
{
    #region Variables
    [Header("Engine Cuttoff Properties")]
    public KeyCode cutoffKey = KeyCode.O;
    public UnityEvent onEngineCuttoff = new UnityEvent();
    #endregion

    #region Builtin Methods
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(cutoffKey))
        {
           HandleEngineCutoff(); 
        }
    }
    #endregion

    #region Custom Methods
    void HandleEngineCutoff()
    {
        if(onEngineCuttoff != null)
        {
            onEngineCuttoff.Invoke();
        }
    }
    #endregion
}
