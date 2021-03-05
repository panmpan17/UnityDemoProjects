using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuralControlBird : BirdContoller
{
    public NuralData data;

    void Update()
    {
        Transform upGround = NuralGameControl.ins.cloestGround.transform.GetChild(0);
        Transform downGround = NuralGameControl.ins.cloestGround.transform.GetChild(1);
        float[] inputs = new float[] {
            transform.position.x,
            transform.position.y,
            upGround.position.x,
            upGround.position.y,
            downGround.position.x,
            downGround.position.y,
        };

        float jump = DoNuralThingy(inputs);

        if (jump > 0)
        {
            Jump();
        }
    }

    float DoNuralThingy(float[] inputs)
    {
        float sum = 0;
        for (var i = 0; i < inputs.Length; i++)
        {
            sum += inputs[i] * data.weights[i];
        }
        sum += data.bias;
        return sum;
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        gameObject.SetActive(false);
        NuralGameControl.ins.BirdOver(this);
    }
}

[System.Serializable]
public struct NuralData
{
    static public NuralData Random(int weightNum=3, float weightMin=-10.0f, float weightMax=10.0f, float biasMin=-10.0f, float biasMax=10.0f)
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

    public float[] weights;
    public float bias;

    public NuralData(float[] _weights, float _bias)
    {
        weights = _weights;
        bias = _bias;
    }
}