using System;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    private KeyCode _interactiveInput = KeyCode.E;
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private float _directionX;
    private float _directionY;

    private bool _freezPlayer = false;
    private int _countFreezPlayer = 0;

    private bool _freezInput = false;
    private int _countFreezInput = 0;

    public static event Action<float, float> PlayerMoved;
    public static event Action launchedInteractiveZone;

    private void Update()
    {
        if (!_freezPlayer)
            DownButtonMove();
        if (!_freezInput)
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
            if (_freezPlayer != true)
            {
                _directionX = 0;
                _directionY = 0;
                _freezPlayer = true;
            }
            _countFreezPlayer++;
        }
        else
        {
            _countFreezPlayer--;
            if (_countFreezPlayer == 0)
            {
                _freezPlayer = false;
            }
        }
    }

    private void CounterInput(bool freez)
    {
        if (freez == true)
        {
            if (_freezInput != true)
            {
                _freezInput = true;
            }
            _countFreezInput++;
        }
        else
        {
            _countFreezInput--;
            if (_countFreezInput == 0)
            {
                _freezInput = false;
            }
        }
    }

    public void FreezPlayer(bool freez)
    {
        CounterPlayer(freez);
    }

    public void FreezInput(bool freez)
    {
        CounterInput(freez);
    }
}
