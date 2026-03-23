using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Action OnPlayButtonClick;

    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnQuit;

    [Header("Settings")]
    [SerializeField] private Button _btnOpenSettings;
    [SerializeField] private Settings _settings;
    [SerializeField] private Button _btnCloseSettings;

    [Header("Achievements")]
    [SerializeField] private AchievementsUI _achievementsUI;
    [SerializeField] private Button _btnOpenAchievements;
    [SerializeField] private Button _btnCloseAchievements;

    public void Initialize()
    {
        _btnPlay.onClick.AddListener(() => OnPlayButtonClick?.Invoke());
        _btnQuit.onClick.AddListener(() => Application.Quit());

        _btnOpenSettings.onClick.AddListener(() => OpenSettings());
        _btnCloseSettings.onClick.AddListener(() => CloseSettings());

        _btnOpenAchievements.onClick.AddListener(() => OpenAchievements());
        _btnCloseAchievements.onClick.AddListener(() => CloseAchievements());

        _achievementsUI.Initialize();
        _settings.Initialize();
    }

    private void OpenSettings()
    {
        _settings.gameObject.SetActive(true);
    }

    private void CloseSettings()
    {
        _settings.gameObject.SetActive(false);
    }

    private void OpenAchievements()
    {
        _achievementsUI.Initialize();
        _achievementsUI.gameObject.SetActive(true);
    }

    private void CloseAchievements()
    {
        _achievementsUI.gameObject.SetActive(false);
    }
}
