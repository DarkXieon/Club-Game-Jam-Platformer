using System.Linq;

using UnityEngine;

public class Jump : MonoBehaviour
{
    private const string jumpButton = "Jump";

    public LayerMask Mask;
    public Vector2 JumpHeightMinMax;
    public float Offset;
    public float Radius;

    private Rigidbody2D body;
    private bool inAir;
    private float timeHeld;
    private float startTime;
    private float startHeight;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Collider2D[] hits = new Collider2D[10];
        int count = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + Offset * transform.localScale.y), Radius * transform.localScale.y, new ContactFilter2D() { useLayerMask = true, layerMask = Mask }, hits);

        hits = hits
            .Where(collider => collider != null && collider.gameObject != gameObject)
            .Distinct()
            .ToArray();
        count = hits.Length;

        inAir = count == 0;
        
        if (!inAir && (IsJumpButtonPressed() || IsJumpButtonHeld()))
        {
            float jumpVelocity = Mathf.Sqrt(-2 * JumpHeightMinMax.x * Physics2D.gravity.y);

            body.AddForce(Vector2.up * jumpVelocity * body.mass, ForceMode2D.Impulse);

            timeHeld = 0f;
            startTime = Time.time;
            startHeight = body.position.y;
            inAir = true;
        }
    }

    private void FixedUpdate()
    {
        if (inAir && IsJumpButtonHeld() && body.velocity.y > 0 && enabled)
        {
            float minJumpVelocity = Mathf.Sqrt(-2 * JumpHeightMinMax.x * Physics2D.gravity.y);
            float minJumpTime = minJumpVelocity / -Physics2D.gravity.y;

            float maxHoldTime = minJumpTime * .8f;

            if(timeHeld < maxHoldTime && Time.time < startTime + minJumpTime)
            {
                float previousTimeHeld = timeHeld;
                timeHeld = Mathf.Min(Time.fixedDeltaTime + timeHeld, maxHoldTime);

                float currentTime = Time.time - startTime;
                float currentDistance = body.position.y - startHeight;
                float additionalDistance = (JumpHeightMinMax.y - JumpHeightMinMax.x) * (timeHeld / maxHoldTime);
                
                float startingVelocity = Mathf.Sqrt(-2 * Physics2D.gravity.y * (JumpHeightMinMax.x + additionalDistance));
                float currentVelocity = Mathf.Sqrt(Mathf.Pow(startingVelocity, 2) + 2 * Physics2D.gravity.y * currentDistance);

                body.velocity = new Vector2(body.velocity.x, currentVelocity);
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + Offset * transform.localScale.y, 0), Radius * transform.localScale.y);
    }

    private bool IsJumpButtonPressed()
    {
        return Input.GetButtonDown(jumpButton);
    }

    private bool IsJumpButtonHeld()
    {
        return Input.GetButton(jumpButton);
    }
}
