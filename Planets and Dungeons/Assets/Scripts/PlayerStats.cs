using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private Health hp;
    public GameObject player;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private TextMeshProUGUI timeLose;
    [SerializeField] private TextMeshProUGUI timeWin;
    [SerializeField] private TextMeshProUGUI roomsLose;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI scoreWin;
    [SerializeField] private TextMeshProUGUI scoreLose;
    [HideInInspector] public bool isWin;
    private bool isLose = true;
    private int seconds = 0;
    private int minutes = 0;
    private int hours = 0;

    private float second = 1f;
    float currentSecond = 1f;

    [HideInInspector] public int roomsCompleted;
    [HideInInspector] public int scoreAmount;
    [SerializeField] private TextMeshProUGUI rooms;

    private void Start()
    {
        hp = player.GetComponent<Health>();
        Time.timeScale = 1f;
}
    void Update()
    {
        Timer();
        RoomsCounter();
        ScoreCounter();
        if (hp.health <= 0)
        {
            if(isLose)
            {
                healthText.text = 0.ToString();
                loseScreen.SetActive(true);
                Time.timeScale = 0f;
                timeLose.text = "Time: " + timer.text;
                roomsLose.text = "Rooms completed: " + roomsCompleted.ToString();
                scoreLose.text = "Score: " + scoreAmount.ToString();
                isLose = false;
            }
        }
        else
        {
            healthText.text = hp.health.ToString();
        }
        if(isWin)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0f;
            timeWin.text = "Time: " + timer.text;
            scoreWin.text = "Score: " + scoreAmount.ToString();
            isWin = false;
        }

    }

    private void Timer()
    {
        currentSecond -= Time.deltaTime;
        if(currentSecond <= 0)
        {
            seconds++;
            currentSecond = second;
            if(seconds >= 60)
            {
                seconds = 0;
                minutes++;
                if(minutes >= 60)
                {
                    hours++;
                }
            }
        }
        string secondsText;
        if (seconds < 10)
        {
            secondsText = ":0" + seconds.ToString();
        }
        else
        {
            secondsText = ":" + seconds.ToString();
        }
        string minutesText;
        if (minutes < 10)
        {
            minutesText = ":0" + minutes.ToString();
        }
        else
        {
            minutesText = ":" + minutes.ToString();
        }
        string hoursText;
        if (hours < 10)
        {
            hoursText = "0" + hours.ToString();
        }
        else
        {
            hoursText = hours.ToString();
        }
        timer.text = hoursText + minutesText + secondsText;
    }
    private void RoomsCounter()
    {
        rooms.text = "Rooms: " + roomsCompleted.ToString();
    }
    private void ScoreCounter()
    {
        score.text = "Score: " + scoreAmount.ToString();
    }
}
