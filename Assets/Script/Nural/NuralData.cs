
namespace Nural {
    [System.Serializable]
    public struct NuralData
    {
        static public NuralData Random(int weightNum = 3, float weightMin = -10.0f, float weightMax = 10.0f, float biasMin = -10.0f, float biasMax = 10.0f)
        {
            float[] weights = new float[weightNum];
            for (var i = 0; i < weightNum; i++)
            {
                weights[i] = UnityEngine.Random.Range(weightMin, weightMax);
            }

            return new NuralData(weights, UnityEngine.Random.Range(biasMin, biasMax));
        }

        static public NuralData Add(NuralData data1, NuralData data2)
        {
            if (data1.weights.Length != data2.weights.Length)
            {
                throw new System.ArgumentException("");
            }

            float[] weights = new float[data1.weights.Length];

            for (var i = 0; i < weights.Length; i++)
            {
                weights[i] = data1.weights[i] + data2.weights[i];
            }

            return new NuralData(weights, data1.bias + data2.bias);
        }

        static public float Proccess(NuralData data, float[] inputs)
        {
            float result = 0;
            for (var i = 0; i < inputs.Length; i++)
            {
                result += inputs[i] * data.weights[i];
            }
            result += data.bias;
            return result;
        }

        public float[] weights;
        public float bias;

        public NuralData(float[] _weights, float _bias)
        {
            weights = _weights;
            bias = _bias;
        }
    }
}