using UnityEngine;
using System.Collections;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "WeaponMagazine.asset", menuName = "Inventory System/Weapons/Magazine", order = 0)]
public class WeaponMagazineItemTemplate : WeaponPartItemTemplate {
    [Header("Weapon Magazine")]
    public int ClipSize;
}

[Serializable]
public class WeaponMagazineItem : WeaponPartItem<WeaponMagazineItemTemplate> { }

