using UnityEngine;
using System.Collections;
using Gameplay.Unit.Movement;

namespace Gameplay.Movement
{
    public class PlayerController :  IPlayer
    {
        public delegate void BaseSpeedChange(float newValue);
        public BaseSpeedChange OnBaseSpeedEvent;
        PlayerUnit player;
        float moveSpeedValue = 5f;

        public void Start()
        {
            player = GetComponentInParent<PlayerUnit>();
        }

        public override void OnStickChanged(float distance, Vector2 stickPos)
        {
            if (stickPos.x == 0 && stickPos.y == 0)
            {
                StopChar();
                return;
            }
            Vector3 playerInput = CheckStick(distance, stickPos);
            Debug.Log(playerInput);
            Move(playerInput);
            Turn();
        }

        public void StopChar()
        {
            player.Anim = 0;
            player.BaseMovement.NavMeshAgent.Stop();
            player.BaseMovement.RigidBody.velocity = Vector3.zero;
            
        }

        private Vector3 CheckStick(float speed, Vector2 stickPos)
        {
            Debug.Log("Velocidade: "+speed);
          if (speed < 0.1f)
            {
                StopChar();
                return Vector3.zero;
            }
            DispatchBaseSpeedEvent(speed);
            Vector3 playerInput = new Vector3(stickPos.x, 0, stickPos.y);
            float fSpeed = GetSpeed(speed);
            playerInput *= fSpeed;
            walkOrRun(speed);
            player.Rotation = Quaternion.LookRotation(playerInput);
            return playerInput;
        }
        private float GetSpeed(float speed)
        {
            float fSpeed = speed * moveSpeedValue;
            return fSpeed;
        }
        protected void DispatchBaseSpeedEvent(float currentValue)
        {
            if (OnBaseSpeedEvent != null)
            {
                OnBaseSpeedEvent(currentValue);
            }
        }
        public void Turn()
        {
            player.Turn();
        }

        private void Move(Vector3 playerInput)
        {
            Vector3 finalSpeed = playerInput * Time.fixedDeltaTime;
            player.BaseMovement.NavMeshAgent.Move(finalSpeed);
        }

        public void walkOrRun(float speed)
        {
            player.Anim = speed<0.2f ? 0 : speed < 0.5f ? 1 : 2;
        }
        

    }
}