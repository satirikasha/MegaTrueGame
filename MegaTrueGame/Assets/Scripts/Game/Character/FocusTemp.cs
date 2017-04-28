using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusTemp : MonoBehaviour {

    private CameraFocusPoint _Focus;
    private float _Target;

	void Start () {
        _Focus = this.GetComponent<CameraFocusPoint>();
	}
	
	void Update () {
        _Target = Input.GetMouseButton(0) ? 1 : 0;
        _Focus.Weight = Mathf.Lerp(_Focus.Weight, _Target, Time.deltaTime * 3);
	}
}
