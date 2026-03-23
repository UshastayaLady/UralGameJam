using System.Collections.Generic;
using UnityEngine;

public class AchievementsUI : MonoBehaviour
{
    [SerializeField] private Achievement _achievementPrefab;
    [SerializeField] private Transform _achievementParent;

    private List<Achievement> _achievements;

    public void Initialize()
    {
        _achievements = new List<Achievement>();
        _achievements.Clear();
        ClearAllAchievements();

        foreach (var achievement in AchievementManager.Instance.GetAllAchievements())
        {
            var achievementGO = Instantiate(_achievementPrefab, _achievementParent);
            achievementGO.Initialize(achievement);
            _achievements.Add(achievementGO);
        }
    }

    private void ClearAllAchievements()
    {
        var childCount = _achievementParent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(_achievementParent.GetChild(i).gameObject);
        }
    }

    private void UpdateAchievementsUI(int id)
    {
        foreach(var achievement in  _achievements)
        {
            if(achievement.Id == id)
            {
                achievement.CompleteAchievement();
                return;
            }
        }
    }
}
