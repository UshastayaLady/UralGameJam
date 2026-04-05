using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class AnimatiorPlayer : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _sprite;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        InputPlayer.PlayerMoved += MoveAnimatin;
    }
    private void MoveAnimatin(float directionX, float directionY)
    {

        if (directionX < 0)
            _sprite.flipX = false;
        else if (directionX > 0)
            _sprite.flipX = true;
         
        _animator.SetFloat("directionY", directionY);        
        _animator.SetFloat("directionX", directionX);
    }
    private void OnDisable()
    {
        InputPlayer.PlayerMoved -= MoveAnimatin;
    }
}
