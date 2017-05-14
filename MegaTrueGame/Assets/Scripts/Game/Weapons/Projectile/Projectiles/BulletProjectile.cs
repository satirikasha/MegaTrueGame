using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : WeaponProjectile {

    private Vector3 _TargetPosition;
    private RaycastHit _Hit;
    private ParticleEffect _BulletSparks;

    protected override void OnEnable() {
        base.OnEnable();
        _TargetPosition = this.transform.position;
    }

    protected override void Update() {
        base.Update();
        _TargetPosition = this.transform.position + this.transform.forward * WeaponData.ProjectileSpeed * Time.deltaTime;

        if (Physics.Raycast(this.transform.position, _TargetPosition - this.transform.position, out _Hit, WeaponData.ProjectileSpeed * Time.deltaTime)) {
            _BulletSparks = VisualEffect.GetEffect<ParticleEffect>("BulletSparks");
            _BulletSparks.transform.position = this.transform.position;
            _BulletSparks.transform.forward = _Hit.normal;
            _BulletSparks.Play();
            this.gameObject.SetActive(false);
        }

        this.transform.position = _TargetPosition;
    }
}
