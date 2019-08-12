using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public List<ImpController> players;
    public List<Tank_Data> tanks;

    public P_Gen progen;

    public int bulletInstance;
    //Data
    public float playerHealth, playerMaxHealth;
    public float enemyHealth, enemyMaxHealth;


    public float playerDamage;
    public float enemyDamage;

    public bool gamePlayStart = false;

    //GUI
    public Canvas parentUi; 
    public Image healthUI;

    //waypoint
    public List<Transform> waypoints;

    //respawn points
    public List<Transform> respawnPoint; 


    //see if player one or two
    public enum PlayerMode
    {
        singlePlayer,
        Multiplayer
    }

    //Cameras
    public List<Camera> playerCam;

    public PlayerMode playermode;

    //Gameplay
    public bool gameplayStart = false;

    void Start()
    {//stops game object from being destroyed on load. 
        if (instance == null)
        {
            instance = this;//reference current script
            DontDestroyOnLoad(this);//don't destroy wile moving
        }
        else
        {
            Destroy(gameObject);
        }
        playerHealth = playerMaxHealth;//players health starts at max. 
        enemyHealth = enemyMaxHealth;//enemy health starts at max. 
        //healthUI.fillAmount = playerHealth / playerMaxHealth;//the fill amount has a value between zero and one. 
        
    }
    private void Update()
    {
        //healthUI.fillAmount = playerHealth;//player health will equal to fill amount. 

        if (IsPlaying())
        {
            progen.enabled = true;
            parentUi.gameObject.SetActive(true);
        }
    }

    public void LoseHealth(float amount, float entityValue)
    {
        if(playerHealth != 0)
        {
            healthUI.fillAmount -= amount / entityValue;
            playerHealth = healthUI.fillAmount;
        }
    }
    public void GainHealth(float amount, float entityValue)
    {
        if (playerHealth < playerMaxHealth)
        {
            healthUI.fillAmount += amount / entityValue;
            playerHealth = healthUI.fillAmount;
        }
    }

    public void SpawnOnSpot(ImpController player, Vector3 spawnPosition)
    {
        Instantiate(player.gameObject);
        player.gameObject.transform.position = spawnPosition;
    }

    public void FindWaypoints()
    {
        GameObject[] wp = GameObject.FindGameObjectsWithTag("Waypoint");
        waypoints.Add(wp[0].transform);
    }

    public bool IsPlaying()
    {
        switch (gameplayStart)
        {
            case true:
                switch (playermode)
                {
                    case PlayerMode.singlePlayer:

                        playerCam[0].rect = new Rect(0, 0, 1, 1);

                        break;
                    case PlayerMode.Multiplayer:

                        playerCam[0].rect = new Rect(0, 0, 0.49f, 1);

                        break;
                    default:

                        break;
                }
                return true;
            default:
                //parentUi.gameObject.SetActive(false);
                return false;
        }
    }

}
