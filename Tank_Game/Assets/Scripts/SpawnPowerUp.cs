using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUp : MonoBehaviour
{
    public List<GameObject> powerups;
    public int instanceCount = 0;

    private Timer timer;

    private void Update()
    {
        timer = GetComponent<Timer>();
        timer.StartTimer(0);
        if (timer.currentTime[0] > 5)
        {
            if (instanceCount < 1) SpawnPowerUps();
        }
    }

    public void SpawnPowerUps()
    {
        int i = Random.Range(0, powerups.Count);
        GameObject powerUp = Instantiate(powerups[i]); instanceCount++;
        powerUp.transform.position = transform.position;
    }
}
