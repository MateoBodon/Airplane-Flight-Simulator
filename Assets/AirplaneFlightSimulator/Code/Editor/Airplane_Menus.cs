using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class Airplane_Menus
{
    [MenuItem("Airplane Tools/Create New Airplane")]
    public static void CreateNewAirplane()
    {
        // Airplane_SetupTools.BuildDefaulttAirplane("New Airplane");
        AirplaneSetup_Window.LaunchSetupWindow();
    }
}
