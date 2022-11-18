using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] Tutorial tutorial;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tutorial.TurnOnSecondPhase();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        tutorial.TurnOffSecondPhase();
    }
}
