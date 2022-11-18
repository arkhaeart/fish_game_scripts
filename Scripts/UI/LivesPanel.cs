using GameSystems;
using UnityEngine;
using Zenject;

public class LivesPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] lives;

    [Inject]
    void Init(GameManager gameManager)
    {
        GameConfig.DecreaseLive = DisableLive;

        for (int i = 0; i < gameManager.currentConfig.lives; i++)
        {
            lives[i].SetActive(true);
        }
    }

    public void DisableLive(int liveCount)
    {
        lives[liveCount].SetActive(false);
    }
}
