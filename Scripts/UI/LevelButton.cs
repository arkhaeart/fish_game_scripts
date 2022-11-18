using GameSystems;
using Persistence;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public GameObject[] fullStars;
    public GameObject levelComplete;
    [HideInInspector] public LevelNumbers levelNumbers;
    public LevelInfo Info { get; set; }

    public static System.Action<int> OnLevelChosen;

    public void Clicked()
    {
        OnLevelChosen(levelNumbers.BuildIndex);
    }

    public void SetInteractable(LevelButton prevButton)
    {
        if (prevButton == null || Info.isCompleted)
        {
            GetComponent<Button>().interactable = true;
            levelComplete.SetActive(Info.isCompleted);

            for (int i = 0; i < Info.stars; i++)
            {
                fullStars[i].SetActive(true);
            }
        }
        else
        {
            GetComponent<Button>().interactable = prevButton.Info.isCompleted;
        }
    }
}

public enum CreatedFrom
{
    buildIndex,
    iterator
}

public struct LevelNumbers
{
    public LevelNumbers(int levelNumber, CreatedFrom createdFrom)
    {
        if (createdFrom == CreatedFrom.iterator)
        {
            LevelInRender = levelNumber + 1;
            BuildIndex = levelNumber + GameManager.utilScenes;
            ArrayIndex = levelNumber;
        }
        else if (createdFrom == CreatedFrom.buildIndex)
        {
            LevelInRender = levelNumber - GameManager.utilScenes + 1;
            BuildIndex = levelNumber;
            ArrayIndex = levelNumber - GameManager.utilScenes;
        }
        else
        {
            LevelInRender = 0;
            BuildIndex = 0;
            ArrayIndex = 0;
        }
    }

    public int LevelInRender { get; }
    public int BuildIndex { get; }
    public int ArrayIndex { get; }

    public static explicit operator LevelNumbers(int levelNumber)
    {
        return new LevelNumbers(levelNumber, CreatedFrom.iterator);
    }
}
