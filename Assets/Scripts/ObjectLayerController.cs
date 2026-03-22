using UnityEngine;

public class ObjectLayerController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            transform.position -= new Vector3(0, 0, 2);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            transform.position += new Vector3(0, 0, 2);
    }
}
