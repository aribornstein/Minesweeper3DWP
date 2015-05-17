using UnityEngine;
using System.Collections;

public class NewGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

       void OnMouseEnter()
    {
       
        renderer.material.color= Color.blue;
    }

    void OnMouseExit()
    {
         renderer.material.color= Color.white;
    }

    void OnMouseUp()
    {

        DestroyGameOver();
        
        Controller.StateChanged = true;
        Controller.GameState = 0;
    }
    void DestroyGameOver()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("GameOverComponent"))
        {
            Destroy(go);
        }
    }
}
