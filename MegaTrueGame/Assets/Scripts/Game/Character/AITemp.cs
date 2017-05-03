using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.AI;

public class AITemp : MonoBehaviour {

    private MovementController _MovementController;
    private NavMeshPath _Path;

	void Awake () {
        _MovementController = this.GetComponent<MovementController>();
        _Path = new NavMeshPath();
	}
	
	void Update () {
        if (NavMesh.CalculatePath(this.transform.position, PlayerController.LocalPlayer.Position, NavMesh.AllAreas, _Path)) {
            _Path.DrawDebug(Color.yellow);
            var direction = Vector3.ProjectOnPlane((_Path.corners[1] - _Path.corners[0]), Vector3.up).normalized;
            _MovementController.SetLookDirection(direction);
            _MovementController.SetMoveDirection(this.transform.forward);
        }
        else {
            Debug.Log("Shit");
        }
    }

    private bool flag;
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<Bullet>()) {
            if (flag) {
                Destroy(this.gameObject);
            }
            else {
                flag = true;
            }
        }
    }
}
