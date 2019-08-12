using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Move : MonoBehaviour
{
    public float bulletSpeed;//speed of the bullet
    public Transform tf;//bullet transform
    public Shoot shoot;//shoot script
    public Timer timer;//timer
    public Tank_Data data;//tank data
    public bool enemySide;//if bullet is coming for player/enemy
    public float destroyDuration;//How long until bullet gets destroyed
    

    private void Awake()
    {
        //Goes through each one and finds the components
        shoot = FindObjectOfType<Shoot>();
        timer = FindObjectOfType<Timer>();
        data = FindObjectOfType<Tank_Data>();

    }
    
    // Start is called before the first frame update

    // Update is called once per frame
   private void Update()
    {//The amount of time before bullet destroys itself. 
        DestroyDuration(destroyDuration);
        Move();
    }
    public void Move()
    {//bullet speed moving foward. 
        tf.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }
    public void DestroyDuration(float seconds)
    {//This is part of the script for the timer.
        timer.StartTimer(11);
        if (timer.currentTime[11] > seconds)
        {
            timer.ResetTime(11, false);//resets the time 
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (enemySide)
        {//obsticle will destroy game object. 
            case false:
                if (other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Enemy")
                {
                    if (other.gameObject.GetComponent<AIController>() != null)
                    {
                        AIController enemy = other.gameObject.GetComponent<AIController>();
                        enemy.ChangeAttackState(AIController.AiAttackState.Attack);
                    }
                    Destroy(gameObject);//destroy game object. 
                }
                break;
            case true:
                if (other.gameObject.name == "SD_Firefly")
                {//player health can be taken by enemy AI. 
                    //GameManager.instance.playerHealth = GameManager.instance.LoseHealth(GameManager.instance.playerHealth, 10f);
                }
                break;
        }
    }
}
