using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdContoller : MonoBehaviour
{
    public float jumpVelocity = 3;

    public float upwardAngularSpeed = 360;
    public float upwardStopAngle;
    private bool upward;
    public float downwardAngularSpeed = -180;

    private new Rigidbody2D rigidbody2D;
    private Vector3 initialPosition;


    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.simulated = false;

        initialPosition = transform.position;
    }

    private void OnEnable() {
        transform.position = initialPosition;
        rigidbody2D.simulated = true;
        rigidbody2D.velocity = new Vector2(0, jumpVelocity);
        rigidbody2D.angularVelocity = downwardAngularSpeed;
        transform.rotation = Quaternion.Euler(0, 0, upwardStopAngle);
    }

    void Update()
    {
        if (upward)
        {
            if (transform.rotation.eulerAngles.z >= upwardStopAngle)
            {
                upward = false;
                rigidbody2D.angularVelocity = downwardAngularSpeed;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Debug.Log("Michael");
            if (rigidbody2D.simulated)
            {
                rigidbody2D.velocity = new Vector2(0, jumpVelocity);

                rigidbody2D.angularVelocity = upwardAngularSpeed;
                upward = true;
            }
            else
            {
                transform.position = initialPosition;

                rigidbody2D.simulated = true;
                rigidbody2D.velocity = new Vector2(0, jumpVelocity);

                transform.rotation = Quaternion.Euler(0, 0, 0);
                rigidbody2D.angularVelocity = upwardAngularSpeed;
                upward = true;
            }
        }
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     rigidbody2D.velocity = new Vector2(0, jumpVelocity);

            // transform.rotation = Quaternion.Euler(0, 0, startDirection);
        // }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        rigidbody2D.simulated = false;
        GameControl.ins.GameOver();
        enabled = false;
    }
}
