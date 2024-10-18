using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace UltraEvents.Utils
{
    // Token: 0x02000005 RID: 5
    internal static class ModUtils
    {
        // Token: 0x06000066 RID: 102 RVA: 0x000059EC File Offset: 0x00003BEC
        public static NewMovement GetPlayerTransform()
        {
            return MonoSingleton<NewMovement>.Instance;
        }
        public static List<EnemyIdentifier> GetEveryEnemy()
        {
            List<EnemyIdentifier> enemies = GameObject.FindObjectsOfType<EnemyIdentifier>().ToList();
            return enemies != null ? enemies : new List<EnemyIdentifier>(); // Return an empty list if null
        }

        public static List<EnemyIdentifier> GetEveryEnemyThatAreAlive()
        {
            List<EnemyIdentifier> enemies = GetEveryEnemy();
            if (enemies == null || enemies.Count == 0) return new List<EnemyIdentifier>(); // Safety check for null or empty list
            enemies.RemoveAll((x) => x == null || x.dead); // Additional null check for each enemy
            return enemies;
        }

        public static EnemyIdentifier getRandomEnemy()
        {
            List<EnemyIdentifier> enemies = GetEveryEnemy();
            if (enemies == null || enemies.Count == 0) return null; // Check if the list is null or empty
            return enemies[Random.Range(0, enemies.Count)];
        }

        public static EnemyIdentifier getRandomEnemyThatIsAlive()
        {
            List<EnemyIdentifier> enemies = GetEveryEnemyThatAreAlive();
            if (enemies == null || enemies.Count == 0) return null; // Check if the list is null or empty
            return enemies[Random.Range(0, enemies.Count)];
        }
        public static Vector3 GetRandomNavMeshPoint(Vector3 origin, float radius)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius; // Get random direction within the sphere
            randomDirection += origin; // Offset by the origin position

            NavMeshHit hit;
            // Sample the NavMesh at the random position within the radius
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
            {
                return hit.position; // Return the position on the NavMesh
            }

            return origin; // Return the origin if no valid point was found
        }
        public static List<T> GetEverythingOfType<T>(System.Predicate<T> matchThing = null) where T : UnityEngine.Object
        {
            List<T> values = Resources.FindObjectsOfTypeAll<T>().ToList();
            if (matchThing != null)
            {
                values.RemoveAll(matchThing);
            }
            return values;
        }

        // Token: 0x06000067 RID: 103 RVA: 0x00005A08 File Offset: 0x00003C08
        public static void AttachWeapon(int tempSlot, string pPref, GameObject weapon, GunSetter gs)
        {
            bool flag = false;
            bool flag2 = pPref != "";
            if (flag2)
            {
                flag = (GameProgressSaver.CheckGear(pPref) != 0 && MonoSingleton<PrefsManager>.Instance.GetInt("weapon." + pPref, 0) != 0);
                bool flag3 = !flag;
                if (flag3)
                {
                    MonoSingleton<PrefsManager>.Instance.SetInt("weapon." + pPref, 1);
                    bool flag4 = !SceneHelper.IsPlayingCustom;
                    if (flag4)
                    {
                        GameProgressSaver.AddGear(pPref);
                    }
                }
            }
            MonoSingleton<GunControl>.Instance.noWeapons = false;
            bool flag5 = gs != null;
            if (flag5)
            {
                gs.enabled = true;
                gs.ResetWeapons(false);
            }
            bool flag6 = !flag;
            if (flag6)
            {
                for (int i = 0; i < MonoSingleton<GunControl>.Instance.slots[tempSlot].Count; i++)
                {
                    bool flag7 = MonoSingleton<GunControl>.Instance.slots[tempSlot][i].name == weapon.name + "(Clone)";
                    if (flag7)
                    {
                        flag = true;
                    }
                }
            }
            bool flag8 = !flag;
            if (flag8)
            {
                GameObject item = Object.Instantiate<GameObject>(weapon, MonoSingleton<GunControl>.Instance.transform);
                MonoSingleton<GunControl>.Instance.slots[tempSlot].Add(item);
                MonoSingleton<GunControl>.Instance.ForceWeapon(weapon, true);
                MonoSingleton<GunControl>.Instance.noWeapons = false;
                MonoSingleton<GunControl>.Instance.UpdateWeaponList(false);
            }
            else
            {
                bool isPlayingCustom = SceneHelper.IsPlayingCustom;
                if (isPlayingCustom)
                {
                    for (int j = 0; j < MonoSingleton<GunControl>.Instance.slots[tempSlot].Count; j++)
                    {
                        bool flag9 = MonoSingleton<GunControl>.Instance.slots[tempSlot][j].name == weapon.name + "(Clone)";
                        if (flag9)
                        {
                            MonoSingleton<GunControl>.Instance.ForceWeapon(weapon, true);
                            MonoSingleton<GunControl>.Instance.noWeapons = false;
                            MonoSingleton<GunControl>.Instance.UpdateWeaponList(false);
                        }
                    }
                }
            }
        }
    }
}
