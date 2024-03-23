using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(Airplane_Controller))]
public class AirplaneController_Editor : Editor
{
    #region Variables
    private Airplane_Controller targetController;
    #endregion

    #region Builtin Methods
    void OnEnable()
    {
        targetController = (Airplane_Controller)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(15);
        if (GUILayout.Button("Get Airplane Components", GUILayout.Height(35)))
        {
            //Find All Engines
            targetController.engines.Clear();
            targetController.engines = FindAllEngines().ToList();

            //Find All Wheels
            targetController.wheels.Clear();
            targetController.wheels = FindAllWheels().ToList();

            //Find All Control Surfaces
            targetController.controlSurfaces.Clear();
            targetController.controlSurfaces = FindAllControlSurfaces().ToList();
        }
        GUILayout.Space(15);

        if (GUILayout.Button("Create Airplane Preset", GUILayout.Height(35)))
        {
            string filePath = EditorUtility.SaveFilePanel("Save Airplane Preset", "Assets", "New Airplane Preset", "asset");
            SaveAirplanePreset(filePath);
        }
    }
    #endregion

    #region Custom Methods
    Airplane_Engine[] FindAllEngines()
    {
        Airplane_Engine[] engines = new Airplane_Engine[0];
        if (targetController)
        {
            engines = targetController.GetComponentsInChildren<Airplane_Engine>(true);
        }
        return engines;
    }

    Airplane_Wheel[] FindAllWheels()
    {
        Airplane_Wheel[] wheels = new Airplane_Wheel[0];
        if (targetController)
        {
            wheels = targetController.GetComponentsInChildren<Airplane_Wheel>(true);
        }
        return wheels;
    }

    Airplane_ControlSurface[] FindAllControlSurfaces()
    {
        Airplane_ControlSurface[] controlSurfaces = new Airplane_ControlSurface[0];
        if (targetController)
        {
            controlSurfaces = targetController.GetComponentsInChildren<Airplane_ControlSurface>(true);
        }
        return controlSurfaces;
    }

    void SaveAirplanePreset(string aPath)
    {
        if(targetController && !string.IsNullOrEmpty(aPath))
        {
            string appPath = Application.dataPath;

            string finalPath = "Assets" + aPath.Substring(appPath.Length);

            //Create new Preset
            Airplane_Preset newPreset = ScriptableObject.CreateInstance<Airplane_Preset>();
            newPreset.airplaneWeight = targetController.airplaneWeight;
            if(targetController.centerOfGravity)
            {
                newPreset.cogPosition = targetController.centerOfGravity.localPosition;
            }

            if(targetController.characteristics)
            {
                newPreset.maxMPH = targetController.characteristics.maxMPH;
                newPreset.rbLerpSpeed = targetController.characteristics.rbLerpSpeed;
                newPreset.maxLiftPower = targetController.characteristics.maxLiftPower;
                newPreset.liftCurve = targetController.characteristics.liftCurve;
                newPreset.dragFactor = targetController.characteristics.dragFactor;
                newPreset.flapDragFactor = targetController.characteristics.flapDragFactor;
                newPreset.pitchSpeed = targetController.characteristics.pitchSpeed;
                newPreset.rollSpeed = targetController.characteristics.rollSpeed;
                newPreset.yawSpeed = targetController.characteristics.yawSpeed;
            }

            //Create Final Presett
            AssetDatabase.CreateAsset(newPreset, finalPath);
        }
    }
    #endregion
}
