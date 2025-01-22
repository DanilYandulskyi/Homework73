using UnityEngine;
using System.Collections;

public class GoldSpawner : MonoBehaviour
{
    [SerializeField] private Gold _gold;
    [SerializeField] private float _interval;

    private void Awake()
    {
        StartCoroutine(SpawnResource());
    }

    private IEnumerator SpawnResource()
    {
        while (enabled)
        {
            Spawn();
            yield return new WaitForSeconds(_interval);
        }
    }

    private void Spawn()
    {
        int minValue = -5;
        int maxValue = 5;

        Instantiate(_gold, new Vector3(Random.Range(minValue, maxValue),
        0, Random.Range(minValue, maxValue)), Quaternion.identity);
    }
}
