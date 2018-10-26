using UnityEngine;

public class Activator : MonoBehaviour
{
    public Activatable ToActivate;
    public bool CanActivateMultipleTimes;

    private bool activated;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!activated || CanActivateMultipleTimes)
        {
            ToActivate.OnActivate.Invoke();

            activated = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!activated || CanActivateMultipleTimes)
        {
            ToActivate.OnActivate.Invoke();

            activated = true;
        }
    }
}
