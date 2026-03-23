using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;
    private AchievementsData[] _achievements;

    public event Action OnInitialized;
    public event Action<int> OnCompleteAchievement;

    public void Initialize()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(Instance);
            Instance = this;
        }

        _achievements = Resources.LoadAll<AchievementsData>("Achievements");
    }

    public void CompleteAchievement(int id)
    {
        for (int i = 0; i < _achievements.Length; i++)
        {
            if (_achievements[i].Id == id)
            {
                _achievements[i].IsAchieved = true;
                OnCompleteAchievement?.Invoke(id);
                return;
            }
        }
    }

    public AchievementsData[] GetAllAchievements()
    {
        return _achievements;
    }
}
