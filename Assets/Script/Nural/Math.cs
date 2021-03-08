using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Nural.Math
{
    public static class Matrices
    {
        #if UNITY_EDITOR
        [MenuItem("Game/Test Math", false, 1)]
        public static void Test()
        {
            NuralData data = NuralData.Random(3, 3);
            DebugPrint(data.weights);

            SavingSystem.SaveData(data, "test.json");

            data = SavingSystem.GetData("test.json");

            DebugPrint(data.weights);
        }
        #endif

        public static void DebugPrint(float[,] matric)
        {
            string info = "";
            for (int j = 0; j < matric.GetLength(1); j++)
            {
                for (int i = 0; i < matric.GetLength(0); i++)
                {
                    info += matric[i, j].ToString("0.0000") + ", ";
                }
                info += "\n";
            }
            Debug.Log(info);
        }

        public static float[,] Add(float[,] matric, float number)
        {
            float[,] newMatric = new float[matric.GetLength(0), matric.GetLength(1)];
            for (int i = 0; i < matric.GetLength(0); i++)
            {
                for (int j = 0; j < matric.GetLength(1); j++)
                {
                    newMatric[i, j] = matric[i, j] + number;
                }
            }

            return newMatric;
        }

        public static float[,] Add(float[,] matric1, float[,] matric2)
        {
            float[,] newMatric = new float[matric1.GetLength(0), matric1.GetLength(1)];

            for (int i = 0; i < matric1.GetLength(0); i++)
            {
                for (int j = 0; j < matric1.GetLength(1); j++)
                {
                    newMatric[i, j] = matric1[i, j] + matric2[i, j];
                }
            }

            return newMatric;
        }

        public static float[,] Multiply(float[,] matric, float number)
        {
            float[,] newMatric = new float[matric.GetLength(0), matric.GetLength(1)];
            for (int i = 0; i < matric.GetLength(0); i++)
            {
                for (int j = 0; j < matric.GetLength(1); j++)
                {
                    newMatric[i, j] = matric[i, j] * number;
                }
            }

            return newMatric;
        }

        public static float Sum(float[,] matric)
        {
            float sum = 0;

            for (int i = 0; i < matric.GetLength(0); i++)
            {
                for (int j = 0; j < matric.GetLength(1); j++)
                {
                    sum = matric[i, j];
                }
            }

            return sum;
        }

        public static float[,] DotProduct(float[,] matric1, float[,] matric2)
        {
            if (matric1.GetLength(1) != matric2.GetLength(0))
            {
                Debug.Log(matric1.GetLength(0));
                Debug.Log(matric1.GetLength(1));
                Debug.Log(matric2.GetLength(0));
                Debug.Log(matric2.GetLength(1));
                DebugPrint(matric1);
                DebugPrint(matric2);
                throw new System.ArgumentException();
            }

            int size = matric1.GetLength(1);

            float[,] newMatric = new float[matric1.GetLength(0), matric2.GetLength(1)];

            for (int x = 0 ; x < newMatric.GetLength(0); x++)
            {
                for (int y = 0; y < newMatric.GetLength(1); y++)
                {
                    float result = 0;
                    for (int i = 0; i < size; i++)
                    {
                        result += matric1[x, i] * matric2[i, y];
                    }
                    newMatric[x, y] = result;
                }
            }

            return newMatric;
        }
    }
}