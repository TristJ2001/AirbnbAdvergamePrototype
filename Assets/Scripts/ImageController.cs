using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ImageController : MonoBehaviour
{
    public delegate void OnPlayerWon(int attempts);
    public static event OnPlayerWon OnPlayerWonAction;
    
    public delegate void OnPlayerLost();
    public static event OnPlayerLost OnPlayerLostAction;
    
    public delegate void OnSetInfoText(string text);
    public static event OnSetInfoText OnSetInfoTextAction;
    
    private GameObject[] currentSquareRow;
    
    private GameObject[] row1Squares;
    private GameObject[] row2Squares;
    private GameObject[] row3Squares;
    private GameObject[] row4Squares;
    private GameObject[] row5Squares;

    [SerializeField] private GameObject greenSquare;
    [SerializeField] private GameObject redSquare;
    [SerializeField] private GameObject yellowSquare;
    
    [SerializeField] private Sprite Image1;
    [SerializeField] private Sprite Image2;
    [SerializeField] private Sprite Image3;
    [SerializeField] private Sprite Image4;
    [SerializeField] private Sprite Image5;
    [SerializeField] private Sprite Image6;
    [SerializeField] private Sprite Image7;
    [SerializeField] private Sprite Image8;
    [SerializeField] private Sprite Image9;
    [SerializeField] private Sprite Image10;

    private int[] correctCombination;

    // private int currentRow = 1;

    private void Start()
    {
        row1Squares = GameManager._instance.Row1Squares;
        row2Squares = GameManager._instance.Row2Squares;
        row3Squares = GameManager._instance.Row3Squares;
        row4Squares = GameManager._instance.Row4Squares;
        row5Squares = GameManager._instance.Row5Squares;

    }

    

    private void OnEnable()
    {
        GameManager.OnEnableAllImages += EnableAllImages;
    }

    private void OnDisable()
    {
        GameManager.OnEnableAllImages -= EnableAllImages;
    }

    void Update()
    {
        UpdateRowNumber();

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

            if (hit.collider == null || GameManager._instance.GameOver)
            {
                
            }
            else
            {
                if (hit.transform.name == name)
                {
                    int imageNum;
                    
                    switch (name)
                    {
                        case "Image01": imageNum = 1;
                            CheckIfSquareIsBehind("Image01");
                            break;
                        case "Image02": imageNum = 2;
                            CheckIfSquareIsBehind("Image02");
                            break;
                        case "Image03": imageNum = 3;
                            CheckIfSquareIsBehind("Image03");
                            break;
                        case "Image04": imageNum = 4;
                            CheckIfSquareIsBehind("Image04");
                            break;
                        case "Image05": imageNum = 5;
                            CheckIfSquareIsBehind("Image05");
                            break;
                        case "Image06": imageNum = 6;
                            CheckIfSquareIsBehind("Image06");
                            break;
                        case "Image07": imageNum = 7;
                            CheckIfSquareIsBehind("Image07");
                            break;
                        case "Image08": imageNum = 8;
                            CheckIfSquareIsBehind("Image08");
                            break;
                        case "Image09": imageNum = 9;
                            CheckIfSquareIsBehind("Image09");
                            break;
                        case "Image10": imageNum = 10;
                            CheckIfSquareIsBehind("Image10");
                            break;
                        default:
                            imageNum = 0;
                            break;
                    }
                    
                    if (currentSquareRow[0].CompareTag("EmptySquare"))
                    {
                        SetSquareImage(0, imageNum);
                    }
                    else if (currentSquareRow[1].CompareTag("EmptySquare"))
                    {
                        SetSquareImage(1, imageNum);
                    }
                    else if (currentSquareRow[2].CompareTag("EmptySquare"))
                    {
                        SetSquareImage(2, imageNum);
                    }
                    else if (currentSquareRow[3].CompareTag("EmptySquare"))
                    {
                        SetSquareImage(3, imageNum);
                    }
                    else if (currentSquareRow[4].CompareTag("EmptySquare"))
                    {
                        SetSquareImage(4, imageNum);
                        CheckCombination();
                        // UpdateRowNum(GameManager._instance.CurrentRowNum++);
                        GameManager._instance.CurrentRowNum++;
                    }
                    
                    Debug.Log(name);
                }
            }
        }
    }

    private void CheckIfSquareIsBehind(string imageName)
    {
        foreach (GameObject square in GameObject.FindGameObjectsWithTag("YellowSquare"))
        {
            Vector2 squarePos = new Vector2(square.transform.position.x, square.transform.position.y);
            GameObject image = GameObject.FindGameObjectWithTag(imageName);
            Vector2 imagePos = new Vector2(image.transform.position.x, image.transform.position.y);

            if (squarePos == imagePos)
            {
                square.SetActive(false);
            }
        }
        foreach (GameObject square in GameObject.FindGameObjectsWithTag("RedSquare"))
        {
            Vector2 squarePos = new Vector2(square.transform.position.x, square.transform.position.y);
            GameObject image = GameObject.FindGameObjectWithTag(imageName);
            Vector2 imagePos = new Vector2(image.transform.position.x, image.transform.position.y);

            if (squarePos == imagePos)
            {
                square.SetActive(false);
            }
        }
        foreach (GameObject square in GameObject.FindGameObjectsWithTag("GreenSquare"))
        {
            Vector2 squarePos = new Vector2(square.transform.position.x, square.transform.position.y);
            GameObject image = GameObject.FindGameObjectWithTag(imageName);
            Vector2 imagePos = new Vector2(image.transform.position.x, image.transform.position.y);

            if (squarePos == imagePos)
            {
                square.SetActive(false);
            }
        }
    }

    private void UpdateRowNumber()
    {
        if (GameManager._instance.CurrentRowNum == 1)
        {
            currentSquareRow = row1Squares;
            // Debug.Log("Current square row: 1");
        }
        else if (GameManager._instance.CurrentRowNum == 2)
        {
            currentSquareRow = row2Squares;
            // Debug.Log("Current square row: 2");
        }
        else if (GameManager._instance.CurrentRowNum == 3)
        {
            currentSquareRow = row3Squares;
            // Debug.Log("Current square row: 3");
        }
        else if (GameManager._instance.CurrentRowNum == 4)
        {
            currentSquareRow = row4Squares;
            // Debug.Log("Current square row: 3");
        }
        else if (GameManager._instance.CurrentRowNum == 5)
        {
            currentSquareRow = row5Squares;
            // Debug.Log("Current square row: 3");
        }
    }

    private void CheckCombination()
    {
        EnableAllImages();
        
        int numCorrect = 0;

        int wonInNumAttempts = 0;
        
        int[] newCombination = GetCombinationFromImages();
        
        string combinationString = CombinationToString();
        
        Debug.Log($"Input combination {combinationString}");
        
        correctCombination = GameManager._instance.Combination;

        for (int i = 0; i < correctCombination.Length; i++)
        {
            string imageName = currentSquareRow[i].tag.Substring(0, 7);
            Debug.Log(imageName);
            
            if (newCombination[i] == correctCombination[i])
            {
                SetColouredImage(currentSquareRow[i].tag, "green");

                foreach (GameObject square in GameObject.FindGameObjectsWithTag("YellowSquare"))
                {
                    Vector2 squarePos = new Vector2(square.transform.position.x, square.transform.position.y);
                    GameObject image = GameObject.FindGameObjectWithTag(imageName);
                    Vector2 imagePos = new Vector2(image.transform.position.x, image.transform.position.y);
                    
                    if (squarePos == imagePos)
                    {
                        square.SetActive(false);
                    }
                }
                
                SetColouredImage(imageName, "green");
                numCorrect++;
            }
            else if (newCombination[i] != correctCombination[i] 
                     && GameManager._instance.CheckIfNumberExistsInCombination(newCombination[i]))
            {
                SetColouredImage(currentSquareRow[i].tag, "yellow");
                SetColouredImage(imageName, "yellow");
            }
            else
            {
                SetColouredImage(currentSquareRow[i].tag, "red");
                SetColouredImage(imageName, "red");
            }

            currentSquareRow[i].tag = "LockedSquare";
        }

        switch (numCorrect)
        {
            case 0: OnSetInfoTextAction?.Invoke("A good try, but not quite right");
                break;
            case 1: OnSetInfoTextAction?.Invoke("You got 1 right! Keep going!");
                break;
            case 2: OnSetInfoTextAction?.Invoke("Nice! You got 2 right!");
                break;
            case 3: OnSetInfoTextAction?.Invoke("3 right! You're getting close!");
                break;
            case 4: OnSetInfoTextAction?.Invoke("Nearly there!");
                break;
        }
        
        Debug.Log("Num Correct: " + numCorrect);
        Debug.Log("Combination Length: " + correctCombination.Length);

        if (numCorrect == correctCombination.Length)
        {
            switch (GameManager._instance.CurrentRowNum)
            {
                case 1: wonInNumAttempts = 1;
                    break;
                case 2: wonInNumAttempts = 2;
                    break;
                case 3: wonInNumAttempts = 3;
                    break;
                case 4: wonInNumAttempts = 4;
                    break;
                case 5: wonInNumAttempts = 5;
                    break;
            }
            
            OnPlayerWonAction?.Invoke(wonInNumAttempts);
        }

        if (GameManager._instance.CurrentRowNum == GameManager._instance.NumAttempts 
            && numCorrect != correctCombination.Length)
        {
            OnPlayerLostAction?.Invoke();
        }

        // if (!GameManager._instance.GameOver)
        // {
        //     EnableAllImages();
        // }
    }

    private int[] GetCombinationFromImages()
    {
        int[] newCombination = new int[5];

        int i = 0;

        foreach (GameObject square in currentSquareRow)
        {
            if (square.CompareTag("Image01Combination"))
            {
                newCombination[i] = 1;
                i++;
            }
            else if (square.CompareTag("Image02Combination"))
            {
                newCombination[i] = 2;
                i++;
            }
            else if (square.CompareTag("Image03Combination"))
            {
                newCombination[i] = 3;
                i++;
            }
            else if (square.CompareTag("Image04Combination"))
            {
                newCombination[i] = 4;
                i++;
            }
            else if (square.CompareTag("Image05Combination"))
            {
                newCombination[i] = 5;
                i++;
            }
            else if (square.CompareTag("Image06Combination"))
            {
                newCombination[i] = 6;
                i++;
            }
            else if (square.CompareTag("Image07Combination"))
            {
                newCombination[i] = 7;
                i++;
            }
            else if (square.CompareTag("Image08Combination"))
            {
                newCombination[i] = 8;
                i++;
            }
            else if (square.CompareTag("Image09Combination"))
            {
                newCombination[i] = 9;
                i++;
            }
            else if (square.CompareTag("Image10Combination"))
            {
                newCombination[i] = 10;
                i++;
            }
        }

        return newCombination;
    }

    private void SetColouredImage(string squareTag, string colour)
    {
        if (colour == "green")
        {
            GameObject square = GameObject.FindGameObjectWithTag(squareTag);
            Vector3 squarePosition = square.transform.position;
    
            GameObject newSquare = Instantiate(greenSquare, 
                new Vector3(squarePosition.x, squarePosition.y, 1), Quaternion.identity);
            newSquare.tag = "GreenSquare";
        }
        else if (colour == "red")
        {
            GameObject square = GameObject.FindGameObjectWithTag(squareTag);
            Vector3 squarePosition = square.transform.position;
    
            GameObject newSquare = Instantiate(redSquare, 
                new Vector3(squarePosition.x, squarePosition.y, 1), Quaternion.identity);
            newSquare.tag = "RedSquare";
        }
        else if (colour == "yellow")
        {
            GameObject square = GameObject.FindGameObjectWithTag(squareTag);
            Vector3 squarePosition = square.transform.position;
    
            GameObject newSquare = Instantiate(yellowSquare, 
                new Vector3(squarePosition.x, squarePosition.y, 1), Quaternion.identity);
            newSquare.tag = "YellowSquare";
        }

    }

    private void EnableAllImages()
    {
        GameObject image1GO = GameObject.FindGameObjectWithTag("Image01");
        SpriteRenderer image1Renderer = image1GO.GetComponent<SpriteRenderer>();
        image1Renderer.enabled = true;
        
        GameObject image2GO = GameObject.FindGameObjectWithTag("Image02");
        SpriteRenderer image2Renderer = image2GO.GetComponent<SpriteRenderer>();
        image2Renderer.enabled = true;
        
        GameObject image3GO = GameObject.FindGameObjectWithTag("Image03");
        SpriteRenderer image3Renderer = image3GO.GetComponent<SpriteRenderer>();
        image3Renderer.enabled = true;
        
        GameObject image4GO = GameObject.FindGameObjectWithTag("Image04");
        SpriteRenderer image4Renderer = image4GO.GetComponent<SpriteRenderer>();
        image4Renderer.enabled = true;
        
        GameObject image5GO = GameObject.FindGameObjectWithTag("Image05");
        SpriteRenderer image5Renderer = image5GO.GetComponent<SpriteRenderer>();
        image5Renderer.enabled = true;
        
        GameObject image6GO = GameObject.FindGameObjectWithTag("Image06");
        if (image6GO != null)
        {
            SpriteRenderer image6Renderer = image6GO.GetComponent<SpriteRenderer>();
            image6Renderer.enabled = true;
        }

        GameObject image7GO = GameObject.FindGameObjectWithTag("Image07");
        if (image7GO != null)
        {
            SpriteRenderer image7Renderer = image7GO.GetComponent<SpriteRenderer>();
            image7Renderer.enabled = true;
        }
        
        GameObject image8GO = GameObject.FindGameObjectWithTag("Image08");
        if (image8GO != null)
        {
            SpriteRenderer image8Renderer = image8GO.GetComponent<SpriteRenderer>();
            image8Renderer.enabled = true;
        }

        GameObject image9GO = GameObject.FindGameObjectWithTag("Image09");
        if (image9GO != null)
        {
            SpriteRenderer image9Renderer = image9GO.GetComponent<SpriteRenderer>();
            image9Renderer.enabled = true;
        }

        GameObject image10GO = GameObject.FindGameObjectWithTag("Image10");
        if (image10GO != null)
        {
            SpriteRenderer image10Renderer = image10GO.GetComponent<SpriteRenderer>();
            image10Renderer.enabled = true;
        }
    }

    private void SetSquareImage(int square, int image)
    {
        Vector3 scale = new Vector3();
        Sprite Image;
        string imageCombinationName;
        
        switch (image)
        {
            case 1: Image = Image1;
                scale = new Vector3(0.5f, 0.5f, 0.5f);

                imageCombinationName = "Image01Combination";
                
                GameObject image1GO = GameObject.FindGameObjectWithTag("Image01");
                SpriteRenderer image1Renderer = image1GO.GetComponent<SpriteRenderer>();
                image1Renderer.enabled = false;
                
                break;
            case 2: Image = Image2;
                scale = new Vector3(0.255f, 0.275f, 0.5f);
                
                imageCombinationName = "Image02Combination";
                
                GameObject image2GO = GameObject.FindGameObjectWithTag("Image02");
                SpriteRenderer image2Renderer = image2GO.GetComponent<SpriteRenderer>();
                image2Renderer.enabled = false;
                
                break;
            case 3: Image = Image3;
                scale = new Vector3(0.28f, 0.35f, 0.5f);
                
                imageCombinationName = "Image03Combination";

                GameObject image3GO = GameObject.FindGameObjectWithTag("Image03");
                SpriteRenderer image3Renderer = image3GO.GetComponent<SpriteRenderer>();
                image3Renderer.enabled = false;
                
                break;
            case 4: Image = Image4;
                scale = new Vector3(0.25f, 0.29f, 0.5f);
                
                imageCombinationName = "Image04Combination";
                
                GameObject image4GO = GameObject.FindGameObjectWithTag("Image04");
                SpriteRenderer image4Renderer = image4GO.GetComponent<SpriteRenderer>();
                image4Renderer.enabled = false;
                
                break;
            case 5: Image = Image5;
                scale = new Vector3(0.25f, 0.29f, 0.5f);
                
                imageCombinationName = "Image05Combination";
                
                GameObject image5GO = GameObject.FindGameObjectWithTag("Image05");
                SpriteRenderer image5Renderer = image5GO.GetComponent<SpriteRenderer>();
                image5Renderer.enabled = false;
                
                break;
            case 6: Image = Image6;
                scale = new Vector3(0.25f, 0.29f, 0.5f);
                
                imageCombinationName = "Image06Combination";
                
                GameObject image6GO = GameObject.FindGameObjectWithTag("Image06");
                SpriteRenderer image6Renderer = image6GO.GetComponent<SpriteRenderer>();
                image6Renderer.enabled = false;
                
                break;
            case 7: Image = Image7;
                scale = new Vector3(0.25f, 0.29f, 0.5f);
                
                imageCombinationName = "Image07Combination";
                
                GameObject image7GO = GameObject.FindGameObjectWithTag("Image07");
                SpriteRenderer image7Renderer = image7GO.GetComponent<SpriteRenderer>();
                image7Renderer.enabled = false;
                
                break;
            case 8: Image = Image8;
                scale = new Vector3(0.25f, 0.29f, 0.5f);
                
                imageCombinationName = "Image08Combination";
                
                GameObject image8GO = GameObject.FindGameObjectWithTag("Image08");
                SpriteRenderer image8Renderer = image8GO.GetComponent<SpriteRenderer>();
                image8Renderer.enabled = false;
                
                break;
            case 9: Image = Image9;
                scale = new Vector3(0.25f, 0.29f, 0.5f);
                
                imageCombinationName = "Image09Combination";
                
                GameObject image9GO = GameObject.FindGameObjectWithTag("Image09");
                SpriteRenderer image9Renderer = image9GO.GetComponent<SpriteRenderer>();
                image9Renderer.enabled = false;
                
                break;
            case 10: Image = Image10;
                scale = new Vector3(0.25f, 0.29f, 0.5f);
                
                imageCombinationName = "Image10Combination";
                
                GameObject image10GO = GameObject.FindGameObjectWithTag("Image10");
                SpriteRenderer image10Renderer = image10GO.GetComponent<SpriteRenderer>();
                image10Renderer.enabled = false;
                
                break;
            default: Image = null;
                scale = new Vector3(0, 0, 0);
                
                imageCombinationName = "";
                
                Debug.Log("null");
                break;
        }
        
        SpriteRenderer squareRenderer = currentSquareRow[square].GetComponent<SpriteRenderer>();
        squareRenderer.sprite = Image;
        squareRenderer.transform.localScale = scale;
        currentSquareRow[square].tag = imageCombinationName;
    }

    private void UpdateColouredImages()
    {
        
    }

    private string CombinationToString()
    {
        int[] newCombination = GetCombinationFromImages();
        string displayCombination = "";

        foreach (int c in newCombination)
        {
            displayCombination += c + " ";
        }

        return displayCombination;
    }
}
