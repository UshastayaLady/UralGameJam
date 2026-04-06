using System;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private MainMenuUI _uiScenePrefab;

    public Action OnLoadGameplayScene;

    public void Run(UIRootView uiRoot)
    {
        var uiScene = Instantiate(_uiScenePrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        uiScene.OnPlayButtonClick += ()  => OnLoadGameplayScene?.Invoke();
        uiScene.Initialize();
        MusicManager.Instance.PlayMusic();
    }
}
