using UnityEngine;

public class ConveyerLever : Activatable
{
    public Sprite OnSprite;
    public Sprite OffSprite;
    public float PressCooldown = .1f;

    public bool IsOn => on;

    private ConveyorManager[] managers;
    private SpriteRenderer spriteRenderer;
    private bool on;
    private float cooldownRemaining;
    
    private void Start()
    {
        managers = FindObjectsOfType<ConveyorManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (cooldownRemaining > 0f)
            cooldownRemaining = Mathf.Max(cooldownRemaining - Time.deltaTime, 0f);
    }

    public override void OnActivate()
    {
        if(cooldownRemaining <= 0)
        {
            on = !on;
            cooldownRemaining = PressCooldown;
            UpdateSprite();
        }
        
        //UpdateSprite();

        //foreach(ConveyorManager manager in managers)
        //{
        //    manager.OnActivate();
        //}
    }

    private void UpdateSprite()
    {
        spriteRenderer.sprite = on
            ? OnSprite
            : OffSprite;
    }
}