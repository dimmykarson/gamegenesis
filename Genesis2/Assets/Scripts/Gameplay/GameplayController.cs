using UnityEngine;
using System.Collections;
namespace Gameplay
{
    public class GameplayController :  MonoSingleton<GameplayController>
    {
        public delegate void VoidDelegate();
        public event VoidDelegate GameStartedEvent;
        public delegate void OnPlayerSpawnedDelegate(PlayerUnit player);
        public event OnPlayerSpawnedDelegate OnPlayerSpawnEvent;

        [SerializeField]
        private GameObject playerPrefab;

        private void Awake()
        {
            SpawnPlayer();
            DispatchGameStarted();
        }

        private void SpawnPlayer()
        {
            PlayerUnit playerClone = Instantiate(playerPrefab).GetComponent<PlayerUnit>();
            playerClone.transform.SetParent(this.transform);
            playerClone.Initialize();    
            DispatchOnPlayerSpawnEvent(playerClone);
        }

        private void DispatchOnPlayerSpawnEvent(PlayerUnit targetPlayer)
        {
            if (OnPlayerSpawnEvent != null)
                OnPlayerSpawnEvent(targetPlayer);
        }

        private void DispatchGameStarted()
        {
            if (GameStartedEvent != null)
                GameStartedEvent();

        }

        public Vector3 GetRandomPosition()
        {
            return new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
        }
    }
}