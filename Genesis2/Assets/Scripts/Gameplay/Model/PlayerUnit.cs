using UnityEngine;
using System.Collections;
using Gameplay.Unit;

namespace Gameplay
{
    public class PlayerUnit : BaseUnit
    {
        private Animator animator;
        private int anim = 0 ;

        public int Anim
        {
            get
            {
                return anim;
            }

            set
            {
                anim = value;
                animator.SetInteger("opcao", anim);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
            
        }

        public void Update()
        {
            
        }
    }
}
