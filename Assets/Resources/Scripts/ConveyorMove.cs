using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Right = 1,
    Left = -1
}

public class ConveyorMove : MonoBehaviour
{
    public float Speed { get; set; }
    public Direction Direction { get; set; }

    private List<Rigidbody2D> affectedObjects;
    
    private void Start()
    {
        affectedObjects = new List<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        foreach(Rigidbody2D body in affectedObjects)
        {
            body.transform.Translate(Vector3.right * (int)Direction * Speed * Time.fixedDeltaTime, Space.World);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D body = collision.gameObject.GetComponent<Rigidbody2D>();

        if (body != null && body.bodyType != RigidbodyType2D.Static && !affectedObjects.Contains(body))
        {
            affectedObjects.Add(body);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Rigidbody2D body = collision.gameObject.GetComponent<Rigidbody2D>();

        if (body != null)
        {
            affectedObjects.Remove(body);
        }
    }

    public void RemoveBody(Rigidbody2D body)
    {
        affectedObjects.Remove(body);
    }

    //public void ChangeDirection(Direction? direction = null)
    //{
    //    direction = direction == null
    //        ? (Direction)(-(int)direction)
    //        : direction.Value;
    //}
}
