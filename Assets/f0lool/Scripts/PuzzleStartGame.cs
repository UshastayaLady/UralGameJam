using UnityEngine;

public class PuzzleStartGame : MonoBehaviour
{
    [SerializeField] private GameObject _puzzle;
    [SerializeField] private GameObject _box;
    [SerializeField] private GameObject _canvas;

    public void StartPuzzle()
    {
        _puzzle.SetActive(true);
        PuzzleManager.Instance.OnCompletePuzzle += CompletePuzzle;
        _canvas.SetActive(false);
    }

    private void CompletePuzzle()
    {
        _canvas.SetActive(true);
        _box.SetActive(true);
        gameObject.SetActive(false);
    }
}
