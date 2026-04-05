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
        InputPlayer.PlayerMoved += MoveAnimatin;
    }
    private void MoveAnimatin(float directionX, float directionY)
    {
        animator.SetFloat("directionY", directionY);
        animator.SetFloat("directionX", directionX);
    }
    private void OnDisable()
    {
        InputPlayer.PlayerMoved -= MoveAnimatin;
    }
}
