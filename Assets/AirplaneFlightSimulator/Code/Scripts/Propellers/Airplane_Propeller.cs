using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane_Propeller : MonoBehaviour
{
    #region Variables
    [Header("Propeller Properties")]
    public float minQuadRPMs = 300f;
    public float minTextureSwampRPMs = 600f;
    public GameObject mainProp;
    public GameObject blurredProp;

    [Header("Material Properties")]
    public Material blurredPropMat;
    public Texture2D blurLevel1;
    public Texture2D blurLevel2;
    #endregion

    #region Builtin Methods
    void Start()
    {
        if (mainProp && blurredProp)
        {
            HandleSwapping(0f);
        }
    }
    #endregion

    #region Custom Methods
    public void HandlePropeller(float currentRPM)
    {
        //Get degrees per second
        float dps = currentRPM * 6f * Time.deltaTime;

        //Rotate the Propeller Group
        transform.Rotate(Vector3.forward, dps);

        //Handle Propeller Swapping
        if (mainProp && blurredProp)
        {
            HandleSwapping(currentRPM);
        }
        
    }

    void HandleSwapping(float currentRPM)
    {
        if (currentRPM > minQuadRPMs)
        {
            mainProp.SetActive(false);
            blurredProp.SetActive(true);

            if (blurredPropMat && blurLevel1 && blurLevel2)
            {
                if (currentRPM > minTextureSwampRPMs)
                {
                    blurredPropMat.SetTexture("_MainTex", blurLevel1);
                }
                else
                {
                    blurredPropMat.SetTexture("_MainTex", blurLevel2);
                }
            }
        
        }
        else
        {
            mainProp.SetActive(true);
            blurredProp.SetActive(false);
        }
    }
    #endregion
}
