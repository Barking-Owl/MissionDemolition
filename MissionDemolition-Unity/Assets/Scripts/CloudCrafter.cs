/****
 * Created by: Andrew Nguyen
 * Date created: Feb 15, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Feb 16, 2022
 * 
 * Description: Script to generate multiple clouds
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{

    [Header("SET IN INSPECTOR")]
    public int numClouds = 40; //How many clouds to make/the limit of number of clouds so game does not crash
    public GameObject cloudPrefab; //Prefab for the clouds
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1;
    public float cloudScaleMax = 3;
    public float cloudSpeedMult = 0.5f; //Adjust the speed of each cloud

    private GameObject[] cloudInstances;

    private void Awake()
    {
        //Make an array to hold all Clouds
        cloudInstances = new GameObject[numClouds];

        //Find CloudAnchor GameObject
        GameObject anchor = GameObject.Find("CloudAnchor");
        //Iterate and make clouds
        GameObject cloud;
        for (int i = 0; i < numClouds; i++)
        {
            //Instantiate cloudPrefab
            cloud = Instantiate<GameObject>(cloudPrefab);
            //Set position of cloud
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);

            //Scale clouds
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            //Smaller clouds are closer to the ground
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
            //Smaller clouds are also distant
            cPos.z = 100 - 90 * scaleU;
            //Apply the transforms
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            //Make cloud a child ofthe anchor
            cloud.transform.SetParent(anchor.transform);
            //add cloud to cloudinstances
            cloudInstances[i] = cloud;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Iterate over generated clouds
        foreach (GameObject cloud in cloudInstances) {
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;
            //Larger clouds go faster
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;
            //Cloud that goes too far left should go to the far right
            if (cPos.x <= cloudPosMin.x) {
                cPos.x = cloudPosMax.x;
            } //end if
            //Apply new position to cloud
            cloud.transform.position = cPos;
        }//end foreach
    }
}
