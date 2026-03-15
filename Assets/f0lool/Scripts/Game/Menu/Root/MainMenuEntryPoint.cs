using System;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private MainMenuUI _uiScenePrefab;

    public Action LoadGameplayScene;

    public void Run(UIRootView uiRoot)
    {
        var uiScene = Instantiate(_uiScenePrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        uiScene.OnPlayButtonClick += ()  => LoadGameplayScene?.Invoke();
        uiScene.Initialize();
    }
}
