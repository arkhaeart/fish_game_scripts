using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SoundButton : MonoBehaviour
{

    [SerializeField] private Image soundImage;
    [SerializeField] private Sprite soundAvailableSprite;
    [SerializeField] private Sprite soundUnavailableSprite;

    AudioManager audioManager;

    [Inject]
    public void Init(AudioManager audioManager)
    {
        this.audioManager = audioManager;

        AudioListener.pause = audioManager.GetSound();
        RenderSprite();
    }

    public void ToggleSound()
    { 
        AudioListener.pause = audioManager.ToggleSound();
        RenderSprite();
    }

    private void RenderSprite() 
    {
        soundImage.sprite = audioManager.GetSound() ? soundUnavailableSprite : soundAvailableSprite;
    }
}
