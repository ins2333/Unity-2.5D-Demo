using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    /// <summary>
    /// 游戏内的设置管理器，负责处理游戏内的设置面板、存档、退出等功能
    /// </summary>
    public static SettingManager Instance;

    public GameObject SettingPanel;
    private bool IsSettingPanel;

    private bool IsSave;
    private bool IsExit;

    public GameObject AskPanel;
    private bool IsAskPanel;
    public Text AskPanelText;

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

    public void OnSaveButtonClick()
    {
        //存档按钮，弹出确认面板
        IsSave = true;
        IsSettingPanel = false;
        SettingPanel.SetActive(IsSettingPanel);
        IsAskPanel = !IsAskPanel;
        AskPanelText.text = "Confirm Save The Score?";
        AskPanel.SetActive(IsAskPanel);
    }


    public void SetBGMusic() {
        //背景音乐开关
        IsBGMusic = !IsBGMusic;
        bgMusic.enabled = IsBGMusic;
    }

    public void OnChageVolume(float value){
        //音量调节
        AudioListener.volume = value;
    }


    public void ExitMainMenu()
    {
        IsExit = true;
        IsSettingPanel = false;
        SettingPanel.SetActive(IsSettingPanel);
        IsAskPanel = !IsAskPanel;
        AskPanel.SetActive(IsAskPanel);
    }
    public void OnAskYesButtonClick() {

        if (IsAskPanel && IsExit) {
            int score = PlayerScoreManager.Instance.playerScore;
            ConnectSQLite.Instance.SaveScore(score);
            PlayerScoreManager.Instance.playerScore = 0;
            //Debug.Log("分数清零");
            SceneManager.LoadScene(0);
        } else if (IsAskPanel && IsSave) {

            //存档按钮，调用数据库实例方法存档数据
            int score = PlayerScoreManager.Instance.playerScore;
            ConnectSQLite.Instance.SaveScore(score);

            IsAskPanel = false;
            AskPanel.SetActive(IsAskPanel);
            IsSettingPanel = true;
            SettingPanel.SetActive(IsSettingPanel);
            IsSave = false;
        }
    }
    public void OnAskNoButtonClick() {
        //点击否，返回设置面板
        if (IsAskPanel && IsExit)
        {
            IsAskPanel = false;
            AskPanel.SetActive(IsAskPanel);
            IsSettingPanel = true;
            SettingPanel.SetActive(IsSettingPanel);
            IsExit = false;
        }
        else if (IsAskPanel && IsSave) {
            IsAskPanel = false;
            AskPanel.SetActive(IsAskPanel);
            IsSettingPanel = true;
            SettingPanel.SetActive(IsSettingPanel);
            IsSave = false;
        }
    }
}
