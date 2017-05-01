using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.AI;

public class AITemp : MonoBehaviour {

    private MovementController _MovementController;

	void Awake () {
        _MovementController = this.GetComponent<MovementController>();		
	}
	
	void Update () {
        var path = new NavMeshPath();
        if (NavMesh.CalculatePath(this.transform.position, PlayerController.LocalPlayer.Position, NavMesh.AllAreas, path)) {
            path.DrawDebug(Color.yellow);
            var direction = Vector3.ProjectOnPlane((path.corners[1] - path.corners[0]), Vector3.up).normalized;
            _MovementController.SetLookDirection(direction);
            _MovementController.SetMoveDirection(this.transform.forward);
        }
        else {
            Debug.Log("Shit");
        }
    }
}
