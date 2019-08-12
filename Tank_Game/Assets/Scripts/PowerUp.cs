using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public float duration;



    /// <summary>

    /// What to do when the Power Up is applied to a specific object.

    /// </summary>

    public abstract void OnApply(GameObject obj, GameObject source);



    /// <summary>

    /// What to do when the Power Up is removed from a specific object.

    /// </summary>

    public abstract void OnRemove(GameObject obj, GameObject source);


}
