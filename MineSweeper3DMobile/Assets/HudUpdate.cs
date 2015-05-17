using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HudUpdate : MonoBehaviour {

    private static string mapText;
    private static int transformDir;
    private static Quaternion orientation;

    // Use this for initialization
	void Start () 
    {
        mapText = Controller.StringMap;
        //orient north 
        transformDir = 0;
        
        //Update HUD (to be moved to HudManager.Cs)
        Text hud = (Text)Object.FindObjectsOfType(typeof(Text))[0];//hacky when move gui text comes in to play use HudText tag
        hud.text = mapText;


       

	}
	
	// Update is called once per frame
	void Update () 
    {
        //get player orientation
        float angle = 45;
        GameObject playerCamera = GameObject.FindGameObjectsWithTag("PlayerMainCamera")[0];
        GameObject North = GameObject.FindGameObjectsWithTag("Barrier")[0];
        GameObject East = GameObject.FindGameObjectsWithTag("Barrier")[1];
        GameObject South = GameObject.FindGameObjectsWithTag("Barrier")[2];
        GameObject West = GameObject.FindGameObjectsWithTag("Barrier")[3];

        //new object condition
        if (mapText != Controller.StringMap )
        {
            //update hudText
            mapText = Controller.StringMap;
            //get hud
            Text hud = (Text)Object.FindObjectsOfType(typeof(Text))[0];//hacky when move gui text comes in to play use HudText tag

            //preform transformation on new mapText to generate HudText
            if (transformDir == 1) hud.text= transposeEast();
            else if (transformDir == 2) hud.text=transposeSouth();
            else if (transformDir == 3) hud.text=transposeWest();
            else hud.text = mapText;          

        }

        
        if (Vector3.Angle(playerCamera.transform.forward, North.transform.position - playerCamera.transform.position) < angle)
        {

            if (transformDir != 0)
            {
                Text hud = (Text)Object.FindObjectsOfType(typeof(Text))[0];//hacky when move gui text comes in to play use HudText tag
                hud.text = mapText;
                transformDir = 0;
                Debug.Log("North");
            }
        }
        else if (Vector3.Angle(playerCamera.transform.forward, East.transform.position - playerCamera.transform.position) < angle)
        {
            if (transformDir != 1)
            {
                Text hud = (Text)Object.FindObjectsOfType(typeof(Text))[0];//hacky when move gui text comes in to play use HudText tag
                hud.text = transposeEast();
                transformDir = 1;
                Debug.Log("East");
             }
        }
        else if (Vector3.Angle(playerCamera.transform.forward, South.transform.position - playerCamera.transform.position) < angle)
        {
            if (transformDir != 2)
            {
                Text hud = (Text)Object.FindObjectsOfType(typeof(Text))[0];//hacky when move gui text comes in to play use HudText tag
                hud.text = transposeSouth();
                transformDir = 2;
                Debug.Log("South");
            }
        }
        else if (Vector3.Angle(playerCamera.transform.forward, West.transform.position - playerCamera.transform.position) < angle)
        {
            if (transformDir != 3)
            {
                Text hud = (Text)Object.FindObjectsOfType(typeof(Text))[0];//hacky when move gui text comes in to play use HudText tag
                hud.text = transposeWest();
                transformDir = 3;
                Debug.Log("West");
            }
        }
       
	}

    string transposeEast()
    {
        char[] mapArray = transposeWest().ToCharArray();
        System.Array.Reverse(mapArray);
        return new string(mapArray);
    }

    string transposeWest()
    {
        string transpose = "";// the result
        int difSize = 5;  //hud size
        char[,] charMap = new char[difSize, difSize]; //tool to transposse

        //get charMap matrix
        string[] rows = mapText.Split('\n');
        for (int r = 0; r < difSize; r++)
        {
            string[] cols = rows[r].Split(' ');
            for (int c = 0; c < difSize; c++)
            {
                charMap[r, c] = cols[c].ToCharArray()[0];
            }
        }
        // rotate char map

        charMap = RotateMatrix(charMap, difSize);

        // rebuild transposeStringMap
        for (int r = 0; r < difSize; r++)
        {
            string inner = "";
            for (int c = 0; c < difSize; c++)
            {
                inner += charMap[r, c] + " ";
            }
            transpose += inner + "\n";
        }
        //return result
        return transpose;
    }

    string transposeSouth()
    {
        char[] mapArray = mapText.ToCharArray();
        System.Array.Reverse(mapArray);
        return new string(mapArray);
    }

    static char[,] RotateMatrix(char[,] matrix, int n)
    {
        char[,] ret = new char[n, n];

        for (int i = 0; i < n; ++i)
        {
            for (int j = 0; j < n; ++j)
            {
                ret[i, j] = matrix[n - j - 1, i];
            }
        }

        return ret;
    }
}
