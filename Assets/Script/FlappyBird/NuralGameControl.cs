using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuralGameControl : GameControl
{
    public static  NuralGameControl ins;

    public NuralControlBird birdPrefab;
    public int birdCount;

    private NuralControlBird[] m_birds;
    private float[] m_birdResults;

    private float m_gameStartTime;

    protected override void Awake() {
        ins = this;
    }

    private void Start()
    {
        m_birds = new NuralControlBird[birdCount];
        m_birdResults = new float[birdCount];

        ResetGame();
    }

    private void Update()
    {
        UpdateGround();
    }

    public void PrepareNewGeneration()
    {
        for (var i = 0; i < birdCount; i++)
        {
            if (m_birds[i] != null) Destroy(m_birds[i].gameObject);
        }

        for (var i = 0; i < birdCount; i++)
        {
            m_birds[i] = Instantiate<NuralControlBird>(birdPrefab);
            m_birdResults[i] = 0;
        }
    }

    public void BirdOver(NuralControlBird bird)
    {
        bool allDead = true;
        for (var  i = 0; i < birdCount; i++)
        {
            if (m_birds[i] == bird)
            {
                m_birdResults[i] = Time.unscaledTime - m_gameStartTime;
            }

            if (m_birds[i].gameObject.activeSelf) allDead = false;
        }

        if (allDead)
        {
            ResetGame();
        }
    }

    public override void ResetGame()
    {
        base.ResetGame();

        PrepareNewGeneration();

        m_gameStartTime = Time.unscaledTime;
    }
}
