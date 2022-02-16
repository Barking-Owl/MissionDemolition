/****
 * Created by: Andrew Nguyen
 * Date created: Feb 14, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Feb 16, 2022
 * 
 * Description: Let the projectile be tracked by the camera
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    /**** VARIABLES ****/
    //Make sure there's only one reference to the object we're trying to follow
    static public GameObject POI; //Projectile to focus on, could also be anything else the camera focuses on. Don't make it move back and forth/Z axis

    [Header("SET IN INSPECTOR")] //set manually
    public float easing = 0.05f; //5% of a move over time
    public Vector2 minXY = Vector2.zero;

    [Header("SET DYNAMICALLY")] //dont set manually
    public float camZ; //desire Z position of the camera

    private void Awake()
    {
        camZ = this.transform.position.z; //Sets Z value

    } //end Awake




    // Start is called before the first frame update
    void Start()
    {
        
    } //end Start()

    // Update is called once per frame
    void FixedUpdate() //Ensure Camera's framerate is constant despite projectile's variable speed (which uses "Update")
    {
        //if (POI == null) return; //Do nothing if there's no POI

        //Vector3 destination = POI.transform.position; //Get position of POI
        Vector3 destination;
        if (POI == null)
        {
            destination = Vector3.zero;
        } //end if
        else
        {
            destination = POI.transform.position;
            if (POI.tag == "Projectile")
            {
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null; //Null POI if RigidBody is asleep
                } //end if (POI.GetComponent<Rigidbody>().IsSleeping())
            } //end if (POI.tag == "Projectile")
        } //end else

        //Limit camera view
        destination.x = Mathf.Max(minXY.x, destination.x); //Choose a value between these 2 
        destination.y = Mathf.Max(minXY.y, destination.y);

        //Interpolate from current position to destination
        destination = Vector3.Lerp(transform.position, destination, easing);

        destination.z = camZ; //Rests just the Z value
        transform.position = destination;

        Camera.main.orthographicSize = destination.y + 10; //Keep tracking the ball for minimum orthographic size
    } //end Fixed Update()
}
