using System;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private GameplayUI _gameplayUI;

    public Action OnLoadMainMenuScene;

    public void Run(UIRootView uiRoot)
    {
        var gameplayUI = Instantiate(_gameplayUI);
        uiRoot.AttachSceneUI(gameplayUI.gameObject);

        gameplayUI.Menu.OnExitButtonClick += () => OnLoadMainMenuScene?.Invoke();
    }
}
