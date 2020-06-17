﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Activator : MonoBehaviour
{
    private const string activateButton = "Activate";
    //public KeyCode ActivationKey = KeyCode.E;
    
    private Die die;
    private List<Collider2D> insideOf;

    private void Start()
    {
        die = GetComponent<Die>();
        insideOf = new List<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Die die = GetComponent<Die>();
        
        Debug.Assert(collision.tag != "Player");
        Debug.Assert(die != null);

        IEnumerable<Activatable> activations = collision.gameObject.GetComponents<Activatable>();
        
        foreach (Activatable toActivate in activations)
        {
            if (Activatable.CanActivate(toActivate, die.IsDead, IsPressingActivate()))
            {
                Activatable.Activate(toActivate);
            }

            insideOf.Add(toActivate.GetComponents<Collider2D>().Single(collider => collider.isTrigger));
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        Die die = GetComponent<Die>();

        Debug.Assert(collision.tag != "Player");
        Debug.Assert(die != null);

        IEnumerable<Activatable> activations = collision.gameObject.GetComponents<Activatable>();

        foreach (Activatable toActivate in activations)
        {
            if (toActivate.Activated && Activatable.CanDeactivate(toActivate))
            {
                Activatable.Deactivate(toActivate);
            }


            insideOf.Remove(toActivate.GetComponents<Collider2D>().Single(collider => collider.isTrigger));
        }
    }
    
    private void TriggerStay(Collider2D collision)
    {
        IEnumerable<Activatable> activations = collision.gameObject.GetComponents<Activatable>();

        foreach (Activatable toActivate in activations)
        {
            if (Activatable.CanActivate(toActivate, die.IsDead, IsPressingActivate()))
            {
                Activatable.Activate(toActivate);
            }
        }
    }

    private void Update()
    {
        foreach(Collider2D collider in insideOf)
        {
            TriggerStay(collider);
        }
    }

    private bool IsPressingActivate()
    {
        return Input.GetButtonDown(activateButton);
    }
}