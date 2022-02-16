/****
 * Created by: Andrew Nguyen
 * Date created: Feb 16, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Feb 16, 2022
 * 
 * Description: Draws the line of the projectile's trajectory
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{

    static public ProjectileLine S; //singleton, there's only one

    [Header("SET IN INSPECTOR")]
    public float minDist = 0.1f;

    private LineRenderer line;
    private GameObject _poi;
    private List<Vector3> points;

    private void Awake()
    {
        S = this; //set singleton

        line = GetComponent<LineRenderer>(); //Reference to linerenderer
        line.enabled = false;
        points = new List<Vector3>; //new list
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
                AddPoints();
            } //end if
        }//end set

    }//end poi()

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
