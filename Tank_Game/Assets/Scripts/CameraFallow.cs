using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    public static CameraFallow cameraFallow;

    public Tank_Data data;

    public GameObject target;

    public Vector3 offset;

    private readonly Space offsetPositionSpace = Space.Self;

    private readonly bool lookAt = true;

   

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Turret>().gameObject;
        data = FindObjectOfType<Tank_Data>();
        //grabs both turret script and tank data script. 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            ScanForPlayer();
        }
        Refresh();
    }

    public void Refresh()//This code is to allow the camera to fallow the tank wile its moving around. 
    {
       if (offsetPositionSpace == Space.Self)
        {
            transform.position = target.transform.TransformPoint(offset);
        }
        else
        {
            transform.position = target.transform.position + offset;
        }

       if (lookAt)
        {
            transform.LookAt(target.transform);
        }
        else
        {
            transform.rotation = target.transform.rotation;
        }
    }
    public void ScanForPlayer()

    {

        target = FindObjectOfType<Turret>().gameObject;

        data = FindObjectOfType<Tank_Data>(); //Grabs object with TankData script

    }
}
