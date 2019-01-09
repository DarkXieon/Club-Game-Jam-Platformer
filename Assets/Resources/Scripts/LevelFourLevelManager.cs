
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LevelFourLevelManager : MonoBehaviour
{
    public PressurePad Row2Pad;
    public PressurePad Row3Pad;
    public PressurePad Row4Pad;
    public ConveyerLever Lever;
    
    public UnityEvent OnPart2Transition;
    public UnityEvent OnPart3Transition;

    private bool wasOn;
    private bool part1Complete;
    private bool part2Complete;

    private void Update()
    {
        if(!part1Complete)
        {
            if(Row2Pad.IsOn && Row3Pad.IsOn && Lever.IsOn == !wasOn)
            {
                TransitionToPart2();

                part1Complete = true;
            }
        }
        else if(!part2Complete && Lever.IsOn == !wasOn)
        {
            if (Row2Pad.IsOn && Row3Pad.IsOn && Row4Pad.IsOn)
            {
                TransitionToPart3();

                part2Complete = true;
            }
        }

        wasOn = Lever.IsOn;
    }

    private void TransitionToPart2()
    {
        OnPart2Transition.Invoke();

        FindObjectsOfType<ConveyorManager>().ToList().ForEach(manager =>
        {
            manager.enabled = !manager.enabled;
        });

        //StartCoroutine(RelocateCoroutine());
        //foreach(Activatable toActivate in OnPart2Transition)
        //{
        //    toActivate.OnActivate();
        //}
    }

    private IEnumerator RelocateCoroutine()
    {
        yield return new WaitForSeconds(.1f);
        
        FindObjectsOfType<ConveyorManager>().ToList().ForEach(manager =>
        {
            manager.enabled = !manager.enabled;
        });
    }

    private void TransitionToPart3()
    {
        OnPart3Transition.Invoke();
        //foreach (Activatable toActivate in OnPart3Transition)
        //{
        //    toActivate.OnActivate();
        //}
    }
}
