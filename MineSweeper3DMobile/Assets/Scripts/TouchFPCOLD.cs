/*  
 *  TouchFPC is a "touch first person controller" for Unity 3D 
 *  written by Jason Walters
 *  http://glitchbeam.com | https://twitter.com/jasonrwalters
 * 
 *  last update on 12/3/14
 */

using UnityEngine;
using System.Collections;

public class TouchFPCOLD : MonoBehaviour
{



    private Vector2 rotation = new Vector2(0f, 0f);
    private Vector2 position = new Vector2(0f, 13.25786f);

    public float speedPos = 0.1f;
    public float speedRot = 0.1f;

    private float screenHalf = 0.0f;

    // Use this for initialization
    void Start()
    {
        // grab screen width, divide by half
        screenHalf = Screen.width / 2;
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        int i = 0;
        int touchCount = Input.touchCount;
        while (i < Input.touchCount)
        {
            // checking for moving touches
            if (Input.GetTouch(i).phase == TouchPhase.Moved)
            {
                // use the position to determine touch/screen location
                Vector2[] touchPosition = new Vector2[touchCount];
                touchPosition[i] = Input.GetTouch(i).position;

                // use the delta position to increment position & rotation
                Vector2[] touchDeltaPosition = new Vector2[touchCount];
                touchDeltaPosition[i] = Input.GetTouch(i).deltaPosition;

                Debug.Log("TOUCH X: " + touchPosition[i].x);

                // if a touch is detected on left side of screen...
                if ((touchPosition[i].x < screenHalf) && (touchDeltaPosition[i].y > 0))
                {
                    // increment position by touch movement
                    position += touchDeltaPosition[i] * speedPos;

                }
                // if a touch is detected on right side of screen...
                else
                {
                    // increment rotation by touch movement
                    rotation += touchDeltaPosition[i] * speedRot;
                }
            }
            i++;
        }

        // update our position and rotation
        transform.position = new Vector3(position.x, transform.position.y, 0.0f);
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation.x, 0.0f));





        // make sure our "player" moves forward in dir we face
        transform.Translate(Vector3.forward * position.y);


    }

}
