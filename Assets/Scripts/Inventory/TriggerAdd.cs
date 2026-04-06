using UnityEngine;
using UnityEngine.UI;

public class TriggerAdd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Item>())
        {
            collision.gameObject.GetComponent<AxisDragableObject>().enabled = false;
            collision.gameObject.GetComponent<Button>().enabled = true;
            collision.gameObject.GetComponent<Item>().enabled = true;
        }
    }
}
