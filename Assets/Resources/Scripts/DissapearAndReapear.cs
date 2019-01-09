using System.Collections;
using System.Linq;
using UnityEngine;

public class DissapearAndReapear : Activatable
{
    public float DissapearTime;
    public Vector2 Offset;
    
    public override void OnActivate()
    {
        StartCoroutine(RelocateCoroutine());
    }

    private IEnumerator RelocateCoroutine()
    {
        SpriteRenderer[] spriteRenderers = transform.GetComponentsInChildren<SpriteRenderer>();
        spriteRenderers.ToList().ForEach(renderer => renderer.enabled = false);
        yield return new WaitForSeconds(DissapearTime);

        transform.position = (Vector2)transform.position + Offset;
        spriteRenderers.ToList().ForEach(renderer => renderer.enabled = true);
    }
}
