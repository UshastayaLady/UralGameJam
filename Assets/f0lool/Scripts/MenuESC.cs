using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuESC : MonoBehaviour
{
    public Action OnExitButtonClick;

    [SerializeField] private Button _btnExit;
    [SerializeField] private Button _btnClose;

    [Header("Settings")]
    [SerializeField] private Button _btnOpenSettings;
    [SerializeField] private Settings _settings;
    [SerializeField] private Button _btnCloseSettings;

    private void Awake()
    {
        _btnOpenSettings.onClick.AddListener(OpenSettings);
        _btnCloseSettings.onClick.AddListener(CloseSettings);
        
        _btnClose.onClick.AddListener(CloseMenu);
        _settings.Initialize();
        _btnExit.onClick.AddListener(() => OnExitButtonClick?.Invoke());
    }

    private void OpenSettings()
    {
        
        _settings.gameObject.SetActive(true);
    }

    private void CloseSettings()
    {
        _settings.gameObject.SetActive(false);
    }

    private void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
