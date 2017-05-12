using UnityEngine;
using System.Collections;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "WeaponStock.asset", menuName = "Inventory System/Weapons/Stock", order = 0)]
public class WeaponStockItemTemplate : WeaponPartItemTemplate {
    [Header("Weapon Stock")]
    public BarrelType BarrelType;
}

[Serializable]
public class WeaponStockItem : WeaponPartItem<WeaponStockItemTemplate> { }

