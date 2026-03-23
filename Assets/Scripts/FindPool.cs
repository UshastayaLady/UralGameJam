using System;
using UnityEngine;

public class FindPool : MonoBehaviour
{
    public event Action<FindPool> PutInPool;      

    public void EventGo()
    {
        PutInPool?.Invoke(this);
    }
}
