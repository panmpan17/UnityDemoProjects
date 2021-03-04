using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCameraController : MonoBehaviour
{
    // private enum Direction {
    //     Left,
    //     Up,
    //     Right,
    //     Down,
    // }
    // private Direction direction = Direction.Up;
    private int direction = 0;
    // 0 is left, 1 is up, 2 is right, 3 is down

    [SerializeField]
    private float speed;

    [SerializeField]
    private float minX = -7, maxX = 7, minY = -4, maxY = 4;

    void Update()
    {
        // // position += new Vector3(0.1f, 0, 0);
        // // transform.Translate(new Vector3());

        // // State machine

        // if (direction == 0) {
        //     position.x -= speed * Time.deltaTime;
        //     if (position.x <= minX)
        //         direction = 1;
        // }
        // else if (direction == 1) {
        //     position.y += speed * Time.deltaTime;
        //     if (position.y >= maxY)
        //         direction = 2;
        // }
        // else if (direction == 2) {
        //     position.x += speed * Time.deltaTime;
        //     if (position.x >= maxX)
        //         direction = 3;
        // }
        // else if (direction == 3) {
        //     position.y -= speed * Time.deltaTime;
        //     if (position.y <= minY)
        //         direction = 0;
        // }

        // transform.position = position;
    }
}
