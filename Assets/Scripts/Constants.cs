using System.Collections.Generic;
using UnityEngine;

public class Constants 
{

    public const string volume_level="volume_level";
    public const string sound_effects_level="SE_volume";
    public static readonly Dictionary<string,KeyCode> default_keys=new Dictionary<string,KeyCode>(){
        {"turn_ship_up",KeyCode.W},
        {"turn_ship_down",KeyCode.S},
        {"turn_ship_left",KeyCode.A},
        {"turn_ship_right",KeyCode.D},

        {"accelerate",KeyCode.LeftShift},
        {"deccelerate",KeyCode.LeftControl}
    };
}
