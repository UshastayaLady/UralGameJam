using UnityEngine;

public class ObjectLayerController : MonoBehaviour, ITriggerZoneEnter, ITriggerZoneExit
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
