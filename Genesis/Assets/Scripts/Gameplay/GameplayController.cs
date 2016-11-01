using UnityEngine;
using System.Collections;
using System;

public class GameplayController : MonoSingleton<GameplayController>
{
    [SerializeField]
    private PlayerSpawner playerSpawner;
    [SerializeField]
    private PoolSpawner enemyPoolSpawners;
    private int qtMaxEnemys = 20;
    private int qtEnemySpwanned = 0;

   

    public void addQtEnemySpwanned()
    {
        qtEnemySpwanned++;
    }

    private void Start()
    {
        PlayerSpawn();

        EnemySpawn();

    }

    private void EnemySpawn()
    {
        enemyPoolSpawners  = Instantiate(enemyPoolSpawners).GetComponent<PoolSpawner>();
        enemyPoolSpawners.transform.SetParent(this.transform);
        enemyPoolSpawners.transform.localPosition = GetRandomPosition();
        enemyPoolSpawners.transform.localEulerAngles = Vector3.zero;

        int size = enemyPoolSpawners.Spawners.Length;
        for(int i = 0; i < size; i++)
        {
            Vector3 p = GetRandomPosition();
            Debug.Log("Enemy spawnado!" + p);
            enemyPoolSpawners.Spawners[i].initialize(p);
        }
    }

    private void PlayerSpawn()
    {
        playerSpawner = Instantiate(playerSpawner).GetComponent<PlayerSpawner>();
        playerSpawner.transform.SetParent(this.transform);
        playerSpawner.transform.localPosition = Vector3.zero;
        playerSpawner.transform.localEulerAngles = Vector3.zero;
    }

    public static Vector3 GetRandomPosition()
    {
        return new Vector3(UnityEngine.Random.Range(-50, 50), 0, UnityEngine.Random.Range(-75, 75));
    }
    public int QtMaxEnemys
    {
        get
        {
            return qtMaxEnemys;
        }

        set
        {
            qtMaxEnemys = value;
        }
    }

    public int QtEnemySpwanned
    {
        get
        {
            return qtEnemySpwanned;
        }

        set
        {
            qtEnemySpwanned = value;
        }
    }
}
