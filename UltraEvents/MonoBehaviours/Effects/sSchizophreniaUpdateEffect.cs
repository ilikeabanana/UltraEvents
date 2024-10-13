using System.Collections.Generic;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    internal class sSchizophreniaUpdateEffect : Effect
    {
        private HashSet<Renderer> alreadyDoneRenderers = new HashSet<Renderer>();
        private List<RemoveOnUnseen> activeRemoveComponents = new List<RemoveOnUnseen>();

        private List<EnemyIdentifier> enemyCache = new List<EnemyIdentifier>();
        private List<GoreSplatter> goreCache = new List<GoreSplatter>();
        private List<ExplosionController> explosionCache = new List<ExplosionController>();

        private void Start()
        {
            CacheObjects();
        }

        private void CacheObjects()
        {
            // Cache all objects, both active and inactive
            enemyCache.AddRange(Resources.FindObjectsOfTypeAll<EnemyIdentifier>());
            goreCache.AddRange(Resources.FindObjectsOfTypeAll<GoreSplatter>());
            explosionCache.AddRange(Resources.FindObjectsOfTypeAll<ExplosionController>());
        }

        private void Update()
        {
            OptimizeObjectType(enemyCache);
            OptimizeObjectType(goreCache);
            OptimizeObjectType(explosionCache);
        }

        private void OptimizeObjectType<T>(List<T> cache) where T : Component
        {
            foreach (T t in cache)
            {
                if (t == null) continue;

                Renderer[] renderers = t.GetComponentsInChildren<Renderer>(true); // true = include inactive
                foreach (Renderer renderer in renderers)
                {
                    if (!alreadyDoneRenderers.Contains(renderer))
                    {
                        RemoveOnUnseen removeOnUnseen = renderer.gameObject.AddComponent<RemoveOnUnseen>();
                        removeOnUnseen.theObject = t.gameObject;
                        activeRemoveComponents.Add(removeOnUnseen); // Track added component
                        alreadyDoneRenderers.Add(renderer);
                    }
                }
            }
        }

        public override void RemoveEffect()
        {
            // Remove all RemoveOnUnseen components added during the effect
            foreach (RemoveOnUnseen component in activeRemoveComponents)
            {
                if (component != null)
                {
                    Destroy(component);
                }
            }
            activeRemoveComponents.Clear();
            alreadyDoneRenderers.Clear(); // Clear the renderers hashset to avoid stale data
        }
    }
}