using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OverrideKeyMaps : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction moveLeftAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ApplyKeyBindings();
    }

public Dictionary<string, KeyCode> GetAllKeys()
    {
        Dictionary<string, KeyCode> used_keys = new Dictionary<string, KeyCode>();
        string function;
        KeyCode collectedKeyBind;
        string keycode_string;
        foreach (var item in Constants.default_keys)
        {
            try
            {
                keycode_string = PlayerPrefs.GetString(item.Key);
                collectedKeyBind = (KeyCode)System.Enum.Parse(typeof(KeyCode), keycode_string);
                used_keys.Add(item.Key, collectedKeyBind);
            }
            catch (System.Exception e)
            {
                used_keys.Add(item.Key, item.Value);
                PlayerPrefs.SetString(item.Key, item.Value.ToString());
                Debug.LogException(e);
                continue;
            }


        }
        return used_keys;
    }

    public void ApplyKeyBindings()
    {
        Dictionary<string, KeyCode> keyBindings = GetAllKeys();

        foreach (var binding in keyBindings)
        {
            string functionName = binding.Key;
            KeyCode keyCode = binding.Value;

            // Get the corresponding action from PlayerInput
            InputAction action = playerInput.actions[functionName];

            if (action != null)
            {
                // Override the binding using InputSystem format
                action.ApplyBindingOverride($"<Keyboard>/{keyCode.ToString().ToLower()}");
                Debug.Log($"Rebound {functionName} to {keyCode}");
            }
            else
            {
                Debug.LogWarning($"Action {functionName} not found in PlayerInput.");
            }
        }
    }


}
