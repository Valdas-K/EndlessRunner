using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    //Duomenų saugojimo katalogas vartotojo kompiuteryje
    public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/saves/";

    //Kintamajam suteikiamas failo tipas .json, kad nereikėtų kartoti to paties teksto
    public static readonly string FILE_EXT = ".json";

    public static void Save(string fileName, Data dataToSave) {
        //Jei nėra saugojimo katalogo, sukuriamas naujas katalogas
        if (!Directory.Exists(SAVE_FOLDER)) {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
        //Įrašomi duomenys į failą
        //File.WriteAllText(SAVE_FOLDER + fileName + FILE_EXT, dataToSave);

        FileStream dataStream = new FileStream(SAVE_FOLDER + fileName, FileMode.Create);
        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(dataStream, dataToSave);
        dataStream.Close();
    }

    public static Data Load(string fileName) {
        //Duomenų saugojimo failo vieta
        //string fileLoc = SAVE_FOLDER + fileName + FILE_EXT;
        string fileLoc = SAVE_FOLDER + fileName;

        //Jei failas egzistuoja, yra įkeliami duomenys
        if(File.Exists(fileLoc)) {
            //string loadedData = File.ReadAllText(fileLoc);
            //return loadedData;

            FileStream dataStream = new FileStream(fileLoc, FileMode.Open);
            BinaryFormatter converter = new BinaryFormatter();
            Data saveData = converter.Deserialize(dataStream) as Data;
            dataStream.Close();
            return saveData;

        } else {
            return null;
        }
    }
}