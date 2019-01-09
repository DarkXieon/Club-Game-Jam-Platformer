using System;
using System.Linq;

using UnityEngine;

public class StickToWalls : MonoBehaviour
{
    public KeyCode StickButton = KeyCode.E;
    public LayerMask Mask;
    public float Offset;
    public float Radius;

    private Rigidbody2D body;
    private Action<Rigidbody2D> unstick;
    private bool stopped;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Collider2D[] hitsRight = new Collider2D[10];
        int countRight = Physics2D.OverlapCircle(new Vector2(transform.position.x + Offset * transform.localScale.x, transform.position.y), Radius * transform.localScale.x, new ContactFilter2D() { useLayerMask = true, layerMask = Mask }, hitsRight);

        hitsRight = hitsRight
            .Where(collider => collider != null && collider.gameObject != gameObject && collider.gameObject.GetComponent<StickyWall>() != null)
            .Distinct()
            .ToArray();
        countRight = hitsRight.Length;

        Collider2D[] hitsLeft = new Collider2D[10];
        int countLeft = Physics2D.OverlapCircle(new Vector2(transform.position.x - Offset * transform.localScale.x, transform.position.y), Radius * transform.localScale.x, new ContactFilter2D() { useLayerMask = true, layerMask = Mask }, hitsLeft);

        hitsLeft = hitsLeft
            .Where(collider => collider != null && collider.gameObject != gameObject && collider.gameObject.GetComponent<StickyWall>() != null)
            .Distinct()
            .ToArray();
        countLeft = hitsLeft.Length;

        if (unstick != null && Input.GetKeyDown(StickButton))
        {
            unstick.Invoke(body);
        }
        else if ((countRight > 0 || countLeft > 0) && unstick == null && (Input.GetKey(StickButton) || Input.GetKeyDown(StickButton)))
        {
            Collider2D hitCollider = hitsLeft.Concat(hitsRight).First();

            unstick = hitCollider.GetComponent<StickyWall>().StickPlayer(body, hitCollider);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(unstick != null && GetComponent<Die>().IsDead)
        {
            StickyWall stickyWall = transform.parent.GetComponent<StickyWall>();

            if(stickyWall.StickOnDeath && !stickyWall.IgnorePhysicsOnDeath)
            {
                unstick.Invoke(body);

                unstick = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(new Vector3(transform.position.x + Offset * transform.localScale.x, transform.position.y, 0), Radius * transform.localScale.x);
        Gizmos.DrawSphere(new Vector3(transform.position.x - Offset * transform.localScale.x, transform.position.y, 0), Radius * transform.localScale.x);
    }

    public void UnStick()
    {
        unstick = null;
    }
}
