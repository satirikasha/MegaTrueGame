using UnityEngine;
using System.Collections;
using System;
using System.Linq;

[Flags]
public enum BulletType {
    Piercing = 1 << 0,
    Bouncing = 1 << 1,
    Cluster = 1 << 2,
    Aiming = 1 << 3
}

[CreateAssetMenu(fileName = "WeaponMagazine.asset", menuName = "Inventory System/Weapons/Magazine", order = 0)]
public class WeaponMagazineItemTemplate : WeaponPartItemTemplate {
    [Header("Weapon Magazine")]
    public BulletType BulletType;
    public int ClipSize;
}

[Serializable]
public class WeaponMagazineItem : WeaponPartItem<WeaponMagazineItemTemplate> { }

