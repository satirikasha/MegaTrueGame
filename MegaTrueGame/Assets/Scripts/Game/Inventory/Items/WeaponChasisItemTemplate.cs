using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public enum WeaponType {
    Bullet,
    Rocket,
    Plasma,
    Laser
}

[CreateAssetMenu(fileName = "WeaponChasis.asset", menuName = "Inventory System/Weapons/Chasis", order = 0)]
public class WeaponChasisItemTemplate : WeaponPartItemTemplate {
    [Header("Weapon Chasis")]
    public WeaponType WeaponType;
    public float Damage;
    public float FireRate;
    public float RechargeSpeed;
    public float ProjectileSpeed;
    public float PushForce;
    public float Precision;
}

[Serializable]
public class WeaponChasisItem : WeaponPartItem<WeaponChasisItemTemplate> { }

