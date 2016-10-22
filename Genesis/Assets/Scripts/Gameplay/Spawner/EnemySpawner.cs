using UnityEngine;
using System.Collections;
using Model;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private Enemy enemyPrefab;



    public Enemy Spawn(int targetHealth, int targetMoveSpeed)
    {
        Enemy enemyClone = Instantiate(enemyPrefab).GetComponent<Enemy>();
        enemyClone.Hp = targetHealth;
        enemyClone.Velocidade = targetMoveSpeed;

        enemyClone.transform.SetParent(transform);
        enemyClone.transform.localPosition = Vector3.zero;
        enemyClone.transform.localEulerAngles = Vector3.zero;
        return enemyClone;
    }
}
