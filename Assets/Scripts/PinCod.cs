using UnityEngine;

public class PinCod : MonoBehaviour
{
    [SerializeField] private string _pincode;   
    [SerializeField] private GameObject activeObject;
    private string _writePin;

    protected void OnEnable()
    {
        AnswerClick.ClickedAnswer -= AddPin;
    }

    private void AddPin(int number)
    {
        _writePin = _writePin + $"{number}";
        if (_writePin.Length >= 4)
        {
            if (_pincode.Equals(_writePin))
            {
                activeObject.SetActive(true);
                enabled = false;
            }
            else 
            {
                _writePin = "";
            }
        }        
    }

    protected void OnDisable()
    {
        AnswerClick.ClickedAnswer -= AddPin;
    }
}
