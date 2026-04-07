using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private SaveData _data;
    private string _savePath;

    public void Initialize()
    {
        _savePath = Application.persistentDataPath + "/save.json";

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
            Instance = this;
        }

        _data = LoadData();
        
        if(_data == null)
        {
            _data = new SaveData();
            
        }
    }

    public SaveData LoadData()
    {
        SaveData data = null;

        if (File.Exists(_savePath))
        {
            string json = File.ReadAllText(_savePath); 
            data = JsonUtility.FromJson<SaveData>(json); 
        }

        return data;
    }

    private void SaveData()
    {
        if(_data == null)
        {
            _data = new SaveData();
        }

        string json = JsonUtility.ToJson(_data); 
        File.WriteAllText(_savePath, json); 
    }

    public void SaveSFXVolume(float volume)
    {
        _data.VolumeSFX = volume;

        SaveData();
    }

    public void SaveMusicVolume(float volume)
    {
        _data.VolumeMusic = volume;

        SaveData();
    }

    public void SaveResolution(int id)
    {
        _data.ResolutionID = id;

        SaveData();
    }

    public float GetVolumeSFX()
    {
        return _data.VolumeSFX;
    }

    public float GetVolumeMusic()
    {
        return _data.VolumeMusic;
    }

    public int GetResolution()
    {
        return _data.ResolutionID;
    }
}
