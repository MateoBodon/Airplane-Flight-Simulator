using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XboxAirplane_Input : BaseAirplane_Input
{
    protected override void HandleInput()
    {
        //Process Main Control Input
            pitch = Input.GetAxis("Vertical");
            roll = Input.GetAxis("Horizontal");
            yaw = Input.GetAxis("X_RH_Stick");
            throttle = Input.GetAxis("X_RV_Stick");

            //Process Brake Inputs
            brake = Input.GetAxis("Fire1");

            //Process Flap Inputs
            if(Input.GetKeyDown("X_R_Bumper"))
            {
                flaps += 1;
            }
            if (Input.GetKeyDown("X_L_Bumper"))
            {
                flaps -= 1;
            }

            flaps = Mathf.Clamp(flaps, 0, maxFlapIncrements);
    }
}
