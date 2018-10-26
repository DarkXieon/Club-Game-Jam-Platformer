using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    //public GameObject Prefab;
    public KeyCode DeathKey;

    private Vector2 spawnAt;
    private Rigidbody2D body;
    private Move move;
    private Jump jump;
    private StickToWalls stick;

    private void Start()
    {
        spawnAt = transform.position;
        body = GetComponent<Rigidbody2D>();
        move = GetComponent<Move>();
        jump = GetComponent<Jump>();
        stick = GetComponent<StickToWalls>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(DeathKey))
        {
            Instantiate(gameObject, spawnAt, Quaternion.identity);

            tag = "Untagged";
            gameObject.layer = 0;
            body.bodyType = RigidbodyType2D.Static;
            move.enabled = false;
            jump.enabled = false;
            stick.enabled = false;
            enabled = false;
        }
    }
}
