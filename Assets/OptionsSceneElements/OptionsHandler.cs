using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionsHandler : MonoBehaviour
{
    //Min and max volume range
    private const int minValue = 0;
    private const int maxValue = 10;
    private int range_count = (maxValue - minValue) + 1;
    [Range(minValue, maxValue)] public int volume_level_value = 5;
    
    [Header("Add volume prefab here")]
    public GameObject volume_Level_indicator_prefab;
    [Header("Link this to the container of volume level prefab")]
    public GameObject volume_level_container;
    private Image[] volume_levels_array=new Image[11];
    
    [Header("Enables or disables debug log messages of this script")]
    public bool debug;


    private void Start()
    {
        //PlayerPrefs.DeleteAll();//Temporary, remove on final version
        //PlayerPrefs.SetInt(Constants.volume_level,volume_level_value);//Temporary, remove on final version
        if (volume_Level_indicator_prefab==false)
        {
            Debug.LogWarning("Prefab has not being assigned. Assign it on inspector");
            return;
        }
        bool is_prefab_valid=volume_Level_indicator_prefab.GetComponent<Image>();
        if (is_prefab_valid==false)
        {
            Debug.LogWarning("Prefab does not have UnityEngine.UI.Image component");
            return;
        }
        volume_level_value = PlayerPrefs.GetInt(Constants.volume_level, 5);
        PopulateVolumeIndicators();
        ChangeViewAccordingToVolume_level_value();

    }

    public void IncreaseVolume()
    {
        int temp_volume_level = PlayerPrefs.GetInt(Constants.volume_level, 5);
        if (temp_volume_level >=10)
        {
            Debugging("Max Volume reached");
            return;
        }
        temp_volume_level +=1;
        SetVolume(temp_volume_level);
    }

    public void DecreaseVolume()
    {
        int temp_volume_level = PlayerPrefs.GetInt(Constants.volume_level, 5);
        if (temp_volume_level <= 0)
        {
            Debugging("Min Volume reached");
            return;
        }
        temp_volume_level -=1;
        SetVolume(temp_volume_level);
    }


    private void SetVolume(int value)
    {
        volume_level_value =value;
        PlayerPrefs.SetInt(Constants.volume_level, value);
        ChangeViewAccordingToVolume_level_value();
    }


    private void PopulateVolumeIndicators()
    {
        //Clear transforms once
        foreach (Transform child in volume_level_container.transform)
        {
            Destroy(child.gameObject);
        }
        //Calculates the size
        RectTransform containerRect = volume_level_container.GetComponent<RectTransform>();
        float containerWidth = containerRect.rect.width;

        float instanceWidth = volume_Level_indicator_prefab.GetComponent<RectTransform>().rect.width;
        float requiredWidth = instanceWidth * range_count;

        if (containerWidth < requiredWidth)
        {
            Debug.LogWarning("Container is too small to fit 11 volume indicators!");
            return;
        }

        float startX = -containerWidth / 2f + instanceWidth / 2f; // Leftmost position
        float endX = containerWidth / 2f - instanceWidth / 2f;   // Rightmost position

        GameObject new_instance;
        for (int i = 0; i < range_count; i++)
        {
            float t = i / 9f; // Normalize between 0 and 1
            float xPos = Mathf.Lerp(startX, endX, t);
            new_instance=Instantiate(volume_Level_indicator_prefab, volume_level_container.transform);
            new_instance.transform.localPosition=new Vector3(xPos, 0,0);
            new_instance.GetComponent<RectTransform>().anchoredPosition = new Vector3(xPos, 0,0);
            volume_levels_array[i]=new_instance.GetComponent<Image>();
        }
    }

    private void ChangeViewAccordingToVolume_level_value(){
        for (int i = 0; i < volume_level_value; i++)
        {
            volume_levels_array[i].enabled=true;
        }
        for (int i = volume_level_value; i < volume_levels_array.Length; i++)
        {
            volume_levels_array[i].enabled=false;
        }
    }

    private void Debugging(string msg){
        if (debug)
            {
                Debug.Log(msg);
            }
    }



}
