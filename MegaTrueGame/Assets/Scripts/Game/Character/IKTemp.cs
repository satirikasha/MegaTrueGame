using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTemp : MonoBehaviour {

    private Animator _Animator;
    private Gun _Gun;

    void Start () {
        _Animator = this.GetComponent<Animator>();
        _Gun = this.transform.parent.GetComponentInChildren<Gun>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnAnimatorIK(int layerIndex) {
        _Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        _Animator.SetIKPosition(AvatarIKGoal.LeftHand, _Gun.LeftHandPoint.position);
        _Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        _Animator.SetIKPosition(AvatarIKGoal.RightHand, _Gun.RightHandPoint.position);
    }
}
