using System;

namespace Gameplay.Difficulty
{
    [Serializable]
    public struct DifficultyBalance
    {
        public float minimumSpawnInterval;
        public float maximumSpawnInterval;
        public int health;
        public int moveSpeed;
        public int totalEnemies;
    }
}