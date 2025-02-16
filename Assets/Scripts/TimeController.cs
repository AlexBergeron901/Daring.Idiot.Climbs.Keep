using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TimeController : MonoBehaviour
{
    // Start is called before the first frame update
    public static TimeController instance;

    public TextMeshProUGUI timeCounter;
    public TextMeshProUGUI escapeMessage;
    public TextMeshProUGUI victoryMessage;
    AudioSource _audio;
    [SerializeField] AudioClip _victoryHorn;
    public bool hasWon;
    //public TextMeshProUGUI subtitle;

    private TimeSpan timePlaying;
    private bool timerGoing = false;
    private float elapsedTime;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        BeginTimer();
        timerGoing = false;
        escapeMessage.text = "";
        timeCounter.text = "";
        victoryMessage.text = "";
        hasWon = false;

    }

    void Update() {
        //noEscapeMessage();
    }
    public void setSubtitle(string message, float temps)
    {

    }
    public void Victory()
    {
        Debug.Log("bitch");
        StopTimer();
        hasWon = true;
        victoryMessage.text = "Victoire! \n" + timeCounter.text;
        AudioSource.PlayClipAtPoint(_victoryHorn, GameObject.FindGameObjectWithTag("Player").transform.position); 
        StartCoroutine(fuckingLeave());
    }
    IEnumerator fuckingLeave()
    {

        yield return new WaitForSeconds(10f);
        Application.Quit();
    }
    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void StopTimer()
    {
        timerGoing = false;
    }

    public void noEscapeMessage() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           escapeMessage.text = "There is no escape!";
           StartCoroutine(TempsAttente());
        }
    }

    private IEnumerator TempsAttente()
    {
        yield return new WaitForSeconds(3);
        escapeMessage.text = "";
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Temps: " + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;
            yield return null;
        }
    }

    // Update is called once per frame
 
}
