using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class KillPlayerOnContact : MonoBehaviour
{
    public bool AttachPlayerOnDeath;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Die player = collision.gameObject.GetComponent<Die>();

        if(player != null && !player.IsDead)
        {
            player.Kill();

            if (AttachPlayerOnDeath)
            {
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                player.transform.parent = transform;
                player.transform.localEulerAngles = new Vector3(player.transform.localEulerAngles.x, player.transform.localEulerAngles.y, player.transform.localEulerAngles.z - 90);
            }
        }
    }
}
