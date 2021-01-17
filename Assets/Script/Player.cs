using UnityEngine;
public class Player : MonoBehaviour
{
    public Game game;

    private void Update()
    {
        if (game.isStart && !game.isGameOver && Input.GetMouseButtonDown(0))
            FlipPlayer();
    }

    private void FlipPlayer()
    {
        GetComponent<SpriteRenderer>().flipY = !GetComponent<SpriteRenderer>().flipY;
        game.turn_speed *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "granade")
            game.isGameOver = true;
    }
}
