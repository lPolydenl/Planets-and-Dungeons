using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private Item[] items;
    [SerializeField] private int[] amount;

    public void Drop(Transform point)
    {
        for (int i = 0; i < items.Length; i++)
        {
            for (int j = 0; j < amount[i]; j++)
            {
                float chance = Random.Range(0f, 100f);
                if (chance <= items[i].chance)
                {
                    Instantiate(items[i], point.position, Quaternion.identity);
                }
            }
        }
    }


}
