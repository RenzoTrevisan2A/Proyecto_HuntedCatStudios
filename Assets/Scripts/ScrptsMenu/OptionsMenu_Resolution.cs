using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System;

public class OptionsMenu_Resolution : MonoBehaviour
{

    public TMP_Dropdown resolutionDropDown;
    Resolution[] resolutions;

    void Start()
    {
        CheckResolutions();
    }

    private void CheckResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();
        int actualResolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                actualResolution = i;
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = actualResolution;
        resolutionDropDown.RefreshShownValue();

        resolutionDropDown.value = PlayerPrefs.GetInt("resolutionNumber", 0);
    }

    public void ChangeResolutions(int resolutionIndex)
    {
        PlayerPrefs.SetInt("resolutionNumber", resolutionDropDown.value);

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
