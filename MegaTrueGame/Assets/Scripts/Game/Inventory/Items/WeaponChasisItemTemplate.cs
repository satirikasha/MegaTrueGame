using UnityEngine;
using System.Collections;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "WeaponChasis.asset", menuName = "Inventory System/Weapons/Chasis", order = 0)]
public class WeaponChasisItemTemplate : WeaponPartItemTemplate {
    [Header("Weapon Chasis")]
    public System.Reflection.BindingFlags WeaponType;
}

[Serializable]
public class WeaponChasisItem : WeaponPartItem<WeaponChasisItemTemplate> { }

