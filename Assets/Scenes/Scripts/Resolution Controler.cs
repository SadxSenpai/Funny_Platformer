using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionControler : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filterdResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex = 0;



    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        filterdResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
#pragma warning disable CS0618 // Type or member is obsolete
        currentRefreshRate = Screen.currentResolution.refreshRate;
#pragma warning restore CS0618 // Type or member is obsolete

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                filterdResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filterdResolutions.Count; i++)
        {
            string resolutionOption = filterdResolutions[i].width + " x " + filterdResolutions[i].height + " " + filterdResolutions[i].refreshRate + " Hz";
            options.Add(resolutionOption);
            if (filterdResolutions[i].width == Screen.width && filterdResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filterdResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
