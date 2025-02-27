using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;

public class KeyBindController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI button_text;
    [SerializeField] private Image keybind_image;
    [SerializeField] private string current_function;
    private KeyMapController controller;

    public KeyCode assignedKey = KeyCode.None; // Stored keybind

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        assignedKey = LoadKeybind(current_function,assignedKey);
        UpdateButtonText(assignedKey.ToString());
        controller=FindFirstObjectByType<KeyMapController>();
        controller.AddKeyBinderToList(this);
    }

    public void StartKeybinding()
    {
        Debug.Log($"Press any key to do {current_function}");
        StartCoroutine(WaitForKeyPress());
    }
    

    IEnumerator WaitForKeyPress()
    {
        button_text.text = "...";
        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                assignedKey = key;
                SaveKeybind();
                break;
            }
        }

        UpdateButtonText(assignedKey.ToString());
    }

    void SaveKeybind()
    {
        PlayerPrefs.SetString(current_function, assignedKey.ToString());
        PlayerPrefs.Save();
        controller.VerifyKeyRepetitions();
    }

    void UpdateButtonText(string txt)
    {
        if (assignedKey != KeyCode.None)
        {
            button_text.text = txt;
        }
    }
    public KeyCode LoadKeybind(string function,KeyCode bind)
    {
         KeyCode collectedKeyBind;
        if (PlayerPrefs.HasKey(function))
        {
            string keycode_string = PlayerPrefs.GetString(function);
            collectedKeyBind = (KeyCode)System.Enum.Parse(typeof(KeyCode), keycode_string);
        }else
        {
            collectedKeyBind=bind;
            PlayerPrefs.SetString(current_function, bind.ToString());
            PlayerPrefs.Save();
        }
        return collectedKeyBind;
    }

    public string GetKeyBind(){
        return assignedKey.ToString();
    }   
    public void SetButtonColor(Color new_color){
        keybind_image.color=new_color;
    }
    void Update()
    {
        if (assignedKey != KeyCode.None && Input.GetKeyDown(assignedKey))
        {
            Debug.Log($"Key {assignedKey} pressed!");
        }
    }

}
