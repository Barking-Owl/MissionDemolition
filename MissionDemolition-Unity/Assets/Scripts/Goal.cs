/****
 * Created by: Andrew Nguyen
 * Date created: Feb 20, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Feb 20, 2022
 * 
 * Description: Manages goal collision
 * 
 */
 
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    static public bool goalMet = false;

    //Start and fixed aren't needed. Only this method which manages what happens if something touches it:
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            //Should only react if its the projectile hitting it
            Goal.goalMet = true;
            //And set the goal accordingly so we know it's been hit
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 1;
            mat.color = c;
        }
    }
}
