using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public KeyCode DeathKey;
    public bool IsDead => isDead;

    private GameObject prefab;
    private Vector3 spawnAt;
    private Rigidbody2D body;
    private Move move;
    private Jump jump;
    private StickToWalls stick;
    private AnimationVelocityTracker anitmationController;
    private BodyCountLimiter limiter;
    private bool isDead;

    private void Start()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Player Stuff/Player");
        spawnAt = transform.position;
        body = GetComponent<Rigidbody2D>();
        move = GetComponent<Move>();
        jump = GetComponent<Jump>();
        stick = GetComponent<StickToWalls>();
        anitmationController = GetComponent<AnimationVelocityTracker>();
        limiter = FindObjectOfType<BodyCountLimiter>();

        if (limiter == null)
            Debug.LogWarning("You have no body count limter in this scene. The number of bodies is infinate");

        //these are for new players that are spawned
        body.gravityScale = 1f;
        anitmationController.ForceIdle(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(DeathKey))
        {
            Kill();
        }
    }

    public void Kill()
    {
        GameObject newbody = Instantiate(prefab, spawnAt, Quaternion.identity);

        if (limiter != null)
            limiter.AddBody(newbody);

        anitmationController.Die();
        isDead = true;
        
        move.enabled = false;
        jump.enabled = false;
        stick.enabled = false;
        enabled = false;
    }
}
