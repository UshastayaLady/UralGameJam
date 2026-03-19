using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private FindPool sample;
    private Queue <FindPool>  poolObject;
    [SerializeField] private int cauntStart;

    private void Awake()
    {
        poolObject = new Queue<FindPool>();
        StartInitializationPoolObject();
    }

    private void StartInitializationPoolObject()
    {
        for (int i = 0; i < cauntStart; i++)
        {
            AddPoolObject();
        }        
    }

    private void AddPoolObject()
    {
        FindPool newObject = Instantiate(sample);
        newObject.PutInPool += PutObgectInPool;
        AddPoolObject(newObject);
    }

    private void AddPoolObject(FindPool findPool)
    {
        findPool.transform.SetParent(container);
        findPool.gameObject.SetActive(false);
        poolObject.Enqueue(findPool);
    }

    public FindPool GetObgectInPool()
    {
        if (poolObject.Count == 0)
            AddPoolObject();
        return poolObject.Dequeue();
    }

    public FindPool GetObgectInPool(Vector2 position, Quaternion quaternion)
    {
        FindPool forWorkItem = GetObgectInPool();
        forWorkItem.transform.position = position;
        forWorkItem.transform.rotation = quaternion;
        return forWorkItem;
    }
    public void PutObgectInPool(FindPool findPool)
    {        
        AddPoolObject(findPool);
    }

}
