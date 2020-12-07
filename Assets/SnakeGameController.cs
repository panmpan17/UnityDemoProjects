using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SnakeGameController : MonoBehaviour
{
    public static SnakeGameController ins;

    [SerializeField]
    private GameObject pointPrefab;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    private int score;

    [SerializeField]
    private int minX, maxX, minY, maxY;
    [SerializeField]
    private Grid grid;

    private bool waitRestart;

    private SnakeController snake;

    private void Awake() {
        ins = this;
    }

    void Start() {
        snake = FindObjectOfType<SnakeController>();
        SpawnPoints();
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        //     SpawnPoints();
        
        if (waitRestart && Input.anyKeyDown)
        {
            snake.ResetPlayer();
            score = 0;
            scoreText.text = "0";
            waitRestart = false;
        }
    }

    void SpawnPoints()
    {
        Vector3 position;

        while (true)
        {
            int x = Random.Range(minX, maxX);
            int y = Random.Range(minY, maxY);
            position = grid.GetCellCenterWorld(new Vector3Int(x, y, 0));

            //
            if (snake.CheckPoint(position))
                break;
            
            Debug.Log("Refind");
        }

        pointPrefab.transform.position = position;
        pointPrefab.gameObject.SetActive(true);
    }

    public void PlayerTouchPoint()
    {
        score += 1;
        scoreText.text = score.ToString();
        SpawnPoints();
    }

    public void GameOver()
    {
        waitRestart = true;
    }
}
