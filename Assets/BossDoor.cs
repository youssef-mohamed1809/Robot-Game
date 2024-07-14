using UnityEngine;

public class InvisibleDoor : MonoBehaviour
{
    public GameObject bossObject;
    public GameObject bossDoor;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (bossObject != null && bossDoor != null)
            {
                bossObject.SetActive(true);
                bossDoor.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.gameObject != null)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
