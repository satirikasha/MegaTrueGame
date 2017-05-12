using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTemp : MonoBehaviour {

    private Animator _Animator;
    private IKDataTemp _IKData;

    void Start () {
        _Animator = this.GetComponent<Animator>();
        _IKData = this.transform.parent.GetComponentInChildren<IKDataTemp>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnAnimatorIK(int layerIndex) {
        _Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        _Animator.SetIKPosition(AvatarIKGoal.LeftHand, _IKData.LeftHandPoint.position);
        _Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        _Animator.SetIKPosition(AvatarIKGoal.RightHand, _IKData.RightHandPoint.position);
    }
}
