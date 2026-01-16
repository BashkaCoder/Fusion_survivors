using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class NetworkConnectState : IState
    {
        public void Enter()
        {
            Debug.Log("Enter NetworkConnectState");
            //throw new System.NotImplementedException();
        }

        public void Exit()
        {
            Debug.Log("Exit NetworkConnectState");
            //throw new System.NotImplementedException();
        }
    }
}