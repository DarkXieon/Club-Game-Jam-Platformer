using UnityEngine;

public class DoorOpen : Activatable
{
    public float TimeToOpen;
    public Vector2 OpenDistance;

    private Vector2 startPosition;
    private float currentTime;
    private bool started;
    
    private void FixedUpdate()
    {
        if(started && currentTime <= TimeToOpen)//startTime)
        {
            float lerpPercent = currentTime / TimeToOpen;
            currentTime += Time.fixedDeltaTime;

            if(lerpPercent <= 1f)
            {
                Vector2 lerped = Vector2.Lerp(startPosition, startPosition + OpenDistance, lerpPercent);

                transform.position = new Vector3(lerped.x, lerped.y, transform.position.z);
            }
            else
            {
                started = false;
            }
        }
    }

    public override void OnActivate()
    {
        startPosition = transform.position;
        currentTime = 0f;
        //startTime = Time.time;
        started = true;
    }
}
