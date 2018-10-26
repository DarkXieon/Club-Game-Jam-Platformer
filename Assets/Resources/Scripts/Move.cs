using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float MoveSpeed;

    private Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float movement = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;

        int count = body.Cast(Vector2.right, new RaycastHit2D[10], movement);

        if (movement != 0f && count == 0)
            transform.Translate(Vector2.right * movement);
    }
}
