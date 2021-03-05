using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nural;

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

        float jump = NuralData.Proccess(data, inputs);

        if (jump > 0)
        {
            Jump();
        }
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        gameObject.SetActive(false);
        NuralGameControl.ins.BirdOver(this);
    }
}