using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Items.Inventory;
using UnityEngine;

namespace TableTopAR.Items
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Items/DropLibrary")]
    public class DropLibrary : ScriptableObject
    {
        [System.Serializable]
        private class DropConfig
        {
            public InventoryItem Item;
            public float[] RelativeChance;
            public int[] MaxNumber;
            public int[] MinNumber;

            public int GetRandomNumber(int level)
            {
                if (!Item.IsStackable())
                {
                    return 1;
                }
                return UnityEngine.Random.Range(GetByLevel(MinNumber, level), GetByLevel(MaxNumber, level) + 1);
            }
        }

        public struct Dropped
        {
            public InventoryItem Item;
            public int Number;
        }

        [SerializeField]
        private DropConfig[] _potentialDrops;
        [SerializeField]
        private float[] _dropChance;
        [SerializeField]
        private int[] _minDrops;
        [SerializeField]
        private int[] _maxDrops;

        public IEnumerable<Dropped> GetRandomDrops(int level)
        {
            if (!ShouldRandomDrop(level))
            {
                yield break;
            }
            for (int i = 0; i < GetRandomNumberOfDrops(level); i++)
            {
                yield return GetRandomDrop(level);
            }
        }

        private Dropped GetRandomDrop(int level)
        {
            var dropConfig = SelectRandomItem(level);
            return new Dropped
                        {
                            Item = dropConfig.Item,
                            Number = dropConfig.GetRandomNumber(level)
                        };
        }

        private bool ShouldRandomDrop(int level)
        {
            return UnityEngine.Random.Range(0, 100) < GetByLevel(_dropChance, level);
        }

        private int GetRandomNumberOfDrops(int level)
        {
            return UnityEngine.Random.Range(GetByLevel(_minDrops, level), GetByLevel(_maxDrops, level) + 1);
        }

        private DropConfig SelectRandomItem(int level)
        {
            float totalchance = GetTotalChance(level);
            float randomRoll = UnityEngine.Random.Range(0, totalchance);
            float chanceTotal = 0;
            foreach (var drop in _potentialDrops)
            {
                chanceTotal += GetByLevel(drop.RelativeChance, level);
                if (chanceTotal > randomRoll)
                {
                    return drop;
                }
            }
            return null;
        }

        private float GetTotalChance(int level)
        {
            float total = 0;
            foreach (var drop in _potentialDrops)
            {
                total += GetByLevel(drop.RelativeChance, level);
            }
            return total;
        }

        private static T GetByLevel<T>(T[] values, int level)
        {
            if (values.Length <= 0)
            {
                return default;
            }
            if (level > values.Length)
            {
                return values[values.Length - 1];
            }
            if (level <= 0)
            {
                return default;
            }
            return values[level - 1];
        }
    }
}