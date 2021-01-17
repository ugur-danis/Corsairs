using UnityEngine;
public class GranadeRemove : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "granade")
            Destroy(collision.gameObject);
    }
}
