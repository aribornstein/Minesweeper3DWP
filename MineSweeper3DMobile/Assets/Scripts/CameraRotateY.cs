/*  
 *  CameraRotateY is a camera class for the TouchFPC for Unity 3D 
 *  written by Jason Walters
 *  http://glitchbeam.com | https://twitter.com/jasonrwalters
 * 
 *  last update on 12/5/14
 */

using UnityEngine;
using System.Collections;

public class CameraRotateY : MonoBehaviour 
{	
	// Update is called once per frame
	void Update () 
    {
        // fetch TouchFPC class rotation
        Vector2 rotation = TouchFPC.rotation;

        // update our rotation...
        // why are the x/y vectors flipped?
        // x touch pos is a horizontal movement, map to horizontal rotation (y axis)
        // y touch pos is a vertical movement, map to vertical rotation (x axis)
        transform.rotation = Quaternion.Euler(new Vector3(rotation.y, rotation.x, 0.0f));
	}
}
