using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Scene scene;
    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }
    public void ReloadLevel()
    {
        
        SceneManager.LoadScene(scene.name);        
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(scene.buildIndex + 1);
    }
}
