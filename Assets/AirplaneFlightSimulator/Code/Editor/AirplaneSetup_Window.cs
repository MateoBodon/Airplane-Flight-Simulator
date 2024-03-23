using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AirplaneSetup_Window : EditorWindow
{
    #region Variables
    private string wantedName;
    #endregion

    #region Builtin Methods
    public static void LaunchSetupWindow()
    {
        AirplaneSetup_Window.GetWindow(typeof(AirplaneSetup_Window), true, "Airplane Setup").Show();
    }

    void OnGUI()
    {
        wantedName = EditorGUILayout.TextField("Airplane Name:", wantedName);
        if (GUILayout.Button("Create New Airplane"))
        {
            Airplane_SetupTools.BuildDefaultAirplane(wantedName);
            this.Close();
        }
    }
    #endregion

    #region Custom Methods
    #endregion
}
