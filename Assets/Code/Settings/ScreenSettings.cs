using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSettings : MonoBehaviour {
    //Bus laikomos visos ekrano rezoliucijos, priklausomai nuo kompiuterio
    Resolution[] allResolutions;
    private List<Vector2Int> validRes;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] Toggle screenTypeToggle;
    public int savedIndex;

    public List<Vector2Int> allowedResolutions = new() {
        new(800, 1280), new(1176, 664), new(1366, 768),
        new(1440, 900), new(1600, 900), new(1680, 1050),
        new(1920, 1200), new(1920, 1080), new(2560, 1600),
        new(2560, 1440), new(3840, 2160)
    };

    private void Start() {
        FillResolutionDropdown();
        UpdateScreenCheck();
    }

    public void FillResolutionDropdown() {
        //Priskiriamos rezoliucijos
        allResolutions = Screen.resolutions;
        List<Vector2Int> systemResolutions = allResolutions.Select(res => new Vector2Int(res.width, res.height)).Distinct().ToList();

        validRes = systemResolutions.Where(r => allowedResolutions.Contains(r)).ToList();

        //Išvalomas dropdown laukelis, kad nebūtų neteisingų ar atsitiktinių reikšmių
        resolutionDropdown.ClearOptions();

        //Rezoliucijų kintamasis paverčiamas sąrašu, kad būtų galima atvaizduoti laukelyje
        List<string> options = validRes.Select(res => $"{res.x}x{res.y}").ToList();

        //Į dropdown laukelį pridedamos rezoliucijos reikšmės
        resolutionDropdown.AddOptions(options);

        //Vartotojo ekrano rezoliucija įdedama į pirmą dropdown laukelio reikšmę
        Vector2Int currentRes = new(Screen.currentResolution.width, Screen.currentResolution.height);
        int currentIndex = validRes.IndexOf(currentRes);
        resolutionDropdown.value = currentIndex;

        resolutionDropdown.RefreshShownValue();

        SetResolution(currentIndex);
    }

    //Atnaujinama ekrano rezoliucija
    public void SetResolution(int index) {
        //Randama rezoliucija, kurią norima naudoti
        int selectedWidth = validRes[index].x;
        int selectedHeight = validRes[index].y;
        PlayerPrefs.SetInt("ResolutionX", selectedWidth);
        PlayerPrefs.SetInt("ResolutionY", selectedHeight);

        //Nustatoma ekrano rezoliucija
        if (Screen.fullScreen == true) {
            Screen.SetResolution(selectedWidth, selectedHeight, FullScreenMode.FullScreenWindow, Screen.currentResolution.refreshRateRatio);
        } else {
            Screen.SetResolution(selectedWidth, selectedHeight, FullScreenMode.Windowed, Screen.currentResolution.refreshRateRatio);
        }
    }

    //Yra tikrinama, ar yra naudojamas viso ekrano režimas
    public void SetFullScreen(bool isFullScreen) {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("fullScreen", Convert.ToInt16(isFullScreen));
    }

    public void UpdateScreenCheck() {
        if (Screen.fullScreen == true) {
            screenTypeToggle.isOn = true;
        } else {
            screenTypeToggle.isOn = false;
        }
    }
}