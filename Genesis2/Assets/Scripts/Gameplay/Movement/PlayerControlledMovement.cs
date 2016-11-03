using UnityEngine;

namespace Gameplay.Unit.Movement
{
    [RequireComponent(typeof(BaseUnit))]
    public class PlayerControlledMovement : BaseMovement
    {
        [SerializeField]
        private LayerMask groundLayer;

        private Vector3 playerInput = Vector3.zero;
        private Quaternion mouseRotation = Quaternion.identity;
        private void CheckInput()
        {
            playerInput = Vector3.forward*Input.GetAxis("Vertical") + Vector3.right*Input.GetAxis("Horizontal");

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(mouseRay, out hit, 100, groundLayer.value))
            {
                Vector3 diff = hit.point - transform.position;
                diff.y = 0;

                mouseRotation = Quaternion.LookRotation(diff);
            }
        }

        private void FixedUpdate()
        {
            CheckInput();
            Move();
            Turn();
        }

        private void Turn()
        {
            RigidBody.MoveRotation(mouseRotation);
        }

        private void Move()
        {
            Vector3 finalSpeed = playerInput*moveSpeedValue*Time.fixedDeltaTime;
            NavMeshAgent.Move(finalSpeed);
        }
    }
}

