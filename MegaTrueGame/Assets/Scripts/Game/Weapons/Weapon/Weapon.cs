using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Weapon : MonoBehaviour {

    public const float StandardRechargeTime = 5;
    public const float StandardCooldownTime = 1;

    private ProjectilePool _ProjectilePool;

    void Start() {
        Rebuild();
        BulletsCount = WeaponData.ClipSize;
        RechargeTime = Mathf.Max(WeaponData.RechargeTime, CooldownTime);
        _ProjectilePool = new GameObject("ProjectilePool", typeof(ProjectilePool)).GetComponent<ProjectilePool>();
        _ProjectilePool.Owner = this;
    }

    void Update () {
        UpdateFiring();
    }
}
