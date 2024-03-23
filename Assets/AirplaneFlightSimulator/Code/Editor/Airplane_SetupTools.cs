using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class Airplane_SetupTools 
{
    public static void BuildDefaultAirplane(string aName)
    {
        //Create the root GO
        GameObject rootGO = new GameObject(aName, typeof(Airplane_Controller), typeof(BaseAirplane_Input));

        //Create the Center of Gravity
        GameObject cogGO = new GameObject("COG");
        cogGO.transform.SetParent(rootGO.transform, false);

        //Create the Base Components or Find Them
        BaseAirplane_Input input = rootGO.GetComponent<BaseAirplane_Input>();
        Airplane_Controller controller = rootGO.GetComponent<Airplane_Controller>();
        Airplane_Characteristics characteristics = rootGO.GetComponent<Airplane_Characteristics>();

        //Setup the Airplane
        if (controller)
        {
            //Assign core components
            controller.input = input;
            controller.characteristics = characteristics;
            controller.centerOfGravity = cogGO.transform;

            //Create structure
            GameObject graphicsGrp = new GameObject("Graphics_GRP");
            GameObject collisionGrp = new GameObject("Collision_GRP");
            GameObject controlSurfacesGrp = new GameObject("ControlSurfaces_GRP");

            graphicsGrp.transform.SetParent(rootGO.transform, false);
            collisionGrp.transform.SetParent(rootGO.transform, false);
            controlSurfacesGrp.transform.SetParent(rootGO.transform, false);

            //Create First Engine
            GameObject engineGO = new GameObject("Engine", typeof(Airplane_Engine));
            Airplane_Engine engine = engineGO.GetComponent<Airplane_Engine>();
            controller.engines.Add(engine);
            engineGO.transform.SetParent(rootGO.transform, false);

            //Create the base Airplane
            GameObject defaultAirplane = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AirplaneFlightSimulator/Art/Objects/IndiePixel_Airplanes/Indie-Pixel_Airplane/IndiePixel_Airplane.fbx");
            if(defaultAirplane)
            {
                GameObject.Instantiate(defaultAirplane, graphicsGrp.transform);
            }
        }

        //Select the Airplane Setup
        Selection.activeGameObject = rootGO;

    }
}
