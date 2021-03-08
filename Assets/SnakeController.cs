using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField]
    private GameObject bodyPrefab;

    [SerializeField]
    private float speed;
    private float speedTimer;

    private Direction direction = Direction.None;
    private Direction newDirection = Direction.None;
    private enum Direction {
        None,
        Up,
        Down,
        Left,
        Right,
    }

    private List<Transform> bodies = new List<Transform>();
    private int waitSpawnBody = 1;
    
    private Vector3 initPosition;

    private new Rigidbody2D rigidbody;

    private void Awake() {
        initPosition = transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void ResetPlayer()
    {
        for (int i = 0; i < bodies.Count; i++)
        {
            Destroy(bodies[i].gameObject);
        }

        bodies.Clear();
        direction = Direction.None;
        newDirection = Direction.None;
        transform.position = initPosition;
        speedTimer = 0;
        waitSpawnBody = 1;
    }

    void Update()
    {
        speedTimer += Time.deltaTime;

        if (direction != Direction.Down && Input.GetKeyDown(KeyCode.W))
            newDirection = Direction.Up;
        else if (direction != Direction.Up && Input.GetKeyDown(KeyCode.S))
            newDirection = Direction.Down;
        else if (direction != Direction.Right && Input.GetKeyDown(KeyCode.A))
            newDirection = Direction.Left;
        else if (direction != Direction.Left && Input.GetKeyDown(KeyCode.D))
            newDirection = Direction.Right;

        if (speedTimer >= speed)
        {
            Vector3 headPosition;

            switch (newDirection)
            {
                case Direction.Up:
                    headPosition = transform.position + Vector3.up;
                    break;
                case Direction.Down:
                    headPosition = transform.position + Vector3.down;
                    break;
                case Direction.Right:
                    headPosition = transform.position + Vector3.right;
                    break;
                case Direction.Left:
                    headPosition = transform.position + Vector3.left;
                    break;
                default:
                    return;
            }

            direction = newDirection;

            List<Vector3> partPosition = new List<Vector3>();
            if (bodies.Count >= 1)
                partPosition.Add(transform.position);
            for (int i = 1; i < bodies.Count; i++)
                partPosition.Add(bodies[i - 1].position);

            if (waitSpawnBody > 0)
            {
                waitSpawnBody--;
                GameObject newPart = Instantiate<GameObject>(bodyPrefab);

                if (bodies.Count == 0)
                    partPosition.Add(transform.position);
                else
                    partPosition.Add(bodies[bodies.Count - 1].position);

                bodies.Add(newPart.transform);
            }

            // transform.position = headPosition;
            rigidbody.position = headPosition;
            // rigidbody.
            for (int i = 0; i < partPosition.Count; i++)
            {
                bodies[i].position = partPosition[i];
            }

            speedTimer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Points"))
        {
            waitSpawnBody ++;
            SnakeGameController.ins.PlayerTouchPoint();
        }
        else if (other.CompareTag("Wall") || other.CompareTag("Body"))
        {
            SnakeGameController.ins.GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        SnakeGameController.ins.GameOver();
        direction = Direction.None;
        newDirection = Direction.None;
        speedTimer = 0;
    }

    public bool CheckPoint(Vector3 position)
    {
        if (position == transform.position)
            return false;
        for (int i = 0; i < bodies.Count; i++)
        {
            if (position == bodies[i].position)
                return false;
        }
        return true;
    }
}
