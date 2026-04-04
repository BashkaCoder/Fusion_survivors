using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class MenuState : IState
    {
        public void Enter()
        {
            Debug.Log($"Enter {nameof(MenuState)}");
        }

        public void Exit()
        {
            Debug.Log($"Exit {nameof(MenuState)}");
        }
    }
}