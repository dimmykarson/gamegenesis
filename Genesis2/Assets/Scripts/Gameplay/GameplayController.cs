using UnityEngine;
using System.Collections;
namespace Gameplay
{
    public class GameplayController :  MonoSingleton<GameplayController>
    {
        public delegate void VoidDelegate();
        public event VoidDelegate GameStartedEvent;

        private void Start()
        {
           DispatchGameStarted();
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