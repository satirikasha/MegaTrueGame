using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public abstract class WeaponPartItemTemplate : InstancedItemTemplate {
}

[Serializable]
public abstract class WeaponPartItem<T> : InstancedItem<T> where T : WeaponPartItemTemplate { }

