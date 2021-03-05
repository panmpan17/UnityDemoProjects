using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using TMPro;


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

    protected override void Awake() {
        ins = this;
    }

    private void Start()
    {
        m_birdResults = new float[birdCount];

        startText.gameObject.SetActive(false);

        NuralControlBird bird = Instantiate<NuralControlBird>(birdPrefab);
        bird.data = GetData("best-data.data");
        // ResetGame();
        SpawnGround();
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
        if (m_birds != null)
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

            SaveData(bestData, string.Format("generation-{0}.data", m_generationCount));

            m_birds[0] = Instantiate<NuralControlBird>(birdPrefab);
            m_birds[0].data = bestData;

            for (var i = 1; i < birdCount; i++)
            {
                m_birds[i] = Instantiate<NuralControlBird>(birdPrefab);
                m_birds[i].data = NuralData.Add(bestData, NuralData.Random(weightNum: 6, weightMin: -1f, weightMax: 1f, biasMin: -1f, biasMax: 1f));
            }
        }
        else
        {
            m_birds = new NuralControlBird[birdCount];

            for (var i = 0; i < birdCount; i++)
            {
                m_birds[i] = Instantiate<NuralControlBird>(birdPrefab);
                m_birds[i].data = NuralData.Random(weightNum: 6);
            }
        }

        m_generationCount += 1;
        generationCountText.text = m_generationCount.ToString();
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
                SaveData(m_birds[i].data, "best-data.data");
                break;
            }
        }
    }

    public void SaveData(NuralData data, string fileName)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream stream = new FileStream(Path.Combine(Application.persistentDataPath, fileName), FileMode.Create);
        binaryFormatter.Serialize(stream, data);
        stream.Close();
    }

    public NuralData GetData(string fileName)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream stream = new FileStream(Path.Combine(Application.persistentDataPath, fileName), FileMode.Open);
        NuralData data = (NuralData)binaryFormatter.Deserialize(stream);
        stream.Close();

        return data;
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
