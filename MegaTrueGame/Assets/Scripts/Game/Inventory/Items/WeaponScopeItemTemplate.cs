using UnityEngine;
using System.Collections;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "WeaponScope.asset", menuName = "Inventory System/Weapons/Scope", order = 0)]
public class WeaponScopeItemTemplate : WeaponPartItemTemplate {
    [Header("Weapon Scope")]
    public float AimOffset;
    public float AimAssistance;
}

[Serializable]
public class WeaponScopeItem : WeaponPartItem<WeaponScopeItemTemplate> { }

