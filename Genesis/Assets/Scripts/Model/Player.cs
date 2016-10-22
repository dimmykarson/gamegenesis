using UnityEngine;
using System.Collections;
using System;

namespace Model
{
    [RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent))]
    public class Player : MonoBehaviour
    {
        private NavMeshAgent nma;
        private string nome;
        private int hp;
        private int ataque;
        private int defesa;
        [SerializeField]
        private int velocidade = 1;
        private Vector3 playerInput = Vector3.zero;

        [SerializeField]
        private LayerMask groundLayer;
        private Quaternion mouseRotation = Quaternion.identity;

        private Rigidbody rb;

        // Use this for initialization
        void Start()
        {
            nma = GetComponent<NavMeshAgent>();
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            ChecarClique();
            Turn();
            Mover();
        }

        private void Mover()
        {
            Vector3 finalSpeed = playerInput * velocidade * Time.fixedDeltaTime;
            nma.Move(finalSpeed);
        }

        private void Turn()
        {
            rb.MoveRotation(mouseRotation);
        }

        private void ChecarClique()
        {
            playerInput = Vector3.forward * Input.GetAxis("Vertical") + Vector3.right * Input.GetAxis("Horizontal");

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(mouseRay, out hit, 100, groundLayer.value))
            {
                Vector3 diff = hit.point - transform.position;
                diff.y = 0;

                mouseRotation = Quaternion.LookRotation(diff);
            }
        }

        public string Nome
        {
            get
            {
                return nome;
            }

            set
            {
                nome = value;
            }
        }

        public int Hp
        {
            get
            {
                return hp;
            }

            set
            {
                hp = value;
            }
        }

        public int Ataque
        {
            get
            {
                return ataque;
            }

            set
            {
                ataque = value;
            }
        }

        public int Defesa
        {
            get
            {
                return defesa;
            }

            set
            {
                defesa = value;
            }
        }

        public int Velocidade
        {
            get
            {
                return velocidade;
            }

            set
            {
                velocidade = value;
            }
        }
    }

}

