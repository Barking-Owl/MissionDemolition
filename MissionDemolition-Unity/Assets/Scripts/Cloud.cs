/****
 * Created by: Andrew Nguyen
 * Date created: Feb 14, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Feb 14, 2022
 * 
 * Description: Cloud generation
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    /**** VARIABLES ****/
    [Header("SET IN INSPECTOR")] //set manually
    public GameObject cloudSphere;
    public int numberSpheresMinimum = 6;
    public int numberSpheresMaximum = 10;
    //Clouds aren't perfectly circular. Need to create a range of scale
    public Vector2 sphereScaleRangeX = new Vector2(4, 8);
    public Vector2 sphereScaleRangeY = new Vector2(3, 4);
    public Vector2 sphereScaleRangeZ = new Vector2(2, 4);
    public Vector3 sphereOffsetScale = new Vector3(5, 2, 1);
    public float scaleYMin = 2f;

    private List<GameObject> spheres;

    // Start is called before the first frame update
    void Start()
    {
        spheres = new List<GameObject>();
        int num = Random.Range(numberSpheresMinimum, numberSpheresMaximum);

        for (int i = 0; i < num; i++)
        {
            //Instantiate cloud spheres
            GameObject sp = Instantiate<GameObject>(cloudSphere);
            spheres.Add(sp);

            Transform spTrans = sp.transform;
            spTrans.SetParent(this.transform);

            //Randomly assign a position
            Vector3 offset = Random.insideUnitSphere;
            offset.x *= sphereOffsetScale.x;
            offset.y *= sphereOffsetScale.y;
            offset.z *= sphereOffsetScale.z;
        } //end for loop
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
