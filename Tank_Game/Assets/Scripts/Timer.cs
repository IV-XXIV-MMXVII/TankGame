using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Timer : MonoBehaviour
{
    /// <summary>
    /// The time since the timer was activated.
    /// </summary>
    public float[] currentTime;
    [HideInInspector] public float[] resetTime;
    public bool[] timeStarted; //If the time has started

    private void Start()
    {
        Initialize();
    }

    protected void Update()
    {
        RunTimers();
    }

    private void RunTimers()
    {
        #region Run Timers
        ////Current time, when initialized, is set to 0. At the start of the game, assign the value of current time to reset time.
        for (int i = 0; i < timeStarted.Length; i++)
        {
            if (timeStarted[i]) currentTime[i] += Time.deltaTime;
        }
        #endregion
    }

    ///<summary>
    ///Activate a timer by index
    ///</summary>
    public void StartTimer(int index)
    {
        //Starts the timer
        timeStarted[index] = true;
        RunTimers();
    }

    ///<summary>
    ///Reset a timer by index
    ///</summary>
    public void ResetTime(int index, bool continueTimer)
    {
        //Stops the timers, and returns the resetTime value back to the currentTime.
        switch (continueTimer)
        {
            case false:
                timeStarted[index] = false;
                break;
        }
        currentTime[index] = resetTime[index];
    }

    private void Initialize()
    {
        #region Initiate Timers
        currentTime = new float[12];
        resetTime = new float[12];
        timeStarted = new bool[12];

        //Current time, when initialized, is set to 0. At the start of the game, assign the value of current time to reset time.
        for (int i = 0; i < resetTime.Length; i++)
        {
            resetTime[i] = currentTime[i];
        }
        #endregion
    }
}