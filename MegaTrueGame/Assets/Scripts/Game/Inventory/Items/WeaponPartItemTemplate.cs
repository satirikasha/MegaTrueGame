using UnityEngine;
using System.Collections;
using System;
using System.Linq;

//[CreateAssetMenu(fileName = "WeaponPart.asset", menuName = "Inventory System/WeaponPart", order = 0)]
public abstract class WeaponPartItemTemplate : InstancedItemTemplate {
    [Header("Weapon Part")]
    public float Damage;
    public float FireRate;
    public float ReloadSpeed;
    public float PushForce;
    public float Precision;
}

[Serializable]
public abstract class WeaponPartItem<T> : InstancedItem<T> where T : WeaponPartItemTemplate { }

