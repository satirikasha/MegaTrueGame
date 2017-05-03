using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile {

    public float Speed;

    protected Rigidbody Rigidbody { get; set; }

    private bool _ApplyVelocity;

    void Awake() {
        Rigidbody = this.GetComponent<Rigidbody>();
        _ApplyVelocity = true;
    }

    void FixedUpdate() {
        if (_ApplyVelocity) {
            Rigidbody.velocity = this.transform.forward * Speed;
        }
    }

    void OnCollisionEnter(Collision collision) {
        Destroy(this.gameObject);
        _ApplyVelocity = false;
    }
}
