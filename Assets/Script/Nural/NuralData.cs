
namespace Nural {
    [System.Serializable]
    public struct NuralData
    {
        static public NuralData Random(int width, int height, float weightRange = 10.0f, float biasRange = 10.0f)
        {
            float[,] weights = new float[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    weights[x,  y] = UnityEngine.Random.Range(-weightRange, weightRange);
                }
            }

            return new NuralData(weights, UnityEngine.Random.Range(-biasRange, biasRange));
        }

        static public NuralData Add(NuralData data1, NuralData data2)
        {
            if (data1.weights.GetLength(0) != data2.weights.GetLongLength(0))
                throw new System.ArgumentException("");
            if (data1.weights.GetLength(1) != data2.weights.GetLongLength(1))
                throw new System.ArgumentException("");

            return new NuralData(Math.Matrices.Add(data1.weights, data2.weights), data1.bias + data2.bias);
        }

        static public float Proccess(NuralData data, float[] inputs)
        {
            float[,] metricInputs = new float[1, inputs.Length];

            for (int i = 0; i < inputs.Length; i++)
                metricInputs[0, i] = inputs[i];

            return Math.Matrices.Sum(Math.Matrices.DotProduct(metricInputs, data.weights)) + data.bias;
        }

        public float[,] weights;
        public string[] savedWeights;
        public float bias;

        public NuralData(float[,] _weights, float _bias)
        {
            weights = _weights;
            savedWeights = null;
            bias = _bias;
        }
    }
}