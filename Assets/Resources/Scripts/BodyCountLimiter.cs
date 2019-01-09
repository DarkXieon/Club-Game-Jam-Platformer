using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BodyCountLimiter : MonoBehaviour
{
    public int MaxBodyCount = 10;

    private Queue<GameObject> bodies;
    private ConveyorMove[] moveObjects;
    private StickyWall[] wallObjects;

    private void Start()
    {
        bodies = new Queue<GameObject>();
        moveObjects = FindObjectsOfType<ConveyorMove>();
        wallObjects = FindObjectsOfType<StickyWall>();
        
        FindObjectsOfType<GameObject>()
            .Where(obj => obj.tag == "Player")
            .ToList().ForEach(obj => bodies.Enqueue(obj));
    }

    public void AddBody(GameObject body)
    {
        bodies.Enqueue(body);

        if(bodies.Count > MaxBodyCount)
        {
            GameObject toDestroy = bodies.Dequeue();
            Rigidbody2D bodyToDestroy = toDestroy.GetComponent<Rigidbody2D>();

            moveObjects.ToList().ForEach(move => move.RemoveBody(bodyToDestroy));
            wallObjects.ToList().ForEach(wall => wall.RemovePlayer(bodyToDestroy));

            Destroy(toDestroy);
        }
    }
}
