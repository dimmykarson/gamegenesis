using UnityEngine;
using System.Collections;
using Gameplay.Unit;

namespace Model
{
    [RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent), typeof(TriggerVolume))]
    public class Enemy : MonoBehaviour
    {

        
        private TriggerVolume sightTriggerVolume;


        private NavMeshAgent nma;
        private string nome;
        private int hp;
        private int ataque;
        private int defesa;
        [SerializeField]
        private int velocidade = 1;
        private Vector3 playerInput = Vector3.zero;
        
        private Rigidbody rb;
        [SerializeField]
        private LayerMask groundLayer;
        private Coroutine checkDestination;
        private bool reachDestination = false;
        private Vector3 destinationPosition;
        private bool alcancou = false;
        private Player currentTarget;
        private BehaviorState state = BehaviorState.Idle;
        private SphereCollider colider;
        protected void Start()
        {
            sightTriggerVolume = GetComponent<TriggerVolume>();
            nma = GetComponent<NavMeshAgent>();
            rb = GetComponent<Rigidbody>();
            colider = GetComponent<SphereCollider>();

            sightTriggerVolume.OnTriggerEnterEvent += OnSightTriggerVolumeEnter;
            sightTriggerVolume.OnTriggerExitEvent += OnSightTriggerVolumeExit;


            BuscarDestino(GetRandomPosition());
        }

        protected void OnDestroy()
        {

            sightTriggerVolume.OnTriggerEnterEvent -= OnSightTriggerVolumeEnter;
            sightTriggerVolume.OnTriggerExitEvent -= OnSightTriggerVolumeExit;
        }

        private void OnSightTriggerVolumeExit(TriggerVolume volume, Collider collider)
        {
            currentTarget = null;
            this.colider.radius = 0.2f;
            ChangeStateTo(BehaviorState.Patrolling);
        }

        private void OnSightTriggerVolumeEnter(TriggerVolume volume, Collider collider)
        {
            currentTarget = collider.GetComponent<Player>();
            this.colider.radius = 3;
            ChangeStateTo(BehaviorState.SeekingTarget);
        }


        private void BuscarDestino(Vector3 vector3)
        {
            alcancou = false;
            nma.SetDestination(vector3);
            if (checkDestination != null)
            {
                StopCoroutine(checkDestination);
                checkDestination = null;
            }
            checkDestination = StartCoroutine(AlcancarDestino());

        }

        public void ChangeStateTo(BehaviorState targetState)
        {
            if (state == BehaviorState.Idle && targetState == BehaviorState.Patrolling)
            {
                BuscarDestino(GetRandomPosition());
            }
            else if (state == BehaviorState.Patrolling && targetState == BehaviorState.Attacking)
            {
                nma.Stop();
            }
            state = targetState;
        }

        private IEnumerator AlcancarDestino()
        {
            while (!alcancou)
            {
                yield return new WaitForSeconds(0.1f);
                if (!nma.pathPending)
                {
                    if (nma.remainingDistance <= nma.stoppingDistance)
                    {
                        if (nma.pathStatus == NavMeshPathStatus.PathComplete)
                        {
                            if (!nma.hasPath || Mathf.Abs(nma.velocity.sqrMagnitude) < float.Epsilon)
                            {
                                alcancou = true;
                                BuscarDestino(GetRandomPosition());
                            }
                        }
                    }
                }
               
            }
        }

        private static Vector3 GetRandomPosition()
        {
            return new Vector3(UnityEngine.Random.Range(-50, 50), 0, UnityEngine.Random.Range(-75, 75));
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

        // Update is called once per frame
        void Update()
        {
            if (state == BehaviorState.SeekingTarget)
            {
                BuscarDestino(currentTarget.transform.position);
            }
        }

        
    }

}
