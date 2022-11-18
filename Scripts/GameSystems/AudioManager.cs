using UnityEngine;
using Zenject;

public class AudioManager
{
    private const string SOUND = "soundAvailable";

    public bool GetSound() => !PlayerPrefs.HasKey(SOUND) || PlayerPrefs.GetInt(SOUND) == 1 ? false : true;

    public bool ToggleSound()
    {
        if (!PlayerPrefs.HasKey(SOUND) || PlayerPrefs.GetInt(SOUND) == 1)
        {
            PlayerPrefs.SetInt(SOUND, 0);
            return true;
        }
        else
        {
            PlayerPrefs.SetInt(SOUND, 1);
            return false;
        }
    }
}
