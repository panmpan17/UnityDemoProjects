using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdContoller : MonoBehaviour
{
    [SerializeField]
    private float jumpVelocity;
    [SerializeField]
    private float startDirection;
    [SerializeField]
    private float angularVelocity;

    private Vector3 initialPosition;
    private new Rigidbody2D rigidbody2D;

    private void Awake() {
        initialPosition = transform.position;
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.simulated = false;
    }

    private void OnEnable() {
        transform.position = initialPosition;
        rigidbody2D.simulated = true;
        rigidbody2D.velocity = new Vector2(0, jumpVelocity);
        rigidbody2D.angularVelocity = angularVelocity;
        transform.rotation = Quaternion.Euler(0, 0, startDirection);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.rotation = Quaternion.Euler(0, 0, startDirection);
            rigidbody2D.velocity = new Vector2(0, jumpVelocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        enabled = false;
        GameControl.ins.GameOver();
    }
}
