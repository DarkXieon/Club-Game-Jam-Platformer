
using UnityEngine;

namespace Assets.Resources.Scripts.Input
{
    public class UserInput
    {
        public KeyCode BoundButton { get; set; }
        
        public bool IsDown { get; set; }

        public float HoldTime { get; set; }

        public UserInput(KeyCode boundButton)
        {
            BoundButton = boundButton;
        }
    }
}
