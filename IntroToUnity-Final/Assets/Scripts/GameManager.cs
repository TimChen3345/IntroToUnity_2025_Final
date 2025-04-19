using System;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    int score;

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            if (score > HighScore)
            {
                HighScore = score;
            }
            Debug.Log("The score is now:" + score);
        }
    }

    int highScore = 10; //PB
    
    const string KEY_HIGH_SCORE = "HIGH SCORE";

    string FILE_NAME = "/highScoreFile.txt";

    string FILE_PATH;

    int[] highScoreArray;

    public int HighScore
    {
        get
        {
            //highScore = PlayerPrefs.GetInt(KEY_HIGH_SCORE);
            string fileContents = File.ReadAllText(FILE_PATH + FILE_NAME);
            if (fileContents.Length == 0)
            {
                highScore = 1;
                return highScore;
            }
            else
            {
                string[] scoreString = fileContents.Split(',');

                highScoreArray = new int[scoreString.Length];

                for (int i = 0; i < scoreString.Length; i++)
                {
                    highScoreArray[i] = int.Parse(scoreString[i]);

                    Debug.Log("HighScoreArray" + i + ":" + highScoreArray[i]);
                }

                highScore = highScoreArray[0];

                return highScore;
            }
        }
        set
        {
            highScore = value;
            
            Debug.Log("FILE" + FILE_PATH + FILE_NAME);
            string highScoreString = highScore + "";

            for (int i = 0; i < highScoreArray.Length; i++)
            {
                highScoreString += highScoreArray[i] + ",";
            }
            
            highScoreString = highScoreString.TrimEnd(',');
            
            File.WriteAllText(FILE_PATH + FILE_NAME, highScoreString);
            
            Debug.Log("New High Score" + highScore);
        }
    }

    public int levelScore = 5;

    public TextMeshPro tmp;

    private float timeRemaining = 34;

    private int currentSceneNum = 1;
    void Awake()
    {
        // if instance has not been set yet
        if (instance == null)
        {
            // make this object the instance
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //I'm a duplicate and I need to die, there can only be one
            Destroy(gameObject);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FILE_PATH = Application.persistentDataPath;
        Debug.Log(FILE_PATH);
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;

        tmp.text = "Score:" + score + " Level:" + currentSceneNum + " Time Remaining:" + Mathf.Floor(timeRemaining);
        
        //if the time has run out, go to game over scene
        if (timeRemaining <= 0)
        {
            GameOverScene();
            Debug.Log("You've reached Level: " + currentSceneNum + "With Score" + score);
            Destroy(gameObject);
        }
        
        // If I hit a Certain Score, I advance to the next Level(Scene)
        if (score >= levelScore)
        {
            //increase the levelScore by 50%
            levelScore += levelScore + levelScore / 2;
            ChangeScene();
        }
        
    }

    void ChangeScene()
    {
        currentSceneNum++;
        SceneManager.LoadScene(currentSceneNum);
    }

    void GameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void resetHighScore()
    {
        PlayerPrefs.DeleteKey(KEY_HIGH_SCORE);
    }
}
