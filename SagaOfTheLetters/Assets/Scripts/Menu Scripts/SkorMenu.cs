using UnityEngine;

public class SkorMenu : MonoBehaviour
{
    public void ReturnButton()
    {
        SceneTransactionManager.LoadScene("StartMenu");
    }
}
