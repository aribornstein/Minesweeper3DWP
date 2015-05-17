using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour {

    public  bool isBeginner = false;
    public  bool isIntermediate = false;
    public  bool isExpert = false;
	// Use this for initialization
	void Start ()
    {


    }
	
	// Update is called once per frame
	void Update () 
    {
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
      //check beginner go to state 1
        if (isBeginner) Controller.Difficulty = 0;
        //check intermidate
        else if (isIntermediate) Controller.Difficulty = 1;
        //check expert
        else if (isExpert) Controller.Difficulty = 2;
      // update GameState
        Controller.GameState = 1;
        Controller.StateChanged = true;


        Debug.Log("Dificulty:" + Controller.Difficulty + " State:" + Controller.GameState + " " + Controller.StateChanged);

        
    
    }
}
