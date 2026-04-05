using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class IsometricPlayerMoveController : MonoBehaviour
{
    private Rigidbody2D rigbody2D; 

    [SerializeField] private float speedGoY = 3;
    [SerializeField] private float speedGoX = 4;

    [Header("Scale by Y position")]
    [SerializeField] private float minY = -5f;      // самая нижняя позиция (близко к камере)
    [SerializeField] private float maxY = 5f;       // самая верхняя позиция (далеко от камеры)
    [SerializeField] private float maxScale = 1.5f; // масштаб, когда близко (Y = minY)
    [SerializeField] private float minScale = 0.5f; // масштаб, когда далеко (Y = maxY)
    void Awake()
    {
        rigbody2D = GetComponent<Rigidbody2D>();
        rigbody2D.gravityScale = 0;
    }
    private void OnEnable()
    {
        InputPlayer.PlayerMoved += Move;
    }
    private void Move(float directionX, float directionY)
    {
        rigbody2D.linearVelocityX = directionX * speedGoX;
        rigbody2D.linearVelocityY = directionY * speedGoY;
        UpdateScale();
    }

    private void UpdateScale()
    {
        // Нормализуем Y в диапазон [minY, maxY] -> получаем t от 0 до 1
        // t = 0 при Y = minY (близко), t = 1 при Y = maxY (далеко)
        float t = Mathf.InverseLerp(minY, maxY, transform.position.y);

        // Масштаб
        float newScale = Mathf.Lerp(maxScale, minScale, t);
                
        transform.localScale = new Vector3(newScale, newScale, transform.localScale.z);
    }

    private void OnDisable()
    {
        InputPlayer.PlayerMoved -= Move;
    }
}
