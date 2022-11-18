using Persistence;
using UnityEngine;

public class DevPanel : MonoBehaviour
{
    public static System.Action<PlayerData> ChangeData;
    public static System.Action<PlayerData> ChangeButtons;

    public void OpenLevels()
    {
        PlayerData newData;

        if (PreloadScrInstaller.usePlayerPrefs)
        {
            newData = new PlayerData();
            newData.levels = new LevelInfo[PreloadScrInstaller.levelsCount];

            for (int i = 0; i < PreloadScrInstaller.levelsCount; i++)
            {
                newData.levels[i] = new LevelInfo(true, 1);
            }

            PlayerPrefs.SetString("PlayerData", JsonUtility.ToJson(newData));
        }
        else
        {
            TextAsset ass = (TextAsset)Resources.Load($"DataSamples/Dev/player_data-opened", typeof(TextAsset));
            newData = JsonUtility.FromJson<PlayerData>(ass.text);
            DataHandler.SaveNewDataInFileAsync(newData);
        }

        ChangeData?.Invoke(newData);
        ChangeButtons?.Invoke(newData);
    }

    public void ResetLevels()
    {
        PlayerData newData;

        if (PreloadScrInstaller.usePlayerPrefs)
        {
            newData = new PlayerData();
            newData.levels = new LevelInfo[ PreloadScrInstaller.levelsCount ];

            for (int i = 0; i < PreloadScrInstaller.levelsCount; i++)
            {
                newData.levels[i] = new LevelInfo(false, 0);
            }

            PlayerPrefs.SetString("PlayerData", JsonUtility.ToJson(newData));
        }
        else
        {
            TextAsset ass = (TextAsset)Resources.Load($"DataSamples/Dev/player_data-opened", typeof(TextAsset));
            newData = JsonUtility.FromJson<PlayerData>(ass.text);
            DataHandler.SaveNewDataInFileAsync(newData);
        }

        ChangeData?.Invoke(newData);
        ChangeButtons?.Invoke(newData);
    }
}
