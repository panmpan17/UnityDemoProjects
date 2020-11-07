using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameControl : MonoBehaviour
{
    static public GameControl ins;

    [SerializeField]
    private GameObject[] groundSetPrefabs;

    [SerializeField]
    private Vector3 spawnPosition;
    [SerializeField]
    private float distroyX;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float spawnGap;
    private float spawnGapProgess;

    [SerializeField]
    private BirdContoller birdContoller;

    [SerializeField]
    private GameObject startText;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    private int score;

    private List<GameObject> grounds;

    private void Awake() {
        ins = this;
        birdContoller.enabled = false;
        grounds = new List<GameObject>();
    }

    void Update()
    {
        if (!birdContoller.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                startText.SetActive(false);
                birdContoller.enabled = true;
                score = 0;
                scoreText.text = "0";

                while (grounds.Count > 0)
                {
                    Destroy(grounds[0]);
                    grounds.RemoveAt(0);
                }

                SpawnGround();
            }
        }
        else
        {
            spawnGapProgess += Time.deltaTime;
            if (spawnGapProgess >= spawnGap)
                SpawnGround();

            for (int i = 0; i < grounds.Count; i++)
            {
                Vector3 position = grounds[i].transform.position;
                bool cross = position.x <= birdContoller.transform.position.x;

                position.x += moveSpeed * Time.deltaTime;

                if (!cross && position.x <= birdContoller.transform.position.x)
                {
                    score += 1;
                    scoreText.text = score.ToString();
                }

                if (position.x <= distroyX)
                {
                    Destroy(grounds[i]);
                    grounds.RemoveAt(i);
                    i--;
                    break;
                }

                grounds[i].transform.position = position;
            }
        }
    }

    void SpawnGround()
    {
        spawnGapProgess = 0;

        int index = Random.Range(0, groundSetPrefabs.Length);
        GameObject newGround = Instantiate(groundSetPrefabs[index]);
        newGround.transform.position = spawnPosition;

        grounds.Add(newGround);
    }

    public void GameOver()
    {
        startText.SetActive(true);
    }
}
