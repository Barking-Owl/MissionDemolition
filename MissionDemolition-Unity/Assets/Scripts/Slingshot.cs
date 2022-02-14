/****
 * Created by: Andrew Nguyen
 * Date created: Feb 9, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Feb 14, 2022
 * 
 * Description: Controller for the Slingshot
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    //VARIABLES
    [Header("SET IN INSPECTOR")]
    public GameObject prefabProjectile;
    public float velocityMultiplier = 8f;

    [Header("SET DYNAMICALLY")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile; //an instance of a projectile
    public bool aimingMode; //is player aiming or not
    public Rigidbody projectileRB; //Rigid body of projectile

    private void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint"); //Find child object
        launchPoint = launchPointTrans.gameObject; //The game object
        launchPoint.SetActive(false); //disable game object
        launchPos = launchPointTrans.position;
    } //end Awake()

    

    private void OnMouseEnter()
    {

        launchPoint.SetActive(true); 
        print("Slingshot: OnMouseEnter METHOD");

    } //end OnMouseEnter()

    private void OnMouseExit()
    {
        
        launchPoint.SetActive(false);
        print("Slingshot: OnMouseExit METHOD");

    } //end OnMouseExit()

    private void OnMouseDown()
    {

        aimingMode = true; //As long as player is holding LMB
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = launchPos;

        //Gravity shouldn't affect the projectile while the player is holding it. So it should be kinematic.
        projectileRB = projectile.GetComponent<Rigidbody>(); //Meaning physics will not affect projectile
        projectileRB.isKinematic = true;

    } //end OnMouseDown()

    private void Update()
    {
        //Only update if aiming
        if (!aimingMode) return;

        //Get current mouse position in 2D screen coordinates.
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - launchPos; //Pixel amount of change btwn mouse3D and launch position
        //Limit it to the radius of slingshot's collider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize(); //Sets vector to same direction but length is 1.0
            mouseDelta *= maxMagnitude;
        } //end if

        //move projectile to the new position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        //Launch the projectile
        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            projectileRB.isKinematic = false;
            projectileRB.velocity = -mouseDelta * velocityMultiplier;

            FollowCam.POI = projectile; //Set POI for Camera 

            projectile = null; //Doesn't delete the projectile instance but just to not remember it. Had a problem with getting the projectile to not drop.
            //Solution was that the sphere collider was not a trigger so I set it accordingly.
        } //end if
    } //end Update()

} //End Slingshot
