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
    [SerializeField] private GameObject Timer;
    [SerializeField] private GameObject GameOverMenu;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI timesUp;
    private float timeLimit = 10f;
    private float timeRemaining;

    // Update is called once per frame
    void Update()
    {
        if (playingGame) {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timeText.enabled = false;
                timesUp.enabled = true;
                playingGame = false;
                GameOver();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
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
        StartGame();
    }

    public void PlayAgainButton()
    {
        ClearBlocks();
        playingGame = true;
        GameOverMenu.SetActive(false);
        StartGame();
    }

    public void MainMenuButton()
    {
        ClearBlocks();
        playingGame = false;
        Timer.SetActive(false);
        GameOverMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    private void StartGame() {
        playingGame = true;
        timeRemaining = timeLimit;
        gameObject.GetComponent<BlockSpawner>().RandomizeBlock();
        Timer.SetActive(true);
        timeText.enabled = true;
        timesUp.enabled = false;
    }

    private void GameOver() {
        GameOverMenu.SetActive(true);
    }

    private void ClearBlocks() {
        foreach (var gameObj in GameObject.FindGameObjectsWithTag("Block")){
            Destroy(gameObj);
        }
    }

}
