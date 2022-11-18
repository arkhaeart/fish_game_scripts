using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fps60 : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}
