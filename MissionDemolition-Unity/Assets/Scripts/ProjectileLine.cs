/****
 * Created by: Andrew Nguyen
 * Date created: Feb 16, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Feb 20, 2022
 * 
 * Description: Draws the line of the projectile's trajectory
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{

    static public ProjectileLine S; //Singleton, there's only one

    [Header("SET IN INSPECTOR")]
    public float minDist = 0.1f;

    private LineRenderer line;
    private GameObject _poi;
    private List<Vector3> points;

    private void Awake()
    {
        S = this; //set singleton

        line = GetComponent<LineRenderer>(); //Reference to LineRenderer
        line.enabled = false;
        points = new List<Vector3>(); //new list
    } //end Awake()

    public GameObject poi
    {
        get { return (_poi); }
        set
        {
            _poi = value;
            if (_poi != null)
            {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            } //end if
        }//end set

    }//end poi()

    //Clear the line
    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    } //end Clear()

    public void AddPoint()
    {
        //Add points to the line
        Vector3 pt = _poi.transform.position;
        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist)
        {
            return; //Return, if the distance between this point and another is too close
        } //end if
        //The launch point
        if (points.Count == 0)
        {
            Vector3 launchPosDiff = pt - Slingshot.LAUNCH_POS;
            //Adds to the line
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            //The first two points
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            //LineRenderer
            line.enabled = true;

        }
        else
        {
            //Add a point for all other cases
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        } //end point boolean
    } //end AddPoint()

    public Vector3 lastPoint //Check for last point
    {
        get
        {
            if (points == null)
            {
                return (Vector3.zero);
            }
            return (points[points.Count - 1]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (poi == null)
        {
            //Find a POI
            if (FollowCam.POI != null)
            {
                if (FollowCam.POI.tag == "Projectile")
                {
                    poi = FollowCam.POI;
                }
                else
                {
                    return; //If no POI is found
                } //end else
            }//end if (FollowCam.POI != null)
            else
            {
                return; //If no POI is found
            } //end else
        } //end if (poi == null)
        AddPoint(); //If a POI is found
        if (FollowCam.POI == null)
        {
            //Make the poi null if the FollowCam.POI is too
            poi = null;
        } //end if

    } //end fixedUpdate
} //end ProjectileLine class
