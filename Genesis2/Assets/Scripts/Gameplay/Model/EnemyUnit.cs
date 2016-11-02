using UnityEngine;
using System.Collections;
using Gameplay.Unit;
using Gameplay.Unit.Movement;


namespace Gameplay
{
    public class EnemyUnit : BaseUnit
    {
        private TriggerVolume sightTriggerVolume;
        private Rigidbody rigidbody;
        private PlayerUnit currentTarget;
        private PathAgentController pathAgentController;
        private BehaviorState state = BehaviorState.Idle;
        private Coroutine pushRoutine;

        protected override void Awake()
        {
            base.Awake();
            pathAgentController = GetComponent<PathAgentController>();
            sightTriggerVolume = GetComponent<TriggerVolume>();
            rigidbody = GetComponent<Rigidbody>();

            pathAgentController.OnReachDestination += OnReachDestination;
            sightTriggerVolume.OnTriggerEnterEvent += OnSightTriggerVolumeEnter;
            sightTriggerVolume.OnTriggerExitEvent += OnSightTriggerVolumeExit;
        }

        protected override void Start()
        {
            base.Start();
            ChangeStateTo(BehaviorState.Patrolling);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            pathAgentController.OnReachDestination -= OnReachDestination;

            sightTriggerVolume.OnTriggerEnterEvent -= OnSightTriggerVolumeEnter;
            sightTriggerVolume.OnTriggerExitEvent -= OnSightTriggerVolumeExit;
        }

        private void OnSightTriggerVolumeExit(TriggerVolume volume, Collider collider)
        {
            currentTarget = null;
            ChangeStateTo(BehaviorState.Patrolling);
        }

        private void OnSightTriggerVolumeEnter(TriggerVolume volume, Collider collider)
        {
            currentTarget = collider.GetComponent<PlayerUnit>();
            ChangeStateTo(BehaviorState.SeekingTarget);
        }

        public void ChangeStateTo(BehaviorState targetState)
        {
            if (state == BehaviorState.Idle && targetState == BehaviorState.Patrolling)
            {
                SeekNewPosition();
            }
            else if (state == BehaviorState.Patrolling && targetState == BehaviorState.Attacking)
            {
                pathAgentController.Stop();
            }
            state = targetState;
        }

        private void SeekNewPosition()
        {
            Vector3 randomPosition = GameplayController.Instance.GetRandomPosition();
            pathAgentController.SetDestination(randomPosition);
        }

        private void OnReachDestination(Vector3 startPosition, Vector3 endPosition)
        {
            if (state == BehaviorState.Patrolling)
                SeekNewPosition();
        }

        private void Update()
        {
            if (state == BehaviorState.SeekingTarget)
            {
                pathAgentController.SetDestination(currentTarget.transform.position);
            }
        }


    }
}