/*  
 *  TouchFPC is a "touch first person controller" for Unity 3D 
 *  written by Jason Walters
 *  http://glitchbeam.com | https://twitter.com/jasonrwalters
 * 
 *  last update on 12/5/14
 */

using UnityEngine;
using System.Collections;

public class TouchFPC : MonoBehaviour 
{
    public float speedPos = 0.1f;
    public float speedRot = 0.1f;
    public bool invertY = true;

    static public Vector2 rotation = new Vector2(0f, 0f);
    static public Vector2 position = new Vector2(0f, 0f);

    private float screenHalf = 0.0f;

	// Use this for initialization
	void Start () 
    {
        // grab screen width, divide by half
        screenHalf = Screen.width / 2;
	}
	
	// Update is called once per frame
	void Update () 
    {
        int i = 0;
        int touchCount = Input.touchCount;
        while (i < Input.touchCount)
        {
            // use touch position to determine touch screen location
            Vector2[] touchPosition = new Vector2[touchCount];
            touchPosition[i] = Input.GetTouch(i).position;

            // use touch delta position for position & rotation
            Vector2[] touchDeltaPosition = new Vector2[touchCount];
            touchDeltaPosition[i] = Input.GetTouch(i).deltaPosition;

            // checking for moving touches or when touches end
            if (Input.GetTouch(i).phase == TouchPhase.Moved || Input.GetTouch(i).phase == TouchPhase.Ended)
            {
                // if a touch is on left side of screen...
                if (touchPosition[i].x < screenHalf)
                {
                    // position equals touch movement
                    // if on phase ended, stop
                    position = (touchDeltaPosition[i] * speedPos);
                }

                // if a touch is on right side of screen...
                if (touchPosition[i].x > screenHalf)
                {
                    // if true, invert the Y vertical rotation - X axis
                    if (invertY) touchDeltaPosition[i].y *= -1.0f;
                    // increment rotation by touch movement
                    // if on phase ended, stop
                    rotation += touchDeltaPosition[i] * speedRot;
                }

            }
            i++;
        }

        /* Works for non rigidbody
        // update our 2D rotation...
        // touch delta X position equals horizontal rotation - Y axis
        // touch delta Y position equals vertical rotation - X axis
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation.x, 0.0f));

        // clamp to 45 degrees up or down
        rotation.y = Mathf.Clamp(rotation.y, -45.0f, 45.0f);

        // update our 2D positions...
        // heading controlled by touch delta Y position
        transform.Translate(Vector3.forward * position.y);
        // horizontal straffing by touch delta X position
        transform.Translate(Vector3.right * position.x);
         */
	}

    void FixedUpdate()
    {
        // update our 2D rotation...
        // touch delta X position equals horizontal rotation - Y axis
        // touch delta Y position equals vertical rotation - X axis
        rigidbody.rotation = Quaternion.Euler(new Vector3(0.0f, rotation.x, 0.0f));

        // clamp to 45 degrees up or down
        rotation.y = Mathf.Clamp(rotation.y, -45.0f, 45.0f);

        // update our 2D positions...
        // heading controlled by touch delta Y position
        // horizontal straffing by touch delta X position
        float forward = transform.forward.z * position.y;
        float right = transform.right.x * position.x;
        rigidbody.velocity = new Vector3(right, 0.0f, forward);
    }
}
