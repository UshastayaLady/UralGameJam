using UnityEngine;

public class ObjectLayerController : MonoBehaviour, ITriggerZone
{   
    public void OnEnter()
    {
        transform.position -= new Vector3(0, 0, 2);
    }

    public void OnExit()
    {
        transform.position += new Vector3(0, 0, 2);
    }
}
