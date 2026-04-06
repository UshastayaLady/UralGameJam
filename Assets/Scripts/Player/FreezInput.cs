using UnityEngine;

public class FreezInput : MonoBehaviour
{

    private InputPlayer _player;

    private void OnEnable()
    {
        _player = FindAnyObjectByType<InputPlayer>();
        _player.FreezInput(true);
    }


    private void OnDisable()
    {
        _player.FreezInput(false);
    }
}
