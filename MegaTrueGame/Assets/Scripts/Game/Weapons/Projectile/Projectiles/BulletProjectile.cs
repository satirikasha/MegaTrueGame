using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : WeaponProjectile {

    protected override void Update() {
        base.Update();
        this.transform.position += this.transform.forward * 35 * Time.deltaTime;
    }
}
