using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance;

    public GameObject SettingPanel;
    private bool IsSettingPanel;

    public GameObject AskPanel;
    private bool IsAskPanel;

    public GameObject BGMusic;
    private AudioSource bgMusic;
    private bool IsBGMusic = true;

    public Slider VolumeSlider;
    private void Start()
    {
        bgMusic = BGMusic.GetComponent<AudioSource>();
        AudioListener.volume = VolumeSlider.value;
        VolumeSlider.onValueChanged.AddListener(OnChageVolume);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseOnClick();
        }

    }
    public void PauseOnClick()
    {
        //UI面板展开
        IsSettingPanel = !IsSettingPanel;
        SettingPanel.SetActive(IsSettingPanel);

        if (IsSettingPanel)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void SetBGMusic() {
        //背景音乐开关
        IsBGMusic = !IsBGMusic;
        bgMusic.enabled = IsBGMusic;
    }

    public void OnChageVolume(float value){
        AudioListener.volume = value;
    }


    public void ExitMainMenu()
    {
        IsSettingPanel = false;
        SettingPanel.SetActive(IsSettingPanel);
        IsAskPanel = !IsAskPanel;
        AskPanel.SetActive(IsAskPanel);
    }
    public void OnAskYesButtonClick() {
        
        if (IsAskPanel) {
            int score = PlayerScoreManager.Instance.playerScore;
            ConnectSQLite.Instance.SaveScore(score);
            PlayerScoreManager.Instance.playerScore = 0;
            //Debug.Log("分数清零");
            SceneManager.LoadScene(0);
        }
    }
    public void OnAskNoButtonClick() {
        if (IsAskPanel) {
            IsAskPanel = false;
            AskPanel.SetActive(IsAskPanel);
            IsSettingPanel = true;
            SettingPanel.SetActive(IsSettingPanel);
        }
    }
}
