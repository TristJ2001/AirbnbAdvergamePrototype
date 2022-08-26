using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public delegate void EnableAllImages();
    public static event EnableAllImages OnEnableAllImages;
    
    public static GameManager _instance { get; private set; }

    private int currentRowNum = 1;

    private bool gameOver;

    [Range(1, 5)][SerializeField] private int numAttempts = 5;
    public int NumAttempts
    {
        get { return numAttempts; }
    }
    
    [Range(5, 10)][SerializeField] private int numImages = 10;
    public int NumImages
    {
        get { return numImages; }
    }

    private static int numGamesWon = 0;
    public static int NumGamesWon
    {
        get => numGamesWon;
    }
    
    private static int numGamesPlayed = 0;
    public static int NumGamesPlayed
    {
        get => numGamesPlayed;
    }

    private static int gamesWonIn1Attempt = 0;
    public static int GamesWonIn1Attempt
    {
        get => gamesWonIn1Attempt;
    }
    
    private static int gamesWonIn2Attempts = 0;
    public static int GamesWonIn2Attempts
    {
        get => gamesWonIn2Attempts;
    }
    
    private static int gamesWonIn3Attempts = 0;
    public static int GamesWonIn3Attempts
    {
        get => gamesWonIn3Attempts;
    }
    
    private static int gamesWonIn4Attempts = 0;
    public static int GamesWonIn4Attempts
    {
        get => gamesWonIn4Attempts;
    }
    
    private static int gamesWonIn5Attempts = 0;
    public static int GamesWonIn5Attempts
    {
        get => gamesWonIn5Attempts;
    }

    public int CurrentRowNum
    {
        get => currentRowNum;
        set { currentRowNum = value; }
    }

    public bool GameOver => gameOver;

    [SerializeField] private int combinationSize = 5;
    // [SerializeField] private int numOfOptions = 10;

    [SerializeField] private TMP_Text playerInfoText;

    [SerializeField] private GameObject[] row1Squares;
    [SerializeField] private GameObject[] row2Squares;
    [SerializeField] private GameObject[] row3Squares;
    [SerializeField] private GameObject[] row4Squares;
    [SerializeField] private GameObject[] row5Squares;

    [SerializeField] private Sprite emptySquare;

    private GameObject[] currentRow;
    
    public GameObject[] Row1Squares => row1Squares;
    public GameObject[] Row2Squares => row2Squares;
    public GameObject[] Row3Squares => row3Squares;
    public GameObject[] Row4Squares => row4Squares;
    public GameObject[] Row5Squares => row5Squares;
    
    private int[] combination;
    public int[] Combination => combination;
    
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        numAttempts = MainMenuController.numAttempts;
        numImages = MainMenuController.numImages;
        
        playerInfoText.SetText("Hi, I'm BB! Welcome to Picture This!");
        
        combination = new int[combinationSize];
        
        GenerateCombination();
        
        SetNumAttempts();
        SetNumImages();
        
        numGamesPlayed++;
    }

    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnRestartGameButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    private void SetNumAttempts()
    {
        switch (NumAttempts)
        {
            case 1:
                foreach (GameObject square in row2Squares)
                {
                    square.SetActive(false);
                }
                foreach (GameObject square in row3Squares)
                {
                    square.SetActive(false);
                }
                foreach (GameObject square in row4Squares)
                {
                    square.SetActive(false);
                }
                foreach (GameObject square in row5Squares)
                {
                    square.SetActive(false);
                }
                break;
            case 2:
                foreach (GameObject square in row3Squares)
                {
                    square.SetActive(false);
                }
                foreach (GameObject square in row4Squares)
                {
                    square.SetActive(false);
                }
                foreach (GameObject square in row5Squares)
                {
                    square.SetActive(false);
                }
                break;
            case 3:
                foreach (GameObject square in row4Squares)
                {
                    square.SetActive(false);
                }
                foreach (GameObject square in row5Squares)
                {
                    square.SetActive(false);
                }
                break;
            case 4:
                foreach (GameObject square in row5Squares)
                {
                    square.SetActive(false);
                }
                break;
            default:
                return;
        }
    }

    private void SetNumImages()
    {
        switch (NumImages)
        {
            case 5:
                GameObject.FindGameObjectWithTag("Image06").SetActive(false);
                GameObject.FindGameObjectWithTag("Image07").SetActive(false);
                GameObject.FindGameObjectWithTag("Image08").SetActive(false);
                GameObject.FindGameObjectWithTag("Image09").SetActive(false);
                GameObject.FindGameObjectWithTag("Image10").SetActive(false);
                break;
            case 6:
                GameObject.FindGameObjectWithTag("Image07").SetActive(false);
                GameObject.FindGameObjectWithTag("Image08").SetActive(false);
                GameObject.FindGameObjectWithTag("Image09").SetActive(false);
                GameObject.FindGameObjectWithTag("Image10").SetActive(false);
                break;
            case 7:
                GameObject.FindGameObjectWithTag("Image08").SetActive(false);
                GameObject.FindGameObjectWithTag("Image09").SetActive(false);
                GameObject.FindGameObjectWithTag("Image10").SetActive(false);
                break;
            case 8:
                GameObject.FindGameObjectWithTag("Image09").SetActive(false);
                GameObject.FindGameObjectWithTag("Image10").SetActive(false);
                break;
            case 9:
                GameObject.FindGameObjectWithTag("Image10").SetActive(false);
                break;
            
            default:
                return;
        }
    }

    private void OnEnable()
    {
        ImageController.OnPlayerWonAction += OnPlayerWonAction;
        ImageController.OnPlayerLostAction += OnPlayerLostAction;
        ImageController.OnSetInfoTextAction += OnSetInfoTextAction;
    }

    private void OnDisable()
    {
        ImageController.OnPlayerWonAction -= OnPlayerWonAction;
        ImageController.OnPlayerLostAction -= OnPlayerLostAction;
        ImageController.OnSetInfoTextAction -= OnSetInfoTextAction;
    }

    private void OnPlayerWonAction(int wonInNumAttempts)
    {
        OnEnableAllImages?.Invoke();
        playerInfoText.SetText("You Won! Well done!");
        gameOver = true;
        numGamesWon++;

        switch (wonInNumAttempts)
        {
            case 1: gamesWonIn1Attempt++;
                break;
            case 2: gamesWonIn2Attempts++;
                break;
            case 3: gamesWonIn3Attempts++;
                break;
            case 4: gamesWonIn4Attempts++;
                break;
            case 5: gamesWonIn5Attempts++;
                break;
        }
    }

    private void OnPlayerLostAction()
    {
        OnEnableAllImages?.Invoke();
        playerInfoText.SetText("You Lost! You'll get 'em next time!");
        gameOver = true;
    }

    private void OnSetInfoTextAction(string text)
    {
        playerInfoText.SetText(text);
    }

    private void GenerateCombination()
    {
        for (int i = 0; i < combination.Length; i++)
        {
            int randomNum = Random.Range(1, numImages + 1);

            while (CheckIfNumberExistsInCombination(randomNum))
            { 
                randomNum = Random.Range(1, numImages + 1);
            }
            
            combination[i] = randomNum;
        }
    }

    public bool CheckIfNumberExistsInCombination(int number)
    {
        foreach (int c in combination)
        {
            if (c == number)
            {
                return true;
            }
        }

        return false;
    }

    public void UndoRow()
    {
        switch (currentRowNum)
        {
            case 1:
                foreach (GameObject square in row1Squares)
                {
                    square.GetComponent<SpriteRenderer>().sprite = emptySquare;
                    square.tag = "EmptySquare";
                    square.transform.localScale = new Vector3(1, 1, 1);
                    
                    OnEnableAllImages?.Invoke();
                }
                break;
            case 2:
                foreach (GameObject square in row2Squares)
                {
                    square.GetComponent<SpriteRenderer>().sprite = emptySquare;
                    square.tag = "EmptySquare";
                    square.transform.localScale = new Vector3(1, 1, 1);
                    
                    OnEnableAllImages?.Invoke();
                }
                break;
            case 3:
                foreach (GameObject square in row3Squares)
                {
                    square.GetComponent<SpriteRenderer>().sprite = emptySquare;
                    square.tag = "EmptySquare";
                    square.transform.localScale = new Vector3(1, 1, 1);
                    
                    OnEnableAllImages?.Invoke();
                }
                break;
            case 4:
                foreach (GameObject square in row4Squares)
                {
                    square.GetComponent<SpriteRenderer>().sprite = emptySquare;
                    square.tag = "EmptySquare";
                    square.transform.localScale = new Vector3(1, 1, 1);
                    
                    OnEnableAllImages?.Invoke();
                }
                break;
            case 5:
                foreach (GameObject square in row5Squares)
                {
                    square.GetComponent<SpriteRenderer>().sprite = emptySquare;
                    square.tag = "EmptySquare";
                    square.transform.localScale = new Vector3(1, 1, 1);
                    
                    OnEnableAllImages?.Invoke();
                }
                break;
            default: 
                return;
        }
    }
    
    private void DisplayCombinationString()
    {
        string combinationString = "";
        
        for (int i = 0; i < combination.Length; i++)
        {
            combinationString += combination[i] + " ";
        }
        
        Debug.Log(combinationString);
    }
}
