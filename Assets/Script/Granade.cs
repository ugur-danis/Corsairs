using System.Collections;
using UnityEngine;

public class Granade : MonoBehaviour
{
    public GameObject GranadePref;
    public Transform Rocket;
    private Vector2 rocket_pos;

    public float wait_second;
    public int fire_speed;
    public int fire_speed_k = 10;

    public void Start()
    {
        StartCoroutine(TakeRocketPosition());
    }

    private IEnumerator TakeRocketPosition()
    {
        yield return new WaitForSeconds(wait_second);
        rocket_pos = Rocket.position;
        GoGranade();
    }

    private void GoGranade()
    {
        float x = rocket_pos.x - transform.position.x;
        float y = rocket_pos.y - transform.position.y;
        Vector2 RocketPos = new Vector2(x, y);

        var granade = Instantiate(GranadePref, transform.position, Quaternion.identity);
        granade.GetComponent<Rigidbody2D>().AddForce(RocketPos * (fire_speed * fire_speed_k));
        StartCoroutine(TakeRocketPosition());
    }
}
