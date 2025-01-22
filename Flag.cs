using UnityEngine;

public class Flag : MonoBehaviour, IUnitTarget
{
    [SerializeField] private BaseSpawner _baseSpawner;

    public Transform Transform => transform;

    public Base SpawnBase()
    {
        return _baseSpawner.SpawnBase(transform.position);
    }

    public void SetPosition(Vector3 position)
    {
        Transform.position = position;
    }

    public Flag Spawn(Vector3 position)
    {
        return Instantiate(this, position, Quaternion.identity);
    }
}
