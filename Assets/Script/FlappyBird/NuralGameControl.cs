using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using TMPro;
using Nural;


public class NuralGameControl : GameControl
{
    public static  NuralGameControl ins;

    public NuralControlBird birdPrefab;
    public int birdCount;

    private NuralControlBird[] m_birds;
    private float[] m_birdResults;


    [System.NonSerialized]
    public GameObject cloestGround;

    [SerializeField]
    private float x;

    [SerializeField]
    private TextMeshProUGUI bestAliveTimeText, generationCountText;

    private float m_gameStartTime, m_bestAliveTime;
    private int m_generationCount;

    [SerializeField]
    private string preloadNuralData;

    [SerializeField]
    private float evolveVariant;

    protected override void Awake() {
        ins = this;
    }

    private void Start()
    {
        m_birdResults = new float[birdCount];

        startText.gameObject.SetActive(false);

        ResetGame();
    }

    private void Update()
    {
        UpdateGround();

        cloestGround = grounds[0];
        float bestDistance = cloestGround.transform.position.x - x;
        for (var i = 0; i < grounds.Count; i++)
        {
            float distance = grounds[i].transform.position.x - x;
            if (bestDistance < 0 || (distance < bestDistance && distance > 0))
            {
                bestDistance = distance;
                cloestGround = grounds[i];
            }
        }

        if (Time.unscaledTime - m_gameStartTime > m_bestAliveTime)
        {
            m_bestAliveTime = Time.unscaledTime - m_gameStartTime;
            bestAliveTimeText.text = (Time.unscaledTime - m_gameStartTime).ToString("0.00");
        }
    }

    public void PrepareNewGeneration()
    {
        if (m_birds != null) ImproveFromLastGeneration();
        else
        {
            m_birds = new NuralControlBird[birdCount];

            if (preloadNuralData != "")
            {
                NuralData preloadData = SavingSystem.GetData("best-data.data");

                m_birds[0] = Instantiate<NuralControlBird>(birdPrefab);
                m_birds[0].data = preloadData;

                for (var i = 1; i < birdCount; i++)
                {
                    m_birds[i] = Instantiate<NuralControlBird>(birdPrefab);
                    m_birds[i].data = EvolveFromBaseData(preloadData);
                }
            }
            else
            {
                for (var i = 0; i < birdCount; i++)
                {
                    m_birds[i] = Instantiate<NuralControlBird>(birdPrefab);
                    m_birds[i].data = NuralData.Random(weightNum: 6);
                }
            }
        }

        m_generationCount += 1;
        generationCountText.text = m_generationCount.ToString();
    }

    private void ImproveFromLastGeneration()
    {
        NuralData bestData = m_birds[0].data;
        float bestTime = 0;

        for (var i = 0; i < birdCount; i++)
        {
            if (m_birdResults[i] > bestTime)
            {
                bestData = m_birds[i].data;
                bestTime = m_birdResults[i];
            }
            Destroy(m_birds[i].gameObject);
        }

        SavingSystem.SaveData(bestData, string.Format("generation-{0}.data", m_generationCount));

        m_birds[0] = Instantiate<NuralControlBird>(birdPrefab);
        m_birds[0].data = bestData;

        for (var i = 1; i < birdCount; i++)
        {
            m_birds[i] = Instantiate<NuralControlBird>(birdPrefab);
            m_birds[i].data = EvolveFromBaseData(bestData);
        }
    }

    private NuralData EvolveFromBaseData(NuralData data)
    {
        return NuralData.Add(data, NuralData.Random(
            weightNum: 6,
            weightMin: -evolveVariant, weightMax: evolveVariant,
            biasMin: -evolveVariant, biasMax: evolveVariant));
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

    private void OnDrawGizmosSelected() {
        if (cloestGround != null)
        {
            Gizmos.DrawSphere(cloestGround.transform.position, 0.1f);
        }
        Gizmos.DrawSphere(new Vector3(x, 0, 0), 0.1f);
    }

    public void ScanBestDataFromAlives()
    {
        for (var i = 0; i < m_birds.Length; i++)
        {
            if (m_birds[i].gameObject.activeSelf)
            {
                SavingSystem.SaveData(m_birds[i].data, "best-data.data");
                break;
            }
        }
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Game/Open Persistent Data Path", false, 0)]
    static public void M_OpenPersistentDataPath()
    {
        UnityEditor.EditorUtility.RevealInFinder(Application.persistentDataPath);
    }


    [UnityEditor.MenuItem("Game/Save Best Data From Alive", false, 0)]
    static public void M_ScanBestDataFromAlives()
    {
        ins.ScanBestDataFromAlives();
    }
#endif
}
