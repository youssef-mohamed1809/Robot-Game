
using UnityEngine;

public class Key : MonoBehaviour
{

    [SerializeField] GameObject door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        door.SetActive(false);
        Destroy(this.gameObject);
    }
}
