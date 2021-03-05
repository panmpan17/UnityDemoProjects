using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Nural
{
    public static class SavingSystem
    {
        public static void SaveData(NuralData data, string fileName)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            FileStream stream = new FileStream(Path.Combine(Application.persistentDataPath, fileName), FileMode.Create);
            binaryFormatter.Serialize(stream, data);
            stream.Close();
        }

        public static NuralData GetData(string fileName)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            FileStream stream = new FileStream(Path.Combine(Application.persistentDataPath, fileName), FileMode.Open);
            NuralData data = (NuralData)binaryFormatter.Deserialize(stream);
            stream.Close();

            return data;
        }
    }
}