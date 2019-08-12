using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{
    public Transform ePointOfFire;
    public Bullet_Move eBulletPrefab;

    public Timer timer;

   

    // Update is called once per frame
   private void Update()
    {
        eBulletPrefab.transform.position = ePointOfFire.position;
        eBulletPrefab.transform.rotation = ePointOfFire.rotation;
       


        if (GameManager.instance.enemyHealth <= 0)
        {//If health hits zero destroy object. 
            Destroy(gameObject);
        }
        
    }

    public void ShootOutObject(GameObject prefab)
    {//Prefab for enemy bullet. 
        prefab = eBulletPrefab.gameObject;
        Instantiate(prefab);
    }
}
