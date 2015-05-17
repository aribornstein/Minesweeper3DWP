using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    public GameObject StartScreen;
    public static int GameState = 0;
    public static int Difficulty= 0;
    public static bool Win= false;
    public static bool StateChanged = false;
    public static string StringMap = "";

	// Use this for initialization
	void Start () 
    {
        
        Instantiate(StartScreen, new Vector3(-39.11686f, 56.46457f, 10.30713f), transform.rotation);

	}
	
	// Update is called once per frame
	void Update () 
    {


        //new game 
        if (StateChanged && GameState == 0)
        {
            Instantiate(StartScreen, new Vector3(-39.11686f, 56.46457f, 10.30713f), transform.rotation);
            StateChanged = false;
        }
  

	}
}
