using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuralControlBird : BirdContoller
{
    public NuralData data;

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        gameObject.SetActive(false);
        NuralGameControl.ins.BirdOver(this);
    }
}

[System.Serializable]
public class NuralData
{
    public double[] weights;
    public double bias;
}