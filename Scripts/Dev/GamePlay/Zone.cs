using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public static System.Action OnPlayerFail;
    protected void Fail()
    {
        OnPlayerFail?.Invoke();
    }
}
