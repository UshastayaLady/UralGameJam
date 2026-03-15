using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint
{
    private static GameEntryPoint _instance;
    private Coroutine _coroutine;
    private UIRootView _uiRoot;
    private AudioEffectsManager _audioEffectsManager;
    private MusicManager _musicManager;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AutostartGame()
    {
        _instance = new GameEntryPoint();
        _instance.StartGame(); 
    }

    private GameEntryPoint()
    {
        _coroutine = new GameObject("[COROUTINES]").AddComponent<Coroutine>();
        Object.DontDestroyOnLoad(_coroutine.gameObject);

        var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
        _uiRoot = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(_uiRoot.gameObject);
        
        var audioEffectsManager = Resources.Load<AudioEffectsManager>("AudioEffectsManager");
        _audioEffectsManager = Object.Instantiate(audioEffectsManager);
        Object.DontDestroyOnLoad(_audioEffectsManager.gameObject);

        var musicManager = Resources.Load<MusicManager>("MusicManager");
        _musicManager = Object.Instantiate(musicManager);
        Object.DontDestroyOnLoad(_musicManager.gameObject);

        _audioEffectsManager.Initialize();
        _musicManager.Initialize();
    }

    public void StartGame()
    {
#if UNITY_EDITOR
        var sceneName = SceneManager.GetActiveScene().name;

        if(sceneName == Scenes.MAIN_MENU)
        {
            _coroutine.StartCoroutine(LoadAndStartMainMenu());

            return;
        }

        if(sceneName != Scenes.BOOT)
        {
            return;
        }
#endif

        _coroutine.StartCoroutine(LoadAndStartMainMenu());
    }

    private IEnumerator LoadAndStartMainMenu()
    {
        _uiRoot.ShowLoadingScreen();

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MAIN_MENU);

        yield return new WaitForSeconds(2);

        var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
        sceneEntryPoint.Run(_uiRoot);

        sceneEntryPoint.LoadGameplayScene += (() => _coroutine.StartCoroutine(LoadAndStartGameplay()));

        _uiRoot.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartGameplay()
    {
        _uiRoot.ShowLoadingScreen();

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAMEPLAY);

        yield return new WaitForSeconds(2);

        var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
        sceneEntryPoint.Run(_uiRoot);

        _uiRoot.HideLoadingScreen();
    }

    private IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}
