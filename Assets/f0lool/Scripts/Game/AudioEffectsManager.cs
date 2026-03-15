using UnityEngine;

public class AudioEffectsManager : MonoBehaviour
{
    private static AudioEffectsManager _instance;

    [SerializeField] private AudioSource _audioSource;

    public static AudioEffectsManager Instance => _instance;

    public void Initialize()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(_instance.gameObject);
            _instance = this;
        }

        SetVolume(1f);
    }

    public void SetVolume(float volume)
    {
        _audioSource.volume = volume;
    }
}
