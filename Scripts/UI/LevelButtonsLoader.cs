using Common;
using Persistence;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelButtonsLoader : MonoBehaviour
{
    PlayerData data;
    //LevelButton.Factory factory;
    public LevelButton[] levelButtons;

    [Inject]
    public void Init(PlayerData data/*, LevelButton.Factory factory*/)
    {
        Debug.Log($"Level buttons loader is initialising with player data: {data}");
        this.data = data;
        //this.factory = factory;

        DevPanel.ChangeButtons = PlaceButtons_dev;

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        PlaceButtons();
    }

    void PlaceButtons()
    {
        Debug.Log("Trying set data in buttons");
        if (data != null)
        {
            LevelButton prevButton = null;

            for (int i = 0; i < data.levels.Length; i++)
            {
                levelButtons[i].Info = data.levels[i];
                levelButtons[i].levelNumbers = new LevelNumbers(i, CreatedFrom.iterator);
                levelButtons[i].SetInteractable(prevButton);

                prevButton = levelButtons[i];

                if (!prevButton.Info.isCompleted && !levelButtons[i].Info.isCompleted)
                {
                    return;
                }
            }
        }
        else
        {
            Debug.Log("Could not place buttons - data is null");
        }
    }

    void PlaceButtons_dev(PlayerData newData)
    {
        data = newData;

        if(gameObject.activeSelf)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            PlaceButtons();
        }
    }
}
