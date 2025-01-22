using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private FlagHandler _flagHandler;

    public Base SpawnBase(Vector3 position)
    {
        return Instantiate(_base, position, Quaternion.identity);
    }

    public void InitializeBase(Scanner scanner, UnitSpawner unitSpawner, FlagHandler flagHandler)
    {
        _base.Initialize(scanner, unitSpawner, flagHandler);
    }
}
