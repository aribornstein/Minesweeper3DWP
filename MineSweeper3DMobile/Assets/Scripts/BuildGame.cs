using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BuildGame : MonoBehaviour {



    public GameObject room;
    public GameObject player;
    public GameObject bomb;
    public GameObject unswept;
    public GameObject barrier;




    private int RoomWidth=0;
    private int RoomLength=0;
    public List<int[]> bombLocations;


    // Use this for initialization
	void Start () 
    {
               

	}
	
	// Update is called once per frame
	void Update () 
    {
       
        if (Controller.StateChanged && Controller.GameState==1)
        {
            //clear old map
            Controller.StringMap = "";
            //Set Room Dimensions & HUD
            if (Controller.Difficulty == 0)
            {
                RoomWidth = 50;
                RoomLength = 50;
                //update Hud
                for (int i = 0; i < 5; i++) Controller.StringMap += "X X X X X\n";
            }
            else if (Controller.Difficulty == 1)
            {
                RoomWidth = 70;
                RoomLength = 70;
               // for (int i = 0; i < 7; i++) hudText += "X X X X X X X\n";

            }
            else if (Controller.Difficulty == 2)
            {
                RoomWidth = 90;
                RoomLength = 90;
              //  for (int i = 0; i < 9; i++) hudText += "X X X X X X X X X\n"; 
            }
            //buildArena
            BuildArena();
            
            // build barriers
            Instantiate(barrier, new Vector3(42.0f, 0.0f, 129.0f),  Quaternion.Euler(90.0f,180.0f,0.0f));//north
            Instantiate(barrier, new Vector3(146.0f, 0.0f, 32.0f), Quaternion.Euler(90.0f, 90.0f, 0.0f));//east
            Instantiate(barrier, new Vector3(48.0f, 0.0f, -63.0f), Quaternion.Euler(90.0f, 180.0f, 0.0f));//south
            Instantiate(barrier, new Vector3(-58.0f, 0.0f, 32.0f), Quaternion.Euler(90.0f, 90.0f, 0.0f));//west


            //Activate First Person Controller
            player = (GameObject)Instantiate(player, new Vector3(0.0f, 0.0f, 0.0f), transform.rotation);
            GameObject.FindGameObjectsWithTag("MineDetector")[2].transform.position = new Vector3((RoomWidth / 2), 1.0f, (RoomLength / 2) - 5);


            Controller.StateChanged = false;


        }

        //check win and lose conditions
        if (Controller.GameState == 1)
        {
            if (GameObject.FindGameObjectsWithTag("Unswept").Length == 0)
            {
                Debug.Log("Winner");
                Controller.Win = true;
                Controller.GameState = 2;
                Controller.StateChanged = true;
                destroyWorld();                
            }

            else if (GameObject.FindGameObjectsWithTag("MineDetector")[1].transform.position.y < 0.0)
            {
                Debug.Log("Oh No I'm Falling");
                Controller.GameState = 2;
                Controller.StateChanged = true;
                destroyWorld();
            }
        }
       
	}



    private void BuildArena()
    {

        //generate bombs
         GenerateBombs((RoomWidth / 10) * 2 -5);
        //Build Room
        for (int x = 0; x < RoomWidth; x += 10)
        {
            for (int z = 0; z < RoomLength; z += 10)
            {
                //build room
                Instantiate(room, new Vector3(x , 0.0f, z), transform.rotation);
                //generate unswept
                if (!isBombHere(x, z)) Instantiate(unswept, new Vector3(x, 1.0f, z), transform.rotation);

            }
        }
    }

    private void GenerateBombs(int numberOfBombs)
    {

        this.bombLocations = new List<int[]>();
        System.Random rnd = new System.Random();
        for (int i = 0; i < numberOfBombs; i++)
        {
            int x = rnd.Next(1, RoomLength/10) * 10; // creates a number between 1 and RoomLength
            int z = rnd.Next(1, RoomWidth/10) * 10; // creates a number between 1 and RoomWidth
            //make sure bomb isn't already there
            while (isBombHere(x, z))
            {
                x = rnd.Next(1, RoomLength / 10) * 10; // creates a number between 1 and RoomLength
                z = rnd.Next(1, RoomWidth / 10) * 10; // creates a number between 1 and RoomWidth
            }
            
            Instantiate(bomb, new Vector3(x, 1.0f, z), transform.rotation);
            this.bombLocations.Add(new int[] { x, z });
        }


    }

    private bool isBombHere(int x, int z)
    {
        var bomb = (from b in this.bombLocations where ((b[0] == x) && (b[1] == z)) select b).FirstOrDefault();
        if (bomb != null) return true;
        return false;
    }

    public  void destroyWorld()
    {
        Destroy(gameObject);
        foreach (GameObject o in GameObject.FindSceneObjectsOfType(typeof(GameObject)))
        {
            if (o.tag == "MineDetector" || o.tag == "Number" || o.tag == "Bomb" || o.tag == "Room" || o.tag == "Unswept" || o.tag == "Explosion")
            {
                Destroy(o,2);
            }

        }
    }
}
