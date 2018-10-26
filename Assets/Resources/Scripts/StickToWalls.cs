using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StickToWalls : MonoBehaviour
{
    public LayerMask Mask;
    public float Offset;
    public float Radius;

    private Rigidbody2D body;
    private bool stopped;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Collider2D[] hitsRight = new Collider2D[5];
        int countRight = Physics2D.OverlapCircle(new Vector2(transform.position.x + Offset, transform.position.y), Radius, new ContactFilter2D() { useLayerMask = true, layerMask = Mask }, hitsRight);

        Collider2D[] hitsLeft = new Collider2D[5];
        int countLeft = Physics2D.OverlapCircle(new Vector2(transform.position.x - Offset, transform.position.y), Radius, new ContactFilter2D() { useLayerMask = true, layerMask = Mask }, hitsLeft);

        if (countRight > 0 || countLeft > 0)
        {
            if (!stopped)
            {
                body.velocity = Vector2.zero;
                body.gravityScale = 0;
                stopped = true;
            }
        }
        else
        {
            stopped = false;
            body.gravityScale = 1;
        }

        //if (hitRight != default(RaycastHit2D) || hitLeft != default(RaycastHit2D))
        //{
        //    if(!stopped)
        //    {
        //        body.velocity = Vector2.zero;
        //        body.gravityScale = 0;
        //        stopped = true;
        //    }
        //}
        //else
        //{
        //    stopped = false;
        //    body.gravityScale = 1;
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(new Vector3(transform.position.x + Offset, transform.position.y, 0), Radius);
        Gizmos.DrawSphere(new Vector3(transform.position.x - Offset, transform.position.y, 0), Radius);
    }
}
