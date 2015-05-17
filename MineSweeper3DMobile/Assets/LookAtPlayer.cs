using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 v = GameObject.FindGameObjectsWithTag("PlayerMainCamera")[0].transform.position - transform.position;

        v.x = v.z = 0.0f;

        transform.LookAt(GameObject.FindGameObjectsWithTag("PlayerMainCamera")[0].transform.position - v);

        transform.Rotate(0, 180, 0);

	
	}
}
