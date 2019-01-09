using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorManager : Activatable
{
    public float Speed = 1f;
    public Direction Direction = Direction.Left;

    private const string redArrowPath = "Sprites/Red Arrow";
    private const string blueArrowPath = "Sprites/Blue Arrow";

    private ConveyorMove[] childMovers;
    //private GameObject rendererContainer;
    private SpriteRenderer arrowRenderer;
    private Sprite redArrow;
    private Sprite blueArrow;

    private void OnEnable()
    {
        childMovers = transform.GetComponentsInChildren<ConveyorMove>();
        //rendererContainer = new GameObject("Arrow Renderer", typeof(SpriteRenderer));
        arrowRenderer = (new GameObject("Arrow Renderer", typeof(SpriteRenderer))).GetComponent<SpriteRenderer>();
        arrowRenderer.transform.parent = transform;
        arrowRenderer.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        arrowRenderer.transform.localScale = new Vector3(20f, 50f, 1f);
        redArrow = Resources.Load<Sprite>(redArrowPath);
        blueArrow = Resources.Load<Sprite>(blueArrowPath);
        UpdateChildren();
        UpdateArrowDisplay();
    }

    private void OnDisable()
    {
        arrowRenderer.enabled = false;
    }

    public override void OnActivate()
    {
        ChangeDirection();

        if(enabled)
        {
            UpdateChildren();

            UpdateArrowDisplay();
        }
    }

    private void ChangeDirection()
    {
        Direction = (Direction)(-(int)Direction);
    }

    private void UpdateChildren()
    {
        foreach (ConveyorMove mover in childMovers)
        {
            mover.Direction = Direction;
            mover.Speed = Speed;
        }
    }

    private void UpdateArrowDisplay()
    {
        arrowRenderer.sprite = Direction == Direction.Left
            ? redArrow
            : blueArrow;

        if(Direction == Direction.Left)
        {
            arrowRenderer.sprite = redArrow;
            arrowRenderer.flipX = true;
        }
        else
        {
            arrowRenderer.sprite = blueArrow;
            arrowRenderer.flipX = false;
        }
    }
}
