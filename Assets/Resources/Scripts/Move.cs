using System.Linq;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float RunSpeed;
    public float WalkSpeed;
    public KeyCode ToggleWalkButton = KeyCode.CapsLock;

    private bool walking;
    private AnimationVelocityTracker tracker;
    private Rigidbody2D body;
    
    private void Start()
    {
        tracker = GetComponent<AnimationVelocityTracker>();
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(ToggleWalkButton))
            walking = !walking;

        float speed = walking
            ? WalkSpeed
            : RunSpeed;

        float movement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        RaycastHit2D[] hits = new RaycastHit2D[10];
        int count = body.Cast(Vector2.right, hits, movement);

        hits = hits
            .Where(hit => hit.collider != null && !hit.collider.isTrigger)
            .ToArray();
        count = hits.Length;

        if (movement != 0f && count == 0)
        {
            if (movement > 0f)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180);
            }

            transform.Translate(Vector2.right * Mathf.Abs(movement));
            tracker.StartRunning(movement);
        }
        else
        {
            tracker.StopRunning();
        }
    }
}