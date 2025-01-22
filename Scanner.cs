using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Scanner : MonoBehaviour
{
    [SerializeField] private List<Gold> _gold = new List<Gold>();
    [SerializeField] private float _scanDelay;
    [SerializeField] private float _scanRadius;

    private List<Gold> _takenGold = new List<Gold>();

    public IReadOnlyList<Gold> Gold => _gold;

    private void Start()
    {
        StartCoroutine(Scan());
    }

    public void RemoveGold(Gold gold)
    {
        if (_gold.Contains(gold))
        {
            _gold.Remove(gold);
            _takenGold.Add(gold);
        }
    }
    
    private IEnumerator Scan()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(_scanDelay);

            Collider[] colliders = Physics.OverlapSphere(transform.position, _scanRadius);

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out Gold gold))
                {
                    if (_takenGold.Contains(gold) == false)
                    {
                        _gold.Add(gold);
                    }
                }
            }
        }
    }
}
