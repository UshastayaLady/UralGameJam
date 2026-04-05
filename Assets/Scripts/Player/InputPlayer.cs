using System;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    private KeyCode _interactiveInput = KeyCode.E;
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private float _directionX;
    private float _directionY;

    private bool _freez = false;
    private int _countFreez = 0;

    public static event Action<float, float> PlayerMoved;
    public static event Action launchedInteractiveZone;

    private void Update()
    {
        if (!_freez)
            DownButtonMove();
        DownButtonInteractiveZone();
    }
    private void FixedUpdate()
    {
        PlayerMoved?.Invoke(_directionX, _directionY);
    }
    private void DownButtonMove()
    {
        _directionX = Input.GetAxis(Horizontal);
        _directionY = Input.GetAxis(Vertical);        
    }

    private void DownButtonInteractiveZone()
    {
        if (Input.GetKeyDown(_interactiveInput))
        {
            launchedInteractiveZone?.Invoke();
        }
    }

    private void CounterPlayer(bool freez)
    {
        if (freez == true)
        {
            if (_freez != true)
            {
                _freez = true;
            }
            _countFreez++;
        }
        else
        {
            _countFreez--;
            if (_countFreez == 0)
            {
                _freez = false;
            }
        }
    }


    public void FreezPlayer(bool freez)
    {
        CounterPlayer(freez);
    }
}
