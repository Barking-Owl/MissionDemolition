/****
 * Created by: Andrew Nguyen
 * Date created: Feb 20, 2022
 * 
 * Last Edited by: Andrew Nguyen
 * Last Edited: Feb 20, 2022
 * 
 * Description: Game Manager for the game
 * 
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S; //private Singleton

    //VARIABLES//

    [Header("SET IN INSPECTOR")]
    public Text uitLevel; //What the current level is according to the text on UI
    public Text uitShots; //The number of shots on the UI
    public Text uitButton; //Text on the button on the UI
    public Vector3 castlePos; //Position of castles
    public GameObject[] castles; //Holds the castles that can be generated

    [Header("SET DYNAMICALLY")]
    public int level;
    public int levelMax; //Amount of levels in the game
    public int shotsTaken;
    public GameObject castle; //Castle for the level
    public GameMode mode = GameMode.idle;
    public string showing = "Show Stringshot"; //So we can tell the camera when to focus on the slingshot

    // Start is called before the first frame update
    void Start()
    {
        S = this;

        level = 0;
        levelMax = castles.Length; //Amount of levels. This will ensure each castle shows up once
        StartLevel();
    } //end Start()

    void StartLevel()
    {
        //Remove castle so we can replace it with a new one
        if (castle != null)
        {
            Destroy(castle);
        }

        //Then remove old projectiles to reduce clutter
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        //Then instantiate next level's castle
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        //Set camera to normal view with both Slingshot/Castle in view
        SwitchView("Show Both");
        ProjectileLine.S.Clear();

        //Reset goal
        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;
    } //end StartLevel()

    void UpdateGUI()
    {
        //Show data in the text in UI
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
    } //end UpdateGUI()

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();

        //Check if conditions to complete the level are meant
        if ( (mode == GameMode.playing) && Goal.goalMet)
        {
            //Change the mode
            mode = GameMode.levelEnd;
            //Change camera view
            SwitchView("Show Both");
            //Start next level
            Invoke("NextLevel", 2f);
        }
    } //end Update()

    void NextLevel()
    {
        level++;

        if (level == levelMax)
        {
            level = 0;
        } //end if
        StartLevel();
    }//end Nextlevel()

    public void SwitchView(string eView = "")
    {
        if (eView == "")
        {
            eView = uitButton.text;
        }//end if

        showing = eView;

        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButton.text = "Show Castle";
                break;
            case "Show Castle":
                FollowCam.POI = S.castle;
                uitButton.text = "Show Both";
                break;
            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
        } //end Switch
    } //end SwitchView

    public static void ShotFired()
    {
        S.shotsTaken++;
    }
} //end GameManager
