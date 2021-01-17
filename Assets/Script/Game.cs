using UnityEngine;
using UnityEngine.UI;
public class Game : MonoBehaviour
{
    public Player player;

    // Game Status Check
    //[HideInInspector]
    public bool isStart;
    //[HideInInspector]
    public bool isGameOver;

    // Text Object
    [Header("Text")]
    public Text StartTxt;
    public Text TimerTxt;
    public Text BestTimeTxt;

    //Granade Spawn Point
    public GameObject[] GranadeSpawnPoint;

    public int turn_speed;
    private float timer;

    private void Start()
    {
        player.enabled = false;

        isStart = false;
        isGameOver = false;
        turn_speed = 150;
        timer = 0;

        StartTxt.text = "Press to start";
        TimerTxt.text = timer.ToString("00");
        BestTimeTxt.text = PlayerPrefs.GetFloat("BestTime").ToString("00");
        if (BestTimeTxt.text == "") BestTimeTxt.text = "0";
    }

    private void Update()
    {
        if ((!isStart && !isGameOver) && Input.GetMouseButtonDown(0))
            GameStart();
        if (isStart && isGameOver)
            GameOver();
        else if (isStart && !isGameOver)
        {
            timer += Time.deltaTime;
            TimerTxt.text = timer.ToString("00");
        }

        if (!isStart && isGameOver && Input.GetMouseButtonDown(0))
            GameStart();
    }

    private void FixedUpdate()
    {
        if (isStart && !isGameOver)
            Turn();
    }

    private void GameStart()
    {
        isStart = true;
        isGameOver = false;
        player.enabled = true;
        StartTxt.text = string.Empty;

        timer = 0;
        TimerTxt.text = timer.ToString();

        foreach (var item in GranadeSpawnPoint)
        {
            item.SetActive(true);
            item.GetComponent<Granade>().Start();
        }
    }

    private void Turn()
    {
        transform.Rotate(new Vector3(0, 0, turn_speed * Time.deltaTime));
    }

    private void GameOver()
    {
        //Check Best Time
        if (PlayerPrefs.GetFloat("BestTime") < timer)
        {
            PlayerPrefs.SetFloat("BestTime", timer);
            StartTxt.text = "New Best Time: " + timer.ToString("00") + " Second\nPress to retry!";
            BestTimeTxt.text = PlayerPrefs.GetFloat("BestTime").ToString("00");
        }
        else
            StartTxt.text = "Your Time: " + timer.ToString("00") + " Second\nPress to retry!";

        isGameOver = true;
        isStart = false;
        player.enabled = false;


        foreach (var item in GranadeSpawnPoint)
        {
            item.SetActive(false);
        }

        foreach (var item in GameObject.FindGameObjectsWithTag("granade"))
        {
            Destroy(item);
        }
    }
}
