using System;

[Serializable]
public class SaveData
{
    public bool[] PassedLevels;
    public bool[] CompletedAchievements;

    public int ResolutionID = 0;

    public float VolumeSFX = 0.5f;
    public float VolumeMusic = 0.5f;
}
