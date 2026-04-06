using UnityEngine;

public class Zagluska : MonoBehaviour
{
    [SerializeField] private Zagluska2 zagluska2;
    private void OnEnable()
    {
        zagluska2.Add();
    }
}
