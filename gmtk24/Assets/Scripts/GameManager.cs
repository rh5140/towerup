using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public bool playingGame = false;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject TutorialMenu;
    [SerializeField] private GameObject GameOverMenu;
    private TextMeshProUGUI timeText;
    private float timeRemaining = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playingGame) {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                // DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                playingGame = false;
                // GameOver();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = "Time remaining: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Is it bad if I put UI stuff here.. I feel like control flow makes sense here tho

    public void PlayButton()
    {
        TutorialMenu.SetActive(true);
        MainMenu.SetActive(false);
    }
    
    public void BeginButton()
    {
        TutorialMenu.SetActive(false);
        playingGame = true;
    }

    public void PlayAgainButton()
    {
        // restart
    }

    public void MainMenuButton()
    {
        playingGame = false;
        MainMenu.SetActive(true);
    }

}
