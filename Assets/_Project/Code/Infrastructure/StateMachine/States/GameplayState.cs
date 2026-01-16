using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class GameplayState : IState
    {
        public void Enter()
        {
            Debug.Log($"Enter {nameof(GameplayState)}");
            //throw new System.NotImplementedException();
        }

        public void Exit()
        {
            Debug.Log($"Exit {nameof(GameplayState)}");
            //throw new System.NotImplementedException();
        }
    }
}