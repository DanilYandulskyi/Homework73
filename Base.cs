using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Unit> _units = new List<Unit>();
    [SerializeField] private List<Gold> _collectedGold = new List<Gold>();

    [SerializeField] private Scanner _scanner;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private FlagHandler _flagHandler;
    
    [SerializeField] private int _price;

    private bool _isSelected;
    private bool _isFlagTaken = false;

    public event UnityAction GoldAmountChanged;

    public int CollectedResounrseAmount => _collectedGold.Count;

    private void Start()
    {
        foreach (Unit unit in _units)
        {
            unit.CollectedResource += CollectResource;
        }
    }

    private void LateUpdate()
    {
        if (_scanner.Gold.Count > 0)
        {
            for (int i = 0; i < _units.Count; i++)
            {
                if (_units[i].IsStanding)
                {
                    Gold gold = _scanner.Gold[0];

                    _units[i].SetTarget(gold);
                    _scanner.RemoveGold(gold);
                    break;
                }
            }
        }

        TrySendUnitToFlag();
    }

    public void Assign(Unit unit)
    {
        _units.Add(unit);
        unit.CollectedResource += CollectResource;

        unit.SetInitialPosition(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z));
    }

    public void HandleMouseButtonDown()
    {
        if (_isSelected == false)
        {
            _isSelected = true;
        }
        else
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                _flagHandler.SetFlag(new Vector3( hit.point.x, hit.point.y + 0.5f, hit.point.z));
            }
        }
    }

    private void CollectResource(Gold resource)
    {
        _collectedGold.Add(resource);

        TryAddNewUnit();

        GoldAmountChanged?.Invoke();
    }

    private void TryAddNewUnit()
    {
        if (_collectedGold.Count >= _unitSpawner.GoldToSpawn)
        {
            if (_flagHandler.IsFlagSet == false || _units.Count == 1)
            {
                Unit unit = _unitSpawner.SpawnUnit(_units[_units.Count - 1].InitialPosition + new Vector3(2, 0, 0));

                _units.Add(unit);

                unit.CollectedResource += CollectResource;

                for (int i = 0; i < _unitSpawner.GoldToSpawn; i++)
                {
                    _collectedGold.Remove(_collectedGold[_collectedGold.Count - 1]);
                }
            }
        }
    }

    private bool CanBuildNewBase()
    {
        return _collectedGold.Count >= _price & _flagHandler.IsFlagSet & _isFlagTaken == false;
    }

    private void TrySendUnitToFlag()
    {
        if (CanBuildNewBase())
        {
            for (int i = 0; i < _units.Count; i++)
            {
                Unit unit = _units[i];

                if (unit.IsStanding)
                {
                    unit.SetTarget(_flagHandler.TryGetFlag());
                    _isFlagTaken = true;

                    _units.Remove(unit);

                    for (int j = 0; j < _price; j++)
                    {
                        _collectedGold.Remove(_collectedGold[_collectedGold.Count - 1]);
                    }

                    break;
                }
            }
        }
    }

    public void Initialize(Scanner scanner, UnitSpawner unitSpawner, FlagHandler flagHandler)
    {
        _scanner = scanner;
        _unitSpawner = unitSpawner;
        _flagHandler = flagHandler;
    }

    private void OnDestroy()
    {
        foreach (Unit unit in _units)
        {
            unit.CollectedResource -= CollectResource;
        }
    }
}
