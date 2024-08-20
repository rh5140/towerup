using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool playingGame = false;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject MainMenuImage;
    [SerializeField] private GameObject TutorialMenu;
    [SerializeField] private GameObject TutorialImage;
    [SerializeField] private GameObject Timer;
    [SerializeField] private GameObject GameOverMenu;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject timesUp;
    [SerializeField] private float timeLimit = 60f;
    private float timeRemaining;
    private bool firstPlay = true;

    [SerializeField] private AudioSource uiAudio;
    private bool doesPieceLock = true; // Does the piece lock in place once it slows down enough?

    // Update is called once per frame
    void Update() {
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
                timeText.gameObject.SetActive(false);
                timesUp.SetActive(true);
                playingGame = false;
                GameOver();
            }
        }

        if (Input.GetKeyDown("l")) // Piece lock toggle
        {
            doesPieceLock = !doesPieceLock;
            if (doesPieceLock) uiAudio.clip = Camera.main.GetComponent<AudioManager>().HighNote();
            else uiAudio.clip = Camera.main.GetComponent<AudioManager>().LowNote();
            uiAudio.Play();
        }
    }

    void DisplayTime(float timeToDisplay) {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    // Is it bad if I put UI stuff here.. I feel like control flow makes sense here tho

    public void PlayButton() {
        ClearBlocks();
        MainMenu.SetActive(false);
        //if (firstPlay) {
            TutorialSetup();
        //    firstPlay = false;
        //}
        //else {
        //    StartGame();
        //}
    }
    
    public void BeginButton() {
        StartCoroutine(WaitForAnim());
    }

    IEnumerator WaitForAnim() {
        yield return new WaitForSeconds(0.5f);
        StartGame();
        TutorialMenu.SetActive(false);
    }

    public void PlayAgainButton() {
        //ClearBlocks();
        playingGame = true;
        GameOverMenu.SetActive(false);
        StartGame();
    }

    public void MainMenuButton() {
        playingGame = false;
        Timer.SetActive(false);
        GameOverMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    private void StartGame() {
        ClearBlocks();
        MainMenuImage.SetActive(false);
        ResetNotes();
        playingGame = true;
        timeRemaining = timeLimit;
        gameObject.GetComponent<BlockSpawner>().RandomizeBlock();
        Timer.SetActive(true);
        timeText.gameObject.SetActive(true);
        timesUp.SetActive(false);
    }

    private void TutorialSetup() {
        TutorialMenu.SetActive(true);
    }

    private void GameOver() {
        GameOverMenu.SetActive(true);
    }

    private void ClearBlocks() {
        foreach (var gameObj in GameObject.FindGameObjectsWithTag("Block")){
            Destroy(gameObj);
        }
    }

    private void ResetNotes() {
        Camera.main.GetComponent<AudioManager>().repeats = 0;
        Camera.main.GetComponent<AudioManager>().noteIdx = 0;
        Camera.main.GetComponent<AudioManager>().ascending = true;
    }

    public bool DoesPieceLock()
    {
        return doesPieceLock;
    }
    

}
