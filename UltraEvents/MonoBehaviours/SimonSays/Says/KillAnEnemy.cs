using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraEvents.MonoBehaviours.SimonSays.Says
{
    internal class KillAnEnemy : SimonSaysIt
    {
        public override string task => "Kill an enemy";
        public int amountOfEnemiesAlive = 0;

        public override bool checkIfDone()
        {
            List<EnemyIdentifier> enemies = EnemyTracker.Instance.GetCurrentEnemies();
            if (enemies.Count > amountOfEnemiesAlive)
            {
                amountOfEnemiesAlive = enemies.Count;
            }
            if(enemies.Count < amountOfEnemiesAlive)
            {
                return true;
            }
            return false;
        }
    }
}
