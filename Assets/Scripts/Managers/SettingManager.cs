using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    /// <summary>
    /// 游戏内设置管理器，负责处理游戏内设置面板的按钮点击事件，控制不同面板的显示与隐藏，场景切换、读取数据等
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
        IsSave = true;
        IsSettingPanel = false;
        SettingPanel.SetActive(IsSettingPanel);
        IsAskPanel = !IsAskPanel;
        AskPanelText.text = "Confirm Save The Score?";
        AskPanel.SetActive(IsAskPanel);
    }


    public void SetBGMusic()
    {
        //背景音乐开关
        IsBGMusic = !IsBGMusic;
        bgMusic.enabled = IsBGMusic;
    }

    public void OnChageVolume(float value)
    {
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
    public void OnAskYesButtonClick()
    {

        if (IsAskPanel && IsExit)
        {
            SceneManager.LoadScene(0);
        }
        else if (IsAskPanel && IsSave)
        {

            //存档按钮，调用实例方法
            int score = PlayerScoreManager.Instance.playerScore;
            ConnectSQLite.Instance.SaveScore(score);

            IsAskPanel = false;
            AskPanel.SetActive(IsAskPanel);
            IsSettingPanel = true;
            SettingPanel.SetActive(IsSettingPanel);
            IsSave = false;
        }
    }
    public void OnAskNoButtonClick()
    {
        if (IsAskPanel && IsExit)
        {
            IsAskPanel = false;
            AskPanel.SetActive(IsAskPanel);
            IsSettingPanel = true;
            SettingPanel.SetActive(IsSettingPanel);
            IsExit = false;
        }
        else if (IsAskPanel && IsSave)
        {
            IsAskPanel = false;
            AskPanel.SetActive(IsAskPanel);
            IsSettingPanel = true;
            SettingPanel.SetActive(IsSettingPanel);
            IsSave = false;
        }
    }
}