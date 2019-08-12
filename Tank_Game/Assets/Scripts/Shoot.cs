using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public Transform pointOfFire; //The transform of our point of fire
    public Bullet_Move bulletPrefab; //Reference the BulletMover component
    public Tank_Data data;

    public Timer timer;

    public bool canShoot = true;

    public int waitVal = 1;

    private void Update()
    {

        bulletPrefab.gameObject.transform.position = pointOfFire.position; //Update the position, and apply it to our bullet
        bulletPrefab.gameObject.transform.rotation = pointOfFire.rotation;  //Update the rotation, and apply it to our bullet
        if (!canShoot) waitVal = Wait(data.shotsPerSecond);
    }

    public void ShootOutObject(GameObject prefab)
    {
        if (canShoot)
        {
            Instantiate(prefab); //Create our bullet prefab
            canShoot = false;
        }
        
    }

    public void InitiateEnemyControls(float secondsUntilShoot)
    {
        timer.StartTimer(2); //Start our timer
        if (timer.currentTime[2] > secondsUntilShoot) //If timer's current time is greater than our set duration
        {
            ShootOutObject(bulletPrefab.gameObject); //Shoot a bullet
            timer.ResetTime(2, false); //Reset the timer
        }
    }

    public int Wait(float seconds)
    {
        timer.StartTimer(3);
        UpdateCanShoot();
        if (timer.currentTime[3] > seconds)
        {
            timer.ResetTime(3, false);
            return 1;
        }
        return 0;
    }

    public void UpdateCanShoot()
    {
        if (waitVal == 1) canShoot = true;
    }
}
    

