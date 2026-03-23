using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider _audioEffectsVolume;
    [SerializeField] private Slider _musicManager;
    [SerializeField] private TMP_Dropdown _resolutions;

    public void Initialize()
    {
        _audioEffectsVolume.value = SaveManager.Instance.GetVolumeSFX();
        _musicManager.value = SaveManager.Instance.GetVolumeMusic();

        _audioEffectsVolume.onValueChanged.AddListener(value => { AudioEffectsManager.Instance.SetVolume(value); 
            SaveManager.Instance.SaveSFXVolume(value); });

        _musicManager.onValueChanged.AddListener(value => { MusicManager.Instance.SetVolume(value); 
            SaveManager.Instance.SaveMusicVolume(value); });

        _resolutions.onValueChanged.AddListener(x => ChangeScreenResolution(x));
        _resolutions.value = SaveManager.Instance.GetResolution();
    }

    private void ChangeScreenResolution(int y)
    {
        var check = _resolutions.options[y].text;

        string[] parts = check.Split('x');

        int width = int.Parse(parts[0]);
        int height = int.Parse(parts[1]);

        SaveManager.Instance.SaveResolution(y);
        Screen.SetResolution(width, height, FullScreenMode.FullScreenWindow);
    }
}
