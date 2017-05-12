using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Weapon {

    public WeaponProjectile Projectile;

    public bool IsFiring { get; private set; }
    public bool IsRecharging { get; private set; }
    public int BulletsCount { get; private set; }
    public float RechargeTime { get; private set; }

    public float CooldownTime {
        get {
            return WeaponData.CooldownTime;
        }
    }

    public float NormalizedRechargeTime {
        get {
            return IsRecharging ? _CurrentTimeout / RechargeTime : 0;
        }
    }

    private Transform FirePivot;
    private float _CurrentTimeout;

    public void StartFire() {
        IsFiring = true;
    }

    public void StopFire() {
        IsFiring = false;
    }

    private void UpdateFiring() {
        _CurrentTimeout -= Time.deltaTime;

        if (_CurrentTimeout <= 0) {
            if (IsRecharging) {
                IsRecharging = false;
                BulletsCount = WeaponData.ClipSize;
            }

            if (IsFiring) {
                PerformShot();
                BulletsCount--;
                if (BulletsCount > 0) {
                    _CurrentTimeout = CooldownTime;
                }
                else {
                    IsRecharging = true;
                    _CurrentTimeout = RechargeTime;
                }
            }
            else {
                _CurrentTimeout = 0;
            }
        }
    }

    private void PerformShot() {
        _ProjectilePool.GetProjectile().Fire(FirePivot.position, FirePivot.rotation);
    }

}
