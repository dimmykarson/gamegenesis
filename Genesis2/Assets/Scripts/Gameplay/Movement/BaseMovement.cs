﻿using UnityEngine;
using Gameplay.Attribute;

namespace Gameplay.Unit.Movement
{
    [RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent))]
    public class BaseMovement : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        private Rigidbody rigidBody;
        protected BaseUnit baseUnit;

        protected float moveSpeedValue;


        private Attribute.Attribute moveSpeedAttribute;

        public NavMeshAgent NavMeshAgent
        {
            get
            {
                return navMeshAgent;
            }

            set
            {
                navMeshAgent = value;
            }
        }

        public Rigidbody RigidBody
        {
            get
            {
                return rigidBody;
            }

            set
            {
                rigidBody = value;
            }
        }

        protected virtual void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            baseUnit = GetComponent<BaseUnit>();
            rigidBody = GetComponent<Rigidbody>();
        }

        public virtual void Initialize()
        {
            moveSpeedAttribute = baseUnit.AttributePool.GetAttribute(AttributeType.MoveSpeed);
            moveSpeedAttribute.OnAttributeChange += OnMoveSpeedAttributeChange;
            OnMoveSpeedAttributeChange(0, moveSpeedAttribute.CurrentValue);
        }

        private void OnDisable()
        {
            moveSpeedAttribute.OnAttributeChange -= OnMoveSpeedAttributeChange;
        }

        protected virtual void OnMoveSpeedAttributeChange(float prevValue, float currentValue)
        {
            moveSpeedValue = currentValue;
        }
    }
}
