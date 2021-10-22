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


    [Header("Attributes")]
    [SerializeField] private int currentScore;
    [SerializeField] private int currentLevel;
    [SerializeField] private int maxLevels;


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

    }
    #endregion

    public void StartLevel()
    {
        UIManager.Instance.SwitchUIPanel(UIPanelState.Gameplay);
        foreach(GameObject g in controlObjects)
        {
            g.SetActive(true);
        }
    }

    public void WinLevel()
    {
        UIManager.Instance.SwitchUIPanel(UIPanelState.GameWin);
        foreach (GameObject g in controlObjects)
        {
            g.SetActive(false);
        }
        PlayerPrefs.SetInt("level", currentLevel + 1);
        currentLevel++;

        if (currentLevel > maxLevels)
        {
            currentLevel = Random.Range(1, maxLevels);
        }
    }

    public void LoseLevel()
    {
        UIManager.Instance.SwitchUIPanel(UIPanelState.GameLose);
    }

    #region Scene Management

    public void NextLevel()
    {
        SceneManager.LoadScene("Level " + currentLevel);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Level " + currentLevel);
    }

    #endregion

    #region Public Core Functions
   
    public void AddScore(int value)
    {
        currentScore += value;
        UIManager.Instance.UpdateScore(currentScore);
    }

  

  
    #endregion
}
