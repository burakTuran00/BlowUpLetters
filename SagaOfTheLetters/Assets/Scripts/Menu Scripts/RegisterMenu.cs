using UnityEngine;

public class RegisterMenu : MonoBehaviour
{
    public void LogInButton()
    {
        SceneTransactionManager.LoadScene("StartMenu");
    }
}
