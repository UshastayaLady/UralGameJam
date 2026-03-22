using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class IsometricPlayerMoveController : MonoBehaviour
{
    private Rigidbody2D rigbody2D; 

    [SerializeField] private float speedGo = 3;
    
    void Awake()
    {
        rigbody2D = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        PlayerInput.PlayerMoved += Move;
    }
    private void Move(float directionX, float directionY)
    {
        rigbody2D.linearVelocityX = directionX * speedGo;
        rigbody2D.linearVelocityY = directionY * speedGo;
    }
    private void OnDisable()
    {
        PlayerInput.PlayerMoved -= Move;
    }
}
