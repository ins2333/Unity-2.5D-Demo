using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance;

    public GameObject Panel;
    private bool IsPanel;

    public GameObject BGMusic;
    private AudioSource bgMusic;
    private bool IsBGMusic = true;

    public Slider VolumeSlider;
    private void Awake()
    {
        

        bgMusic = BGMusic.GetComponent<AudioSource>();

    }
    private void Start()
    {
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
        IsPanel = !IsPanel;
        Panel.SetActive(IsPanel);

        if (IsPanel)
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

    public void ExitGame()
    {
        //退出游戏
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else    
            Application.Quit();
        #endif

    }
}
