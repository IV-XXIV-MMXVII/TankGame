using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFirePickup : MonoBehaviour
{
    public RapidFire powerup;

    void OnTriggerEnter(Collider col)

    {

        PowerController tempPUC = col.GetComponent<PowerController>();

        if (tempPUC != null)

        {

            Debug.Log("Rapid Fire Power Up");

            tempPUC.Append(powerup, powerup.colSource = col.gameObject);

            Destroy(gameObject);

        }

    }
}
