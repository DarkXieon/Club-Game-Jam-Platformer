using Assets.Resources.Scripts.Input;

using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public string MovementAxis = "Horizontal";
    public float RunSpeed;

    private AnimationVelocityTracker _tracker;
    private Rigidbody2D _body;
    
    private void Start()
    {
        _tracker = GetComponent<AnimationVelocityTracker>();
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        int direction = 0;
        if (Input.GetAxis(MovementAxis) < 0) direction = -1;
        if (Input.GetAxis(MovementAxis) > 0) direction = 1;

        float movement = direction * RunSpeed * Time.deltaTime;
        RaycastHit2D[] hits = new RaycastHit2D[10];
        int count = _body.Cast(Vector2.right, hits, movement);

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
            _tracker.StartRunning(movement);
        }
        else
        {
            _tracker.StopRunning();
        }
    }
}