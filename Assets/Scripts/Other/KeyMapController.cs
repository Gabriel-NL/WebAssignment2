using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyMapController : MonoBehaviour
{
    private Dictionary<string, string> default_button_map;
    private List<KeyBindController> keymap_buttons= new List<KeyBindController>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //PlayerPrefs.DeleteAll();
    }

    public void AddKeyBinderToList(KeyBindController input){
        keymap_buttons.Add(input);
    }

    public void VerifyKeyRepetitions(){
        // Step 1: Create a dictionary to store keybinds and their associated buttons
        Dictionary<KeyCode, List<KeyBindController>> keybinds = new Dictionary<KeyCode, List<KeyBindController>>();

         // Step 2: Populate the dictionary with the keybinds from the keymap_buttons list
        foreach (KeyBindController keyBindController in keymap_buttons)
        {
            KeyCode assignedKey = keyBindController.assignedKey; // Get the assigned key for the button
            
            if (!keybinds.ContainsKey(assignedKey))
            {
                // If the key is not in the dictionary, add it with a new list
                keybinds[assignedKey] = new List<KeyBindController>();
            }
            // Add the KeyBindController to the list of the corresponding key
            keybinds[assignedKey].Add(keyBindController);
        }
        // Step 3: Check for key repetitions and set button colors accordingly
        foreach (KeyValuePair<KeyCode, List<KeyBindController>> entry in keybinds)
        {
            // If there is more than one button using the same key, make them red
            if (entry.Value.Count > 1)
            {
                foreach (KeyBindController button in entry.Value)
                {
                    // Assuming KeyBindController has an Image component to change color
                    button.SetButtonColor(Color.red);
                }
            }
            else
            {
                // If only one button is using the key, set the color to white
                foreach (KeyBindController button in entry.Value)
                {
                    button.SetButtonColor(Color.white);
                }
            }
        }
    }

    public void ResetToDefault()
    {
        default_button_map = new Dictionary<string, string>();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
