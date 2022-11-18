using System.Collections;
using UnityEngine;

public class UISound : MonoBehaviour
{
    [Space(10)]
    public AudioClip[] bubblesPopSounds;

    public void PlaySound()
    {
        GameObject tempGO = new GameObject("TempAudio");
        DontDestroyOnLoad(tempGO);

        tempGO.transform.position = Vector3.zero;
        AudioSource aSource = tempGO.AddComponent<AudioSource>(); 

        aSource.clip = bubblesPopSounds[ Random.Range(0, bubblesPopSounds.Length) ];
        aSource.pitch = Random.Range(0.9f, 1.1f);
        aSource.volume = 0.7f;
        aSource.Play();

        Destroy(tempGO, aSource.clip.length);
    }
}
