/****
 * Created by: Andrew Nguyen
 * Date created: Feb 16, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Feb 16, 2022
 * 
 * Description: Puts rigid body to sleep
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] //This script needs a Rigidbody to run, itll add one if there is not one in the object

public class RigidbodySleep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null) //If rb exists make it sleep
        {
            rb.Sleep();
        }//end if
    } //end Start()

    // Update is called once per frame
    void Update()
    {
        
    } //end Update()
}
