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
            // BinaryFormatter binaryFormatter = new BinaryFormatter();

            // FileStream stream = new FileStream(Path.Combine(Application.persistentDataPath, fileName), FileMode.Create);
            // binaryFormatter.Serialize(stream, data);
            // stream.Close();

            string[] savedWeights = new string[data.weights.GetLength(0)];
            for (int x = 0; x < savedWeights.Length; x++)
            {
                string row = "";
                for (int y = 0; y < data.weights.GetLength(1); y++)
                {
                    row += data.weights[x, y].ToString();
                    if (y < data.weights.GetLength(1) - 1)
                        row += ",";
                }
                savedWeights[x] = row;
            }

            data.savedWeights = savedWeights;

            System.IO.File.WriteAllText(Path.Combine(Application.persistentDataPath, fileName), JsonUtility.ToJson(data, true));
        }

        public static NuralData GetData(string fileName)
        {
            // BinaryFormatter binaryFormatter = new BinaryFormatter();

            // FileStream reader = new FileStream(Path.Combine(Application.persistentDataPath, fileName), FileMode.Open);
            // NuralData data = (NuralData)binaryFormatter.Deserialize(stream);
            // stream.Close();

            StreamReader reader = new StreamReader(Path.Combine(Application.persistentDataPath, fileName));
            string text = reader.ReadToEnd();
            reader.Close();

            NuralData data = JsonUtility.FromJson<NuralData>(text);

            string[] s = data.savedWeights[0].Split(',');
            float[,] weights = new float[data.savedWeights.Length, s.Length];

            for (int x = 0; x < data.savedWeights.Length; x++)
            {
                string[] row = data.savedWeights[x].Split(',');

                for (var y = 0; y < row.Length; y++)
                    weights[x, y] = float.Parse(row[y]);
            }

            data.weights = weights;

            return data;
        }
    }
}