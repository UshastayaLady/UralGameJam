using System;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private float directionX;
    private float directionY;

    public static event Action<float, float> PlayerMoved;

    private void Update()
    {
        DownButtonMove();
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
}
