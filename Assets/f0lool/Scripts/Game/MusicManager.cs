using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager _instance;

    [SerializeField] private AudioSource _musicSource;

    public static MusicManager Instance => _instance;

    public void Initialize()
    {
        if(_instance == null)
        {
            _instance = this;
        } else
        {
            Destroy(_instance.gameObject);
            _instance = this;
        }

        SetVolume(1f);
    }

    public void SetVolume(float volume)
    {
        _musicSource.volume = volume;
    }
}
