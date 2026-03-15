using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Action OnPlayButtonClick;

    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnOpenSettings;
    [SerializeField] private GameObject _panelSettings;
    [SerializeField] private Button _btnCloseSettings;

    public void Initialize()
    {
        _btnPlay.onClick.AddListener(() => OnPlayButtonClick?.Invoke());
        _btnOpenSettings.onClick.AddListener(() => OpenSettings());
        _btnCloseSettings.onClick.AddListener(() => CloseSettings());
    }

    private void OpenSettings()
    {
        _panelSettings.SetActive(true);
    }

    private void CloseSettings()
    {
        _panelSettings.SetActive(false);
    }
}
