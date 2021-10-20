using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Properties
    public static GameManager Instance = null;

    [Header("Camera Reference")]
    [SerializeField] private GameObject matchStickCam = null;
    [SerializeField] private GameObject matchBurnCam = null;
    [SerializeField] private GameObject zoomOutCamera = null;


    [Header("Attributes")]
    [SerializeField] private int currentScore;
    [SerializeField] private int currentFoodRoasted;
    [SerializeField] private int currentGem;
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
        UIManager.Instance.UpdateLevel(currentLevel);
    }
    #endregion

    public void StartLevel()
    {
        UIManager.Instance.SwitchUIPanel(UIPanelState.Gameplay);

    }

    public void WinLevel()
    {
        UIManager.Instance.SwitchUIPanel(UIPanelState.GameWin);
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

    public void AddCurrentFoodRoasted()
    {
        currentFoodRoasted++;

    }
    public void CameraZoomOut()
    {
        matchStickCam.SetActive(false);
        zoomOutCamera.SetActive(true);
    }

    public void CameraZoomIn()
    {
        matchStickCam.SetActive(true);
        zoomOutCamera.SetActive(false);
    }



  
    #endregion
}
