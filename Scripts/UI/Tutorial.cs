using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    GameObject firstPhase, secondPhase;

    public void TurnOffFirstPhase() => firstPhase.SetActive(false);
    public void TurnOnSecondPhase() => secondPhase.SetActive(true);
    public void TurnOffSecondPhase() => secondPhase.SetActive(false);
}
