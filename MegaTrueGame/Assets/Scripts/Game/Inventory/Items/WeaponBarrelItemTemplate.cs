using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public enum BarrelType {
    Normal,
    Short,
    Scatter,
    Sniper
}

[CreateAssetMenu(fileName = "WeaponBarrel.asset", menuName = "Inventory System/Weapons/Barrel", order = 0)]
public class WeaponBarrelItemTemplate : WeaponPartItemTemplate {
    [Header("Weapon Barrel")]
    public BarrelType BarrelType;
}

[Serializable]
public class WeaponBarrelItem : WeaponPartItem<WeaponBarrelItemTemplate> { }

