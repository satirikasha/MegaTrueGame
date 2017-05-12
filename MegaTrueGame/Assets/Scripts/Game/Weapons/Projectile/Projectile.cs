using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

    public float LifeTime = 1;

    public float NormalizedLifeTime {
        get {
            return (Time.time - _BirthTimeStamp) / LifeTime;
        }
    }

    private float _BirthTimeStamp;

    protected virtual void OnEnable() {
        _BirthTimeStamp = Time.time;
    }

    protected virtual void Update() {
        if (NormalizedLifeTime >= 1)
            this.gameObject.SetActive(false);
    }

    protected virtual void OnDisable() {

    }

    public virtual void Fire(Vector3 position, Quaternion rotation) {
        this.transform.position = position;
        this.transform.rotation = rotation;
        this.gameObject.SetActive(true);
    }

}
