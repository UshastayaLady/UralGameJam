using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class RigidbodyMove : MonoBehaviour
{
    private Rigidbody2D rigbody2D; 

    [Header("Horizontal Transform")]
    [SerializeField] private float speedGo = 3;
    private const string Horizontal = nameof(Horizontal);    
    private float directionX;

        

    void Awake()
    {
        rigbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        DownButton();
    }

    private void DownButton()
    {
        directionX = Input.GetAxis(Horizontal);
    }

    private void FixedUpdate()
    {
        Move();      
    }

    private void Move()
    {
        rigbody2D.linearVelocityX = directionX * speedGo;       
    }  

    public float GetDirectionX()
    {
        return directionX;
    }
}
