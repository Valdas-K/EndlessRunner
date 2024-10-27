using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Settings : MonoBehaviour
{
    //Bus laikomos visos ekrano rezoliucijos, priklausomai nuo kompiuterio
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    private void Start()
    {
        //Priskiriamos rezoliucijos
        resolutions = Screen.resolutions;

        //Išvalomas dropdown laukelis, kad nebūtų neteisingų ar atsitiktinių reikšmių
        resolutionDropdown.ClearOptions();

        //Rezoliucijų kintamasis paverčiamas sąrašu, kad būtų galima atvaizduoti laukelyje
        List<string> options = new List<string>();

        //Vartotojo ekrano rezoliucijos indeksas (rezoliucijų masyve), kurio bus ieškoma
        int currentResIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            //Į sąrašą pridedami elementai
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRateRatio + "hz";
            options.Add(option);

            //Ieškoma vartotojo ekrano rezoliucija, kad dropdown laukelyje užimtų pirmą reikšmę
            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height &&
                resolutions[i].refreshRateRatio.ToString() == Screen.currentResolution.refreshRateRatio.ToString())
            {
                currentResIndex = i;
            }
        }
        //Į dropdown laukelį pridedamos rezoliucijos reikšmės
        resolutionDropdown.AddOptions(options);

        //Varototjo ekrano rezoliucija įdedama į pirmą dropdown laukelio reikšmę
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }

    //Atnaujinama ekrano rezoliucija
    public void SetResolution(int resIndex)
    {
        //Randama rezoliucija, kurią norima naudoti
        Resolution resolution = resolutions[resIndex];

        //Nustatoma ekrano rezoliucija
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //Yra tikrinama, ar yra naudojamas viso ekrano režimas
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
