using UnityEngine;
using System.Collections;
using Gameplay.Unit;
using Random = UnityEngine.Random;

namespace Gameplay.Difficulty
{
    public class DifficultyController : MonoBehaviour
    {
        [SerializeField]
        private DifficultyBalance[] balance;

        private EnemySpawner[] spawners;
        private GameplayController gameplayController;

        private int currentLevel = 0;

        private Coroutine routine;
        private DifficultyBalance currentBalance;
        private int enemiesDeadCount = 0;
        private int enemiesSpawnedCount = 0;


        private void Awake()
        {
            gameplayController = GameplayController.Instance;
            gameplayController.GameStartedEvent += OnGameStarted;
            spawners = GetComponentsInChildren<EnemySpawner>();
        }

        private void OnDestroy()
        {
            gameplayController.GameStartedEvent -= OnGameStarted;
        }

        private void OnGameStarted()
        {
            currentLevel = 0;
            currentBalance = balance[currentLevel];
            enemiesDeadCount = 0;
            enemiesSpawnedCount = 0;
            if (routine != null)
                StopCoroutine(routine);

            routine = StartCoroutine(GameFlowCoroutine());
        }

        private IEnumerator GameFlowCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(currentBalance.minimumSpawnInterval, currentBalance.maximumSpawnInterval));
                if (enemiesSpawnedCount < currentBalance.totalEnemies)
                {
                    EnemySpawner randomEnemySpawner = GetRandomEnemySpawner();
                    EnemyUnit enemy = randomEnemySpawner.Spawn(currentBalance.health, currentBalance.moveSpeed);
                    // enemy.EnemyDieEvent += OnEnemyDie;
                    enemiesSpawnedCount += 1;
                }
            }
        }

        private void CheckLevel()
        {
            if (enemiesDeadCount >= currentBalance.totalEnemies)
            {
                enemiesDeadCount = 0;
                enemiesSpawnedCount = 0;
                currentLevel += 1;

                int targetBalance = currentLevel;
                if (targetBalance > balance.Length)
                    targetBalance = balance.Length;

                currentBalance = balance[targetBalance];
            }
        }

        private void OnEnemyDie(EnemyUnit enemy)
        {
            // enemy.EnemyDieEvent -= OnEnemyDie;
            enemiesDeadCount += 1;
            CheckLevel();
        }

        private EnemySpawner GetRandomEnemySpawner()
        {
            return spawners[Random.Range(0, spawners.Length)];
        }
    }
}