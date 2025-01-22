using UnityEngine;

public class FlagHandler : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;

    private Flag _spawnedFlag;
    private bool _isFlagSet = false;

    public bool IsFlagSet => _isFlagSet;

    public void SetFlag(Vector3 position)
    {
        if (_isFlagSet)
        {
            _spawnedFlag.SetPosition(position);
        }
        else
        {
            _isFlagSet = true;
            _spawnedFlag = _flagPrefab.Spawn(position);
        }
    }

    public void Update()
    {
        if (_spawnedFlag == null)
        {
            _isFlagSet = false;
        }
    }

    public Flag TryGetFlag()
    {
        return _spawnedFlag;
    }
}
