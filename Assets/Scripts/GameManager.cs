using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Properties
    public static GameManager Instance = null;

    [Header("Component Reference")]
    [SerializeField] public List<GameObject> controlObjects;
    [SerializeField] public GameObject confetti;

    [Header("Attributes")]
    [SerializeField] private int currentScore;
    [SerializeField] private int currentLevel;
    [SerializeField] private int maxLevels;
    [SerializeField] GameState currentState;


    #endregion

    #region MonoBehaviour Functions
    private void Awake()
    {
        Application.targetFrameRate = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        //SwitchCamera(CameraType.MatchStickCamera);
        currentLevel = PlayerPrefs.GetInt("level", 1);
        foreach (GameObject g in controlObjects)
        {
            g.SetActive(false);
        }
        UIManager.Instance.UpdateLevel(currentLevel);
        currentState = GameState.Main;
        maxLevels = 4;
        TinySauce.OnGameStarted();
    }
    #endregion
    private void Update()
    {

    }
    public void StartLevel()
    {
        UIManager.Instance.SwitchUIPanel(UIPanelState.Gameplay);
        foreach(GameObject g in controlObjects)
        {
            g.SetActive(true);
        }
        currentState = GameState.InGame;
        TinySauce.OnGameStarted(currentLevel+"");


    }
    public void SwitchToMainCam()
    {
        //cineCam.SetActive(false);
        //mainCam.SetActive(true);
    }

    public void WinLevel()
    {
        if (currentState == GameState.InGame)
        {
            confetti.SetActive(true);
            Invoke("ShowWinUI", 1.4f);
            foreach (GameObject g in controlObjects)
            {
                g.SetActive(false);
            }
            CatManager.Instance.enabled = false;
            CatBoneManager.Instance.enabled = false;
            currentState = GameState.Win;

            PlayerPrefs.SetInt("level", currentLevel + 1);
            TinySauce.OnGameFinished(true, 0);

            currentLevel++;           
        }
    }

    public void LoseLevel()
    {
        if (currentState == GameState.InGame)
        {
            //Show cat VFX
            foreach (GameObject g in controlObjects)
            {
                g.SetActive(false);
            }
            CatManager.Instance.enabled = false;
            CatBoneManager.Instance.enabled = false;

            Invoke("ShowLoseUI", 2f);
            currentState = GameState.Lose;
            TinySauce.OnGameFinished(false, 0);
        }
    }

    #region Scene Management

   

    public void ChangeLevel()
    {
        if (currentLevel > maxLevels)
        {
            int newId = currentLevel % maxLevels;
            if (newId == 0)
            {
                newId = maxLevels;
            }
            SceneManager.LoadScene("Level " + (newId));
        }
        else
        {
            SceneManager.LoadScene("Level " + currentLevel);
        }
    }
  
    #endregion


    #region Public Core Functions

    public void AddScore(int value)
    {
        currentScore += value;
        UIManager.Instance.UpdateScore(currentScore);
    } 

    #endregion

    #region Invoke Functions

    void ShowWinUI()
    {
        UIManager.Instance.SwitchUIPanel(UIPanelState.GameWin);
    }

    void ShowLoseUI()
    {
        UIManager.Instance.SwitchUIPanel(UIPanelState.GameLose);
    }
    #endregion
}
