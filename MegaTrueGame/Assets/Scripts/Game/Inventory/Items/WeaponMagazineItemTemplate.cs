using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public enum BulletType {
    Normal,
    Piercing,
    Cluster,
    Aiming
}

[CreateAssetMenu(fileName = "WeaponMagazine.asset", menuName = "Inventory System/Weapons/Magazine", order = 0)]
public class WeaponMagazineItemTemplate : WeaponPartItemTemplate {
    [Header("Weapon Magazine")]
    public BulletType BulletType;
    public int ClipSize;
}

[Serializable]
public class WeaponMagazineItem : WeaponPartItem<WeaponMagazineItemTemplate> { }

