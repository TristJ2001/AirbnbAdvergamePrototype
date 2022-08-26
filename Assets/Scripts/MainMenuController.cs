using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private TMP_Text gamesWonText;
    [SerializeField] private TMP_Text numAttemptsText;
    [SerializeField] private TMP_Text numImagesText;

    [SerializeField] private Slider numAttemptsSlider;
    [SerializeField] private Slider numImagesSlider;

    [SerializeField] private TMP_Text gamesWonIn1Attempt;
    [SerializeField] private TMP_Text gamesWonIn2Attempts;
    [SerializeField] private TMP_Text gamesWonIn3Attempts;
    [SerializeField] private TMP_Text gamesWonIn4Attempts;
    [SerializeField] private TMP_Text gamesWonIn5Attempts;

    public static int numAttempts;
    public static int numImages;

    
    void Start()
    {
        gamesWonText.SetText($"Games Won: {GameManager.NumGamesWon} " + $"/ {GameManager.NumGamesPlayed}");
        
        gamesWonIn1Attempt.SetText($"Games Won In 1 Attempt: {GameManager.GamesWonIn1Attempt}");
        gamesWonIn2Attempts.SetText($"Games Won In 2 Attempts: {GameManager.GamesWonIn2Attempts}");
        gamesWonIn3Attempts.SetText($"Games Won In 3 Attempts: {GameManager.GamesWonIn3Attempts}");
        gamesWonIn4Attempts.SetText($"Games Won In 4 Attempts: {GameManager.GamesWonIn4Attempts}");
        gamesWonIn5Attempts.SetText($"Games Won In 5 Attempts: {GameManager.GamesWonIn5Attempts}");
    }

    // Update is called once per frame
    void Update()
    {
        numAttempts = (int)numAttemptsSlider.value;
        numImages = (int)numImagesSlider.value;
        
        numAttemptsText.SetText($"Number Of Attempts: {numAttempts}");
        numImagesText.SetText($"Number Of Images: {numImages}");
    }

    public void OnStartGameButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnQuitGameButtonClicked()
    {
        Application.Quit();
    }

    public void OnHowToPlayButtonClicked()
    {
        SceneManager.LoadScene("HowToPlayScene");
    }
}
