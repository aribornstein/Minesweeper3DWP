using UnityEngine;
using System.Collections;

public class BombSpawn : MonoBehaviour {

    public GameObject bomb;

    bool explosionOn = false;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (explosionOn)
        {
            explosionOn = false;
            
            Instantiate(bomb, transform.position, transform.rotation);
            StartCoroutine(FallToDeath());
            
            
           
           
        }
    }

    void OnTriggerEnter(Collider collisions)
    {
        if (collisions.gameObject.tag == "MineDetector")
        {
            Debug.Log("BOOOOOM!");
            explosionOn = true;
            
        }
    }

    IEnumerator FallToDeath()
    {
        Debug.Log("Before Waiting 2 seconds");
        yield return new WaitForSeconds(1.5f);
        GameObject.FindGameObjectsWithTag("MineDetector")[1].transform.Translate(0, -1, 0);
        Destroy(gameObject);
        Debug.Log("After Waiting 2 Seconds");
    }
}
