using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : Activatable
{
    public Sprite OnSprite;
    public Sprite OffSprite;

    //public float DeactivateTime;

    public bool IsOn => isOn;
    
    private SpriteRenderer spriteRenderer;
    private bool isOn;
    //private float currentDeactivateTime;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = OffSprite;
    }
    
    public override void OnActivate()
    {
        isOn = true;

        spriteRenderer.sprite = OnSprite;
    }

    public override void OnDeactivate()
    {
        isOn = false;

        spriteRenderer.sprite = OffSprite;
    }
}
