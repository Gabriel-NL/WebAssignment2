using UnityEngine;

public class Quit : MonoBehaviour
{
    public void quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
