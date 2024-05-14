using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{   
    #region Variables
    [SerializeField] private Button playButton;
    [SerializeField] private Toggle[] radioButtons;
    private string _sceneName;
    #endregion

    public void PlayButton()
    {
        if(radioButtons[0].isOn)
        {
            _sceneName = "SceneOf30"; 
        }
        else if(radioButtons[1].isOn)
        {
            _sceneName = "SceneOf90";
        }
        else if(radioButtons[2].isOn)
        {
            _sceneName = "SceneOf180";
        }
        else
        {
            return;
        }

        SceneTransactionManager.LoadScene(_sceneName);
    }
    
    public void QuitButton()
    {
        Application.Quit();
    }

    public void SettingsButton()
    {
        SceneTransactionManager.LoadScene("SettingsMenu");
    }

    public void SkorBoardButton()
    {
        SceneTransactionManager.LoadScene("SkorMenu");
    }
}
