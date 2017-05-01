﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour {

    public const float WalkableAngleThreshold = 0.75f;

    public float MoveSpeed = 10;
    public float StopSpeed = 1;
    public float RotationSpeed = 10;

    public Vector3 Velocity {
        get {
            return _Rigidbody.velocity;
        }
    }

    public Vector3 PlanarVelocity {
        get {
            return Vector3.ProjectOnPlane(_Rigidbody.velocity, Vector3.up);
        }
    }

    private Rigidbody _Rigidbody;
    private Collider _Collider;
    private bool _IsGrounded;
    private RaycastHit _GroundHit;
    private Vector3 _LookDirection;
    private Vector3 _MoveDirection;
    private float _LookSpeedMult;
    private float _MoveSpeedMult;

	void Awake () {
        _Rigidbody = this.GetComponent<Rigidbody>();
        _Collider = this.GetComponent<Collider>();
	}
	
	void Update () {
        UpdateGroundInfo();
        UpdateMovement();
        UpdateRotation();
    }

    public void SetMoveDirection(Vector3 direction, float speed = 1) {
        _MoveDirection = direction.normalized;
        _MoveSpeedMult = speed;
    }

    public void SetLookDirection(Vector3 direction, float speed = 1) {
        _LookDirection = direction.normalized;
        _LookSpeedMult = speed;
    }

    private void UpdateMovement() {
        if (_IsGrounded) {
            _Rigidbody.AddForce(Vector3.ProjectOnPlane(_MoveDirection, _GroundHit.normal).normalized * _Rigidbody.mass * (_MoveSpeedMult * MoveSpeed + _Collider.sharedMaterial.dynamicFriction * 9.8f));
            _Rigidbody.AddForce(-PlanarVelocity * (1 - Vector3.Dot(PlanarVelocity.normalized, _MoveDirection)) * StopSpeed * _Rigidbody.mass);
        }
    }

    private void UpdateRotation() {
        if (_IsGrounded && Vector3.ProjectOnPlane(_LookDirection, Vector3.up) != Vector3.zero) {
            _Rigidbody.MoveRotation(Quaternion.Lerp(_Rigidbody.rotation, Quaternion.LookRotation(_LookDirection), Time.deltaTime * RotationSpeed * _LookSpeedMult));
        }
    }

    private void UpdateGroundInfo() {
        _IsGrounded = Physics.Raycast(transform.position + Vector3.up * 0.05f, -Vector3.up, out _GroundHit, 0.1f);
    }
}
