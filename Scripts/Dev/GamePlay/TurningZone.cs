using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningZone : MonoBehaviour
{
    public static System.Action<TurningApproach,TurningPoint,bool,int> OnPlayerEntered;

    public TurningPoint point;
    public TurningApproach[] approaches;
    public bool isCheckPoint = true;
    public int index;
    bool ignoreNext = false;
    public void PlayerEntered(TurningApproach approach)
    {
        if (!ignoreNext)
        {
            ignoreNext = true;
            OnPlayerEntered?.Invoke(approach, point,isCheckPoint,index);
        }
        else
        {
            Debug.Log("Ignoring player enter");
            ignoreNext = false;
        }
    }

}
