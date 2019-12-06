using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Timer
{
    private bool timing;

    public bool Counter(ref float time, float baseTime)
    {
        //Guarante that the timer is updated
        if (!timing)
        {
            time = baseTime;
            timing = true;
        }
        //Verifies if countdown is over
        if (time < 0)
        {
            time = baseTime;
            timing = false;
            return false;
        }
        //Countdown
        else
        {
            time -= Time.fixedDeltaTime;
            return true;
        }
    }
}   
