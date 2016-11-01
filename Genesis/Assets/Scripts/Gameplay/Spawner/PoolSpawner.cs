using UnityEngine;
using System.Collections;
using Model;
using System;

public class PoolSpawner : MonoBehaviour {

    private EnemySpawner[] spawners;
    private Coroutine routine;

   

    private void Awake()
    {
        spawners = GetComponentsInChildren<EnemySpawner>();
    }

    private void Start()
    {
     
            if (routine != null)
                StopCoroutine(routine);

            routine = StartCoroutine(GameFlowCoroutine());
    }

    private IEnumerator GameFlowCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(1, 1));
            if (GameplayController.Instance.QtEnemySpwanned < GameplayController.Instance.QtMaxEnemys)
            {

                EnemySpawner randomEnemySpawner = GetRandomEnemySpawner();
                Enemy enemy = randomEnemySpawner.Spawn(UnityEngine.Random.Range(10, 100), UnityEngine.Random.Range(5, 9));
                Debug.Log("Spwanando de " + randomEnemySpawner);
                Debug.Log(randomEnemySpawner.transform.position);
                enemy.transform.SetParent(this.transform);
                enemy.transform.localPosition = Vector3.zero;
                enemy.transform.localEulerAngles = Vector3.zero;
                GameplayController.Instance.addQtEnemySpwanned();
            }
            else
            {
                Debug.Log("Quantidade máxima de Enemys atingida");
            }
            
    
        }
    }
     

    private EnemySpawner GetRandomEnemySpawner()
    {
        return spawners[UnityEngine.Random.Range(0, spawners.Length)];
    }
    
    void Update () {
	
	}
    public EnemySpawner[] Spawners
    {
        get
        {
            return spawners;
        }

        set
        {
            spawners = value;
        }
    }
}
