using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider _audioEffectsVolume;
    [SerializeField] private Slider _musicManager;

    private void Awake()
    {
        _audioEffectsVolume.value = 1f;
        _musicManager.value = 1f;
        _audioEffectsVolume.onValueChanged.AddListener(value => AudioEffectsManager.Instance.SetVolume(value));
        _musicManager.onValueChanged.AddListener(value => MusicManager.Instance.SetVolume(value));
    }
}
