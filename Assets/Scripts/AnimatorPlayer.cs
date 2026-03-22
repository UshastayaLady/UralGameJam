using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatiorPlayer : MonoBehaviour
{
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        PlayerInput.PlayerMoved += MoveAnimatin;
    }
    private void MoveAnimatin(float directionX, float directionY)
    {
        
    }
    private void OnDisable()
    {
        PlayerInput.PlayerMoved -= MoveAnimatin;
    }
}
