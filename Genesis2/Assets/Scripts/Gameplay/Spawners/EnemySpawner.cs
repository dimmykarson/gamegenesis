using Gameplay.Unit;
using UnityEngine;
namespace Gameplay
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private EnemyUnit enemyPrefab;

        public EnemyUnit Spawn(int targetHealth, int targetMoveSpeed)
        {

            EnemyUnit enemyClone = SimplePool.Spawn(enemyPrefab.gameObject).GetComponent<EnemyUnit>();
            enemyClone.Initialize(targetHealth, targetMoveSpeed);
            enemyClone.transform.SetParent(transform);
            enemyClone.transform.localPosition = Vector3.zero;
            enemyClone.transform.localEulerAngles = Vector3.zero;
            return enemyClone;
        }
    }
}

