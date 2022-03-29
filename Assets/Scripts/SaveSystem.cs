using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    static string extensionName = "joygame";

    public static void SaveLastLevelNum(int levelNum)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/lastlevelnum" + "." + extensionName;Debug.Log(path);
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, levelNum);
        stream.Close();
    }

    public static int LoadLastLevelNum()
    {
        int _tempLevelNumber = 1;
        string path = Application.persistentDataPath + "/lastlevelnum" + "." + extensionName;//Debug.Log(path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            _tempLevelNumber = (int)formatter.Deserialize(stream);
            stream.Close();
        }
        return _tempLevelNumber;
    }

    public static void SaveCurrencyAmount(int levelNum)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/currencyAmount" + "." + extensionName;
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, levelNum);
        stream.Close();
    }

    public static int LoadCurrencyAmount()
    {
        int tempCurrencyAmount = 0;
        string path = Application.persistentDataPath + "/currencyAmount" + "." + extensionName;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            tempCurrencyAmount = (int)formatter.Deserialize(stream);
            stream.Close();
        }
        return tempCurrencyAmount;
    }
}
