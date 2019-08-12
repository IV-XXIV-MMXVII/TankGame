using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class P_Gen : MonoBehaviour
{

    public static P_Gen proceduralGen;

    private Random.State seedGenerator;

    public int genSeed = 10; //D_Value

    private bool seedGenInit = false;

    public List<GameObject> roomPrefabs;
    public List<GameObject> enemyPrefabs;
    public GameObject[,] gridSize;

    public int numCols;
    public int numRows;


    public float tileWidth = 50.0f; //values subject to change
    public float tileHeight = 50.0f;


    //map of day
    public bool mapOfTheDay;

    private void Start()
    {
        proceduralGen = this;

        Generate();
    }

    void Generate()
    {
        Random.InitState(GenerateSeed());

        //Creat array
        gridSize = new GameObject[numCols, numRows];

        //Loop
        for (int currentCol = 0; currentCol < numCols; currentCol++)
        {
            for (int currentRow = 0; currentRow < numRows; currentRow++)
            {
                //Instantiate room
                GameObject tempRoom = Instantiate(RandomRoom());

                //Instantiate enemy with room
                GameObject tempEnemy = Instantiate(RandomEnemy());

                //add to grid array
                gridSize[currentCol, currentRow] = tempRoom;

                //move into pos
                tempRoom.transform.position = new Vector3(currentCol * tileWidth, 0, -currentRow * tileHeight);
                tempEnemy.transform.position = new Vector3(currentCol * tileWidth, 1, -currentRow * tileHeight);

                //room name
                tempRoom.name = "Room (" + currentCol + "," + currentRow + ")";

                //child of object
                tempRoom.GetComponent<Transform>().parent = this.gameObject.GetComponent<Transform>();

            }
        }


        //after generate field add in player
        GameManager.instance.SpawnOnSpot(GameManager.instance.players[0], new Vector3(-0.082f, -12.60907f, 0.356f));
        CameraFallow.cameraFallow.ScanForPlayer();
    }

    public GameObject RandomRoom()
    {
        int roomIndex = Random.Range(0, roomPrefabs.Count);

        return roomPrefabs[roomIndex].gameObject;
    }
    public GameObject RandomEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Count);

        return enemyPrefabs[enemyIndex];
    }
    public int GenerateSeed()
    {
        switch (mapOfTheDay)
        {
            case true:
                GenerateMapOfTheDay();
                break;
        }
        var temp = Random.state;

        if (!seedGenInit)
        {
            Random.InitState(genSeed);
            seedGenerator = Random.state;
            seedGenInit = true;
        }

        Random.state = seedGenerator;

        var generatedSeed = Random.Range(int.MinValue, int.MaxValue);

        seedGenerator = Random.state;

        Random.state = temp;

        return generatedSeed;
    }

    public void GenerateMapOfTheDay()
    {
        genSeed = GetCurrentDay();
    }

    public int GetCurrentDay()
    {
        string day = DateTime.Now.Day.ToString();
        string month = DateTime.Now.Month.ToString();
        string year = DateTime.Now.Year.ToString();
        string seed = day + month + year;

        int seedVal = Convert.ToInt32(seed);

        return seedVal;
    }

}
