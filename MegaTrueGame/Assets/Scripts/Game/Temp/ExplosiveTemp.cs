using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplosiveTemp : MonoBehaviour {

    public float Force = 3000;
    public float Radius = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<Projectile>()) {
            Physics.OverlapSphere(this.transform.position, Radius, LayerMask.GetMask("NPC", "Player"))
                .Select(_ => _.GetComponent<Rigidbody>())
                .Where(_ => _ != null)
                .ForEach(_ => _.AddExplosionForce(Force, this.transform.position, Radius))
                .Where(_ => (_.position - this.transform.position).magnitude < Radius / 2)
                .ForEach(_ => Destroy(_.gameObject));

            Destroy(this.gameObject);
        }
    }
}
