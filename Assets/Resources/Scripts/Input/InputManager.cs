using System.Collections.Generic;

using UnityEngine;

namespace Assets.Resources.Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        private static Dictionary<InputActions, UserInput> _inputData = new Dictionary<InputActions, UserInput>()
        {
            [InputActions.RunLeft] = new UserInput(KeyCode.A),
            [InputActions.RunRight] = new UserInput(KeyCode.D),
            [InputActions.Jump] = new UserInput(KeyCode.W),
            [InputActions.Activate] = new UserInput(KeyCode.E),
            [InputActions.Grab] = new UserInput(KeyCode.Space),
            [InputActions.Die] = new UserInput(KeyCode.Return)
        };

        public void Update()
        {
            foreach (UserInput userInput in _inputData.Values)
            {
                if (UnityEngine.Input.GetKey(userInput.BoundButton))
                {
                    if (userInput.IsDown)
                    {
                        userInput.HoldTime += Time.deltaTime;
                    }
                    else
                    {
                        userInput.IsDown = true;
                    }
                }
                else
                {
                    userInput.IsDown = false;
                    userInput.HoldTime = 0f;
                }
            }
        }

        public static UserInput GetInput(InputActions inputAction)
        {
            return _inputData[inputAction];
        }
    }
}
