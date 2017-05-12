using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponProjectile : Projectile {

    public WeaponData WeaponData { get; private set; }

    public void Initialize(WeaponData data) {
        WeaponData = data;
    }
}
