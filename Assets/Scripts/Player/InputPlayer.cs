using System;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    private KeyCode interactiveInput = KeyCode.E;
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private float directionX;
    private float directionY;

    public static event Action<float, float> PlayerMoved;
    public static event Action launchedInteractiveZone;

    private void Update()
    {
        DownButtonMove();
        DownButtonInteractiveZone();
    }
    private void FixedUpdate()
    {
        PlayerMoved?.Invoke(directionX, directionY);
    }
    private void DownButtonMove()
    {
        directionX = Input.GetAxis(Horizontal);
        directionY = Input.GetAxis(Vertical);        
    }

    private void DownButtonInteractiveZone()
    {
        if (Input.GetKeyDown(interactiveInput))
        {
            Debug.Log("Input KeyDown");
            launchedInteractiveZone?.Invoke();
        }
    }
}
