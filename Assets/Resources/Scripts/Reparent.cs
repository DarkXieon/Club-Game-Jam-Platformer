using UnityEngine;

public class Reparent : Activatable
{
    public Transform NewParent;
    //public float TimeDelay;

    public override void OnActivate()
    {
        transform.parent = NewParent;
        //StartCoroutine(RelocateCoroutine());
    }

    //private IEnumerator RelocateCoroutine()
    //{
    //    yield return new WaitForSeconds(TimeDelay);

    //    transform.parent = NewParent;
    //}
}
