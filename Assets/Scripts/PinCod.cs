using UnityEngine;
using UnityEngine.UI;

public class PinCod : MonoBehaviour
{
    [SerializeField] private string _pincode;   
    [SerializeField] private GameObject _activeObject;
    [SerializeField] private Text _text;
    private string _writePin;


    public void AddPin(int number)
    {
        _writePin = _writePin + $"{number}";
        _text.text = _writePin;
        if (_writePin.Length >= 4)
        {
            if (_pincode.Equals(_writePin))
            {
                _activeObject.SetActive(true);
                enabled = false;
            }
            else 
            {
                _writePin = "";
            }
        }        
    }
}
