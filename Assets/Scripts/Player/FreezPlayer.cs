using UnityEngine;

public class FreezPlayer : MonoBehaviour
{
    private InputPlayer _player;

    private void OnEnable()
    {
        _player = FindAnyObjectByType<InputPlayer>();
        _player.FreezPlayer(true);
    }
    

    private void OnDisable()
    {
        _player.FreezPlayer(false);
    }
}
