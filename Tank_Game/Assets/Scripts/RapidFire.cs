using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using msg = UnityEngine.Debug;
[System.Serializable]
public class RapidFire : PowerUp
{
    public static RapidFire instance;



    public float val, resetVal;



    public GameObject colSource;



    public override void OnApply(GameObject obj, GameObject source = null)

    {

        if (colSource != null) source = colSource;



        Tank_Data temp = source.GetComponent<Tank_Data>();

        resetVal = temp.rapidFireVal;

        temp.rapidFireVal = val;

        if (temp.shotsPerSecond > (temp.shotsPerSecond = temp.shotsPerSecond - (temp.rapidFireVal / 10)))

        {

            var tempVal = temp.rapidFireVal / 10;

            temp.shotsPerSecond = 1 - tempVal;

        }

        ImpController.controller.canRapidFire = true;

    }



    public override void OnRemove(GameObject obj, GameObject source = null)

    {

        if (colSource != null) source = colSource;



        Tank_Data temp = source.GetComponent<Tank_Data>();

        temp.rapidFireVal = resetVal;

        temp.shotsPerSecond = temp.rapidFireVal;

        ImpController.controller.canRapidFire = false;

    }
}
