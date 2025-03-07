using UnityEngine;

public class LoadMenu : MonoBehaviour
{
    [SerializeField] GameObject saveSelect;
    public bool listingSaves;
    public void loadMenu()
    {
        if (listingSaves)
        {
            saveSelect.SetActive(false);
            listingSaves = false;
        }
        else
        {
            saveSelect.SetActive(true);
            Time.timeScale = 0;
            listingSaves = true;
        }
    }
}
