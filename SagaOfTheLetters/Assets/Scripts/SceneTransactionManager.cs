using UnityEngine.SceneManagement;

public static class SceneTransactionManager 
{
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
