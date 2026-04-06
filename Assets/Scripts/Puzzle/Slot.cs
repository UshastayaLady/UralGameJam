using UnityEngine;

public class Slot
{
    public bool IsEmpty = true;
    public Vector2Int GridPos;
    public Vector2 WorldPos;

    public int CorrectID = 0;
    public int CurrentPuzzleID = 1000;

    public int CurrentRotatePuzzle = 1000;

    public bool IsCorrect => CorrectID == CurrentPuzzleID && CurrentRotatePuzzle == 0;
}
