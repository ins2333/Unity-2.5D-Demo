using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoginManager : MonoBehaviour
{
    public InputField UserName;
    public InputField Password;
    public Text MessageText;
    public void OnLoginButtonClick()
    {
        string userName = UserName.text;
        string password = Password.text;
        if (userName == "admin" && password == "admin")
        {

            SceneManager.LoadScene(1);
        }
        else {
            MessageText.text = "用户名或密码错误！";
            MessageText.enabled = true;
            StartCoroutine(HideMessage());
        }
    }
    IEnumerator HideMessage() {
        yield return new WaitForSeconds(3);
        MessageText.enabled = false;
    }  
}
