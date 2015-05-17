using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using System;

public class NumberSpawn : MonoBehaviour {

    public GameObject NumberText;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }


    void OnTriggerEnter(Collider collisions)
    {
        //Abstract for other levels


        if (collisions.gameObject.tag == "MineDetector")
        {

            //get # adjacent bombs
            int number = CountAdjacentBombs();

            //generate number
            TextMesh mesh = (TextMesh)NumberText.GetComponent("TextMesh");
            mesh.text = number.ToString();
            Instantiate(NumberText, transform.position, transform.rotation);
            
            //update stringMap
            int n = 5;
            if (Controller.Difficulty == 1) n = 7;
            else if (Controller.Difficulty == 2) n = 9; 

            //the below condition is a hack that disables hud functionality on levels above beginner until i can figure out positioning
            if (Controller.Difficulty > 0)
            {
                Destroy(gameObject);
                return;
            }            
            
            float r = gameObject.transform.position.x/10;
            float c = gameObject.transform.position.z/10;
            
            float hr = r+1;
            float hc = (n*n)-(n)-(n*c);
            int hudInd = (int)(  hr+hc );
            Debug.Log("r:"+r+" c:" + c + "\nhr:"+hr+" hc:" + hc + "\nhudInd:" + hudInd);
             int xToBeChanged = this.GetNthIndex('X', hudInd);
            char[] array = Controller.StringMap.ToCharArray();
            array[xToBeChanged] = number.ToString().ToCharArray()[0];
            Controller.StringMap = new string(array);
     

            Destroy(gameObject);
        }
    }

    public int GetNthIndex( char t, int n)
    {
        //set only for five and bad run time memoize hud sizes in controller
        string s = "";
        for (int i = 0; i < 5; i++) s += "X X X X X\n";

        int count = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == t)
            {
                count++;
                if (count == n)
                {
                    return i;
                }
            }
        }
        return -1;
    }
    private int CountAdjacentBombs()
    {
        int numOfBombs = 0;
        string log = "";
        Vector3 north = new Vector3(transform.position.x + 10, 1.0f, transform.position.z);
        Vector3 south = new Vector3(transform.position.x - 10, 1.0f, transform.position.z);
        Vector3 east = new Vector3(transform.position.x, 1.0f, transform.position.z + 10);
        Vector3 west = new Vector3(transform.position.x, 1.0f, transform.position.z - 10);
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
        foreach (GameObject go in bombs)
        {
            if (go.transform.position == north)
            {
                numOfBombs += 1;
                log += " North: " + north;
            }

            else if (go.transform.position == south)
            {
                numOfBombs += 1;
                log += " South: " + south;
            }

            else if (go.transform.position == east)
            {
                numOfBombs += 1;
                log += " East: " + east;
            }

            else if (go.transform.position == west)
            {
                numOfBombs += 1;
                log += "West: " + west;
            }

        }


//         Debug.Log(log);
        return numOfBombs;
    }
}
