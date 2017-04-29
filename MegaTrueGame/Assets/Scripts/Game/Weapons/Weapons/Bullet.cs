using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile {

    public float Speed;

    protected Rigidbody Rigidbody { get; set; }

    void Awake() {
        Rigidbody = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        Rigidbody.velocity = this.transform.forward * Speed;
    }
}
