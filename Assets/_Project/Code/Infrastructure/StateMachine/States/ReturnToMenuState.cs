using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class ReturnToMenuState : IState
    {
        public void Enter()
        {
            Debug.Log($"Enter {nameof(ReturnToMenuState)}");
            //throw new System.NotImplementedException();
        }

        public void Exit()
        {
            Debug.Log($"Exit {nameof(ReturnToMenuState)}");
            //throw new System.NotImplementedException();
        }
    }
}