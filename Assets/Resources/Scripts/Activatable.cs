using UnityEngine;

public abstract class Activatable : MonoBehaviour
{
    public bool Activated { get; set; }
    
    public bool CanActivateMultipleTimes;
    public bool DestroyAfterUse;
    public bool CanActivateWhileDead;
    public bool RequiresUserInput;
    public bool CanToggle;

    protected bool activated;

    public abstract void OnActivate();
    
    public virtual void OnDeactivate() { }

    public static bool CanActivate(Activatable activatable, bool isDead, bool isKeyDown)
    {
        return (!activatable.Activated || activatable.CanActivateMultipleTimes) && (!isDead || activatable.CanActivateWhileDead) && (!activatable.RequiresUserInput || isKeyDown);
    }

    public static bool CanDeactivate(Activatable activatable)
    {
        return activatable.Activated && activatable.CanToggle;
    }

    public static void Activate(Activatable activatable)
    {
        activatable.OnActivate();
        activatable.Activated = true;

        if (activatable.DestroyAfterUse)
        {
            Destroy(activatable);
        }
    }

    public static void Deactivate(Activatable activatable)
    {
        activatable.OnDeactivate();
        activatable.Activated = false;
    }
}
