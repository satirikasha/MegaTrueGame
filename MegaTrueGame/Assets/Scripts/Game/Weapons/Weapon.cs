using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public int ClipSize;
    public float CooldownTime;
    public float RechargeTime;
    public Transform FirePivot;
    public Projectile Projectile;

    public bool IsFiring { get; private set; }
    public bool IsRecharging { get; private set; }
    public int BulletsCount { get; private set; }

    private float _CurrentTimeout;

    protected virtual void Awake() {
        BulletsCount = ClipSize;
        RechargeTime = Mathf.Max(RechargeTime, CooldownTime);
    }

    void Update() {
        UpdateFiring();
    }

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
                BulletsCount = ClipSize;
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

    protected virtual void PerformShot() {
        var projectile = Instantiate(Projectile);
        projectile.transform.position = FirePivot.transform.position;
        projectile.transform.rotation = FirePivot.transform.rotation;
    }

}
