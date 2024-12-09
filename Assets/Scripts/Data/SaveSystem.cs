using UnityEngine;
using System.IO;

public static class SaveSystem {
    //Duomenų saugojimo katalogas vartotojo kompiuteryje
    public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/saves/";

    //Kintamajam suteikiamas failo tipas .json, kad nereikėtų kartoti to paties teksto
    public static readonly string FILE_EXT = ".json";

    public static void Save(string fileName, string dataToSave) {
        //Jei nėra saugojimo katalogo, sukuriamas naujas katalogas
        if (!Directory.Exists(SAVE_FOLDER)) {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
        //Įrašomi duomenys į failą
        File.WriteAllText(SAVE_FOLDER + fileName + FILE_EXT, dataToSave);
    }

    public static string Load(string fileName) {
        //Duomenų saugojimo failo vieta
        string fileLoc = SAVE_FOLDER + fileName + FILE_EXT;

        //Jei failas egzistuoja, yra įkeliami duomenys
        if(File.Exists(fileLoc)) {
            string loadedData = File.ReadAllText(fileLoc);
            return loadedData;
        } else {
            return null;
        }
    }
}