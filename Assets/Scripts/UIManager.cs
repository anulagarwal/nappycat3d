using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    #region Properties
    public static UIManager Instance = null;

    [Header("Components Reference")]
  
    [SerializeField] private GameObject PointText;
    [SerializeField] private GameObject AwesomeText;


    [Header("UI Panel")]
    [SerializeField] private GameObject mainMenuUIPanel = null;
    [SerializeField] private GameObject gameplayUIPanel = null;
    [SerializeField] private GameObject gameOverWinUIPanel = null;
    [SerializeField] private GameObject gameOverLoseUIPanel = null;
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private Text mainLevelText = null;

    [Header("Fill Bar")]
    [SerializeField] private Image fillBar;
    [SerializeField] private Text fillPercent;
    [SerializeField] private Transform slider;
    [SerializeField] private float sliderMinVal;
    [SerializeField] private float sliderMaxVal;

    [Header("Sleep Fill Bar")]
    [SerializeField] private Image sleepfillBar;
    
 
    #endregion

    #region MonoBehaviour Functions
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        //SwitchControls(Controls.Touch);
    }
    #endregion

    #region Getter And Setter

    #endregion

    #region Public Core Functions
    public void SwitchUIPanel(UIPanelState state)
    {
        switch (state)
        {
            case UIPanelState.MainMenu:
                mainMenuUIPanel.SetActive(true);
                gameplayUIPanel.SetActive(false);
                gameOverWinUIPanel.SetActive(false);
                gameOverLoseUIPanel.SetActive(false);
                break;
            case UIPanelState.Gameplay:
                mainMenuUIPanel.SetActive(false);
                gameplayUIPanel.SetActive(true);
                gameOverWinUIPanel.SetActive(false);
                gameOverLoseUIPanel.SetActive(false);
                break;
            case UIPanelState.GameWin:
                mainMenuUIPanel.SetActive(false);
                gameplayUIPanel.SetActive(false);
                gameOverWinUIPanel.SetActive(true);
                gameOverLoseUIPanel.SetActive(false);
                break;
            case UIPanelState.GameLose:
                mainMenuUIPanel.SetActive(false);
                gameplayUIPanel.SetActive(false);
                gameOverWinUIPanel.SetActive(false);
                gameOverLoseUIPanel.SetActive(true);
                break;
        }
    }

  

    public void UpdateScore(int value)
    {
        scoreText.text = "" + value;
    }

    public void UpdateLevel(int level)
    {
        mainLevelText.text = "LEVEL " + level;
    }

    public void UpdateFillBar(float val)
    {
        fillBar.fillAmount = val;
        fillPercent.text = Mathf.Round( (val * 100 ))+ "%";
        
        slider.transform.localPosition = new Vector3( sliderMinVal + ((sliderMaxVal - sliderMinVal)/ (1 / val)), 0, 0);
    }

    public void UpdateSleepFillBar(float val)
    {
        sleepfillBar.fillAmount = val;        
    }
 

    public void SpawnPointText(Vector3 point)
    {
        Instantiate(PointText, point, Quaternion.identity);
    }

    public void SpawnAwesomeText(Vector3 point, string s)
    {
        GameObject g = Instantiate(AwesomeText, new Vector3(point.x, 2, point.z), Quaternion.identity);
        g.GetComponentInChildren<TextMeshPro>().text = s;
    }

    public void OpenSocialLinks(string s)
    {
        switch (s)
        {
            case "instagram":
                Application.OpenURL("http://instagram.com/momo.games_");
                break;

            case "linkedin":
                Application.OpenURL("http://linkedin.com/momo-games");

                break;

            case "twitter":
                Application.OpenURL("https://twitter.com/AnulAgarwal");
                break;

        }
    }
    #endregion

}




