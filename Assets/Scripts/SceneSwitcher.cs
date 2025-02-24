using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string targetScene;
    [SerializeField] private bool debug;

    public void ChangeScene()
    {
        if (debug)
        {
            Debug.Log("Clicked!");
        }
        if (!string.IsNullOrEmpty(targetScene))
        {
            SceneManager.LoadScene(targetScene);
        }
        else
        {
            Debug.LogWarning("Target scene name is empty or null.");
        }
    }
}
