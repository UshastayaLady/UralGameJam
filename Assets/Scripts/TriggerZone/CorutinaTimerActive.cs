using System.Collections;
using UnityEngine;

public class CorutinaTimerActive : MonoBehaviour
{
    [SerializeField] private float _timeWait;

    private void OnEnable()
    {
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(_timeWait);
        gameObject.SetActive(false);
    }
}
