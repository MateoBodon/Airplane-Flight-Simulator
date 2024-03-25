using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AirplaneUI_Controller : MonoBehaviour
{
    #region Variables
    public List<IAirplaneUI> instruments = new List<IAirplaneUI>();
    #endregion

    #region Builtin Methods
    // Start is called before the first frame update
    void Start()
    {
        instruments = GetComponentsInChildren<IAirplaneUI>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (IAirplaneUI instrument in instruments)
        {
            instrument.HandleAirplaneUI();
        }
    }
    #endregion
}
