using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCheckPointIcon : MonoBehaviour
{
    public int index;
    public void Activate(CheckPointEntry entry)
    {
        gameObject.SetActive(true);
        transform.rotation = entry.direction.ToRotation();
    }
}
