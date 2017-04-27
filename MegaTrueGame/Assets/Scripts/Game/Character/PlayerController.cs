using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonBehaviour<PlayerController> {

    public static PlayerController LocalPlayer {
        get {
            return Instance;
        }
    }

    public Vector3 Velocity {
        get {
            return Rigidbody.velocity;
        }
    }

    public Vector3 PlanarVelocity {
        get {
            return Vector3.ProjectOnPlane(Rigidbody.velocity, Vector3.up);
        }
    }

    public Rigidbody Rigidbody { get; private set; }
    private Animator _Animator;
    private Gun _Gun;

    protected override void Awake() {
        base.Awake();
        Rigidbody = this.GetComponent<Rigidbody>();
        _Animator = this.GetComponentInChildren<Animator>();
        _Gun = this.GetComponentInChildren<Gun>();
    }

    void Update() {
        var aim = Input.GetMouseButton(0);
        var moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (moveVector.magnitude > 1)
            moveVector.Normalize();
        if (aim) {
            Rigidbody.AddForce(moveVector * 75);
        }
        else {
            Rigidbody.AddForce(moveVector * 125);
        }
        Rigidbody.AddForce(-Vector3.ProjectOnPlane(Rigidbody.velocity, Vector3.up) * 5);
        if (aim) {
            var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            var distance = 0f;
            new Plane(Vector3.up, 0).Raycast(mouseRay, out distance);
            var aimVector = Vector3.ProjectOnPlane(mouseRay.GetPoint(distance) - this.transform.position, Vector3.up);
            Rigidbody.MoveRotation(Quaternion.Lerp(Rigidbody.rotation, Quaternion.LookRotation(aimVector), Time.deltaTime * 10));
        }
        else {
            if (moveVector != Vector3.zero) {
                Rigidbody.MoveRotation(Quaternion.Lerp(Rigidbody.rotation, Quaternion.LookRotation(moveVector), Time.deltaTime * 5));
            }
        }

        var velocity = _Animator.transform.InverseTransformVector(PlanarVelocity);
        _Animator.SetFloat("Forward", velocity.z);
        _Animator.SetFloat("Right", velocity.x);
    }



    void FixedUpdate() {

    }
}
