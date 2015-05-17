using UnityEngine;
using System.Collections;

public class GameMenuHide : MonoBehaviour {

    public GameObject myGame;
	// Use this for initialization
	void Start ()
    {
	 
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Controller.GameState > 0)
        {            
            destroyScreen();
            Instantiate(myGame, new Vector3(0.0f, 0.0f, 0.0f), transform.rotation);
        }

	}

    void destroyScreen()
    {

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("MenuScreenComponent"))
        {    
            Destroy(go);
        }
    }
}
