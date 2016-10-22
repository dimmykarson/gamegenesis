using UnityEngine;
using System.Collections;
using Model;
using System;

public class PlayerSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject player;
    private Player jogador;
	// Use this for initialization
	void Start () {
        jogador = Instantiate(player).GetComponent<Player>();
        jogador.Nome = "Dimmy";
        Debug.Log("Player spawnado!" + jogador.Nome);
        jogador.transform.SetParent(this.transform);
        jogador.transform.localPosition = Vector3.zero;
        jogador.transform.localEulerAngles = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
