using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCheck : MonoBehaviour
{
    public static System.Action OnTurn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("turn checking collishion: " + collision);
        Debug.Log("colision parent: " + collision.transform.parent.name);

        if (collision.tag == "Turn")
        {
            OnTurn?.Invoke();
        }
    }
}
