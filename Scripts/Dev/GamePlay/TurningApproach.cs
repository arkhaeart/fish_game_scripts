using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningApproach : MonoBehaviour
{
    public Direction[] possibleDirections;
    private TurningZone parent;

    public void SetParent(TurningZone parent)
    {
        this.parent = parent;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (parent == null)
        {
            TurningZone parent = GetComponentInParent<TurningZone>();
            SetParent(parent);
        }

        if (collision.CompareTag("Player"))
        {
            parent.PlayerEntered(this);
        }
    }
}
