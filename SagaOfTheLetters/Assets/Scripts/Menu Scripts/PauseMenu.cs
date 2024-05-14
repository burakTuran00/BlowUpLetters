using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausedMenu;
    [SerializeField] private Button pausedButton;
    [SerializeField] private GameObject currentScoreMenu;
    private bool condition;
    public void PuaseButton()
    {
        condition = true;

        pausedMenu.SetActive(condition);
        currentScoreMenu.SetActive(!condition);
        
        GameManager.Instance.PauseGame(condition);
        pausedButton.interactable = !condition;
    }
    public void ResumeButton()
    {
        condition = false;

        pausedMenu.SetActive(condition);
        currentScoreMenu.SetActive(condition);
        
        GameManager.Instance.PauseGame(condition);
        pausedButton.interactable = !condition;
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
