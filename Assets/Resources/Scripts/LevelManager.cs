using System.Linq;

using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public Stage[] Stages;

    private int currentStage;
    private bool finished;
    
    [System.Serializable]
    public class Stage
    {
        public PressurePad[] ToStart;
        public UnityEvent OnStart;
        public bool CanStart { get { return ToStart.All(pad => pad.IsOn); } }
    }

    private void Update()
    {
        if (Stages[currentStage].CanStart && !finished)
        {
            Stages[currentStage].OnStart.Invoke();

            if (currentStage + 1 < Stages.Length)
                currentStage++;
            else
                finished = true;
        }
    }
}