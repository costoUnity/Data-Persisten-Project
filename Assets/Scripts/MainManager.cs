using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    //To get the bestscore
    private int bestScore;

    // UI loading Screen GameObject
    public GameObject loadingScreen;

    //To get the username
    private string userName;

    //Store the username and Best Score to TextMeshProUGUI 
    public TextMeshProUGUI bestScore_Name;
    public TextMeshProUGUI bestScoreHolder;
    public TextMeshProUGUI bestPlayerName;

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    public bool loadingScreenOf = false;

    
    // Start is called before the first frame update
    void Start()
    {
        if (!string.IsNullOrEmpty(GameManager.Instance.namee))
        {
            loadingScreen.gameObject.SetActive(false);
            loadingScreenOf = true;
             bestScore_Name.text = "Name:" + GameManager.Instance.namee;
        }
        // To Load the best Score
        //bestScore = GameManager.Instance.TheBestScore;
        GameManager.Instance.LoadName();
        bestScoreHolder.text = "Best Score: " +GameManager.Instance.TheBestScore;
        bestPlayerName.text = "Best Player: " + GameManager.Instance.namee;
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space) && loadingScreenOf)
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            bestScore = m_Points;
            if(bestScore > GameManager.Instance.TheBestScore)
            {
                GameManager.Instance.TheBestScore = bestScore;
                bestScoreHolder.text = "Best Score " + GameManager.Instance.TheBestScore;
                bestPlayerName.text = "Name:" + GameManager.Instance.namee;
                GameManager.Instance.SaveName();
            }
            
            
            
            
            //GameManager.Instance.SaveName();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
       
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void GetUserName(string playerInput)
    {
        userName = playerInput;
        GameManager.Instance.namee = userName;
        Debug.Log(userName);
        bestScore_Name.text = "Name:" + GameManager.Instance.namee;

    }

  public  void ToGameOnClick()
    {
        if(!string.IsNullOrEmpty(userName))
        {
            loadingScreen.gameObject.SetActive(false);
            loadingScreenOf = true;
        }
            
        
    }
   
}
