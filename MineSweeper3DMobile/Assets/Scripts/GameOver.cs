using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour 
{
    public GameObject GameOverScreen;
    public GameObject WinScreen;
    
	// Use this for initialization
	void Start () 
    {

	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Controller.StateChanged && Controller.GameState == 2)
        {
            Controller.StateChanged = false;

            if (Controller.Win)
            {
                Controller.Win = false;
                Invoke("buildWin", 3);
                
            }
            else Invoke("buildGameOver", 3);

        }
     
	   
	}

    void buildGameOver()
    { 
     Instantiate(GameOverScreen, new Vector3(0, 0, 0),transform.rotation);
    }

    void buildWin()
    {
        Instantiate(WinScreen, new Vector3(0, 0, 0), transform.rotation);
    }
}
