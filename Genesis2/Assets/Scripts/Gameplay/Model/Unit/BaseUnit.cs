using System;
using UnityEngine;
using Gameplay.Attribute;
using Gameplay.Unit.Movement;

namespace Gameplay.Unit
{
    [RequireComponent(typeof(BaseMovement))]
    public class BaseUnit : MonoBehaviour
    {
        private AttributePool attributePool;
        private BaseMovement baseMovement;
        

        public AttributePool AttributePool
        {
            get { return attributePool; }
        }


        protected virtual void Awake()
        {
            baseMovement = GetComponent<BaseMovement>();
            attributePool = GetComponentInChildren<AttributePool>();
        }

        protected virtual void Start()
        {
            attributePool.GetAttribute(AttributeType.Health).OnAttributeOver += OnHealthOver;
            baseMovement.Initialize();
        }

        public virtual void Initialize()
        {
            attributePool.GetAttribute(AttributeType.MoveSpeed).Initialize(5, 10);
            attributePool.GetAttribute(AttributeType.Health).Initialize(100, 100);
        }
        public void Initialize(int targetHealth, int targetMoveSpeed)
        {
            attributePool.GetAttribute(AttributeType.MoveSpeed).Initialize(targetMoveSpeed, targetMoveSpeed);
            attributePool.GetAttribute(AttributeType.Health).Initialize(targetHealth, targetHealth);
        }

        protected virtual void OnDestroy()
        {
            attributePool.GetAttribute(AttributeType.Health).OnAttributeOver -= OnHealthOver;
        }

        private void OnHealthOver(float prevValue, float currentValue)
        {
            Die();
        }

        protected virtual void Die()
        {

        }
        
    }
}
