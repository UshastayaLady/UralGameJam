using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    [SerializeField] private Image _achievementImage;
    [SerializeField] private TMP_Text _achievementName;

    private bool _isAchieved = false;

    private int _id;

    public int Id => _id;

    public void Initialize(AchievementsData data)
    {
        _achievementImage.sprite = data.Icon;
        _achievementName.text = data.Name;
        _id = data.Id;

        _isAchieved = data.IsAchieved;

        if(!_isAchieved)
            gameObject.SetActive(false);
    }

    public void CompleteAchievement()
    {
        _isAchieved = true;
        gameObject.SetActive(true);
    }
}
