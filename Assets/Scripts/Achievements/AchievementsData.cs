using UnityEngine;

[CreateAssetMenu(fileName ="Achievement", menuName ="UralGameJam/Achievement Data", order = 0)]
public class AchievementsData : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    public bool IsAchieved;
    public int Id;
}
