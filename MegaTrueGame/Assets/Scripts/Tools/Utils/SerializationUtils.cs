﻿using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;


public static partial class Utils {

    public static float GetFloat(this SerializationInfo info, string name) {
        return (float)info.GetValue(name, typeof(float));
    }
}
