using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMainMemuManager : MonoBehaviour
{
    public GameObject ButtonPanel;
    public GameObject NewStartPanel;
    public GameObject ExitPanel;

    private bool IsButtonPanel = true;
    private bool IsNewStartPanel;
    private bool IsExitPanel;
    private void Awake()
    {
        
    }
    public void OnStartButtonClick() {
        IsButtonPanel = !IsButtonPanel;
        ButtonPanel.SetActive(IsButtonPanel);
        if (!IsButtonPanel) {
            IsNewStartPanel = true;
            NewStartPanel.SetActive(IsNewStartPanel);    
        }
        
    }
    public void OnConfirmButtonClick() {
        if (IsNewStartPanel)
        {
            SceneManager.LoadScene(1);
        } else if (IsExitPanel) {
            #if  UNITY_EDITOR
                 UnityEditor.EditorApplication.isPlaying = false;
            #else
                 Application.Quit();
            #endif
        }
    }


    public void OnBackButtonClick() {
        if (IsNewStartPanel) {
            IsButtonPanel = true;
            IsNewStartPanel = false;
            NewStartPanel.SetActive(IsNewStartPanel);
            ButtonPanel.SetActive(IsButtonPanel);
        }
        else if(IsExitPanel){
            IsButtonPanel = true;
            IsExitPanel = false;
            ExitPanel.SetActive(IsExitPanel);
            ButtonPanel.SetActive(IsButtonPanel);
        }
    }


    public void OnLoadButtonClick() { 
        
    }

    public void OnSettingButtonClick() {
        
    }

    public void OnExitButtonClick()
    {
        IsButtonPanel = !IsButtonPanel;
        ButtonPanel.SetActive(IsButtonPanel);
        if (!IsButtonPanel)
        {
            IsExitPanel = true;
            ExitPanel.SetActive(IsExitPanel);
        }
    }    
}
