using UnityEngine;
using UnityEngine.EventSystems;


public class InspectableObject : MonoBehaviour, IPointerClickHandler
{
    [Header("Настройки осмотра")]
    [SerializeField] private float _inspectScale = 2.5f;      
    [SerializeField] private float _moveSpeed = 8f;           
    [SerializeField] private float _rotationSpeed = 200f;     

    private Vector3 _originalPosition;
    private Vector3 _originalScale;
    private Quaternion _originalRotation;
    private bool _isInspecting = false;
    private Camera _mainCamera;
    private SpriteRenderer _spriteRenderer;

    private Vector3 _targetPosition;

    void Start()
    {
        _mainCamera = Camera.main;
        SaveOriginalTransform();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void SaveOriginalTransform()
    {
        _originalPosition = transform.position;
        _originalScale = transform.localScale;
        _originalRotation = transform.rotation;
    }


    void Update()
    {
        if (_isInspecting)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, _originalScale * _inspectScale, _moveSpeed * Time.deltaTime);       
        }
    }

    void StartInspection()
    {
        SaveOriginalTransform();

        _spriteRenderer.sortingOrder = 100;

        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        _targetPosition = _mainCamera.ScreenToWorldPoint(screenCenter);
        _targetPosition.z = transform.position.z;

        _isInspecting = true;
    }

    void EndInspection()
    {
        _isInspecting = false;

        transform.position = _originalPosition;
        transform.localScale = _originalScale;
        transform.rotation = _originalRotation;
        _spriteRenderer.sortingOrder = 5;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isInspecting)
        {
            StartInspection();
        }
        else
        {
            EndInspection();
        }
    }

}


