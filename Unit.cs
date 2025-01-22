using System;
using UnityEngine;

[RequireComponent(typeof(UnitMover))]
public class Unit : MonoBehaviour
{
    private const float DistanceToStop = 0.2f;

    [SerializeField] private Vector3 _initialPosition;
    [SerializeField] private Gold _gold;
    [SerializeField] private Flag _flag;

    private IUnitTarget _target;
    private UnitMover _mover;

    public event Action<Gold> CollectedResource;

    public bool IsResourceCollected { get; private set; } = false;
    public bool IsStanding { get; private set; } = true;

    public Vector3 InitialPosition => _initialPosition;

    private void Awake()
    {
        _mover = GetComponent<UnitMover>();
        _initialPosition = transform.position;
    }

    private void Update()
    {
        if (_gold != null)
        { 
            if (_gold.isActiveAndEnabled == false)
            {
                _target = null;
                _gold = null;
                IsStanding = true;
            }
        }

        if (IsResourceCollected & Vector2.SqrMagnitude(transform.position - _initialPosition) <= DistanceToStop)
            OnCollectedGold();

        if (_target != null)
        {
            _mover.Move(_target.Transform.position - transform.position);
            IsStanding = false;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.TryGetComponent(out IUnitTarget unitTarget))
        {
            if (unitTarget == _target)
            {
                if (unitTarget is Gold)
                {
                    IsResourceCollected = true;
                    _mover.SetDirection(_initialPosition - transform.position);
                    _gold.StartFollow(transform);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IUnitTarget unitTarget))
        {
            if (unitTarget == _target)
            {
                if (unitTarget is Flag)
                {
                    _flag.SpawnBase().Assign(this);
                    Destroy(_flag.gameObject);
                    _target = null;
                    _flag = null; 
                    IsStanding = true;
                }
            }
        }
    }

    public void SetTarget(IUnitTarget unitTarget)
    {
        _target = unitTarget;

        if (_target is Gold)
            _gold = (Gold)_target;
        else if (_target is Flag)
            _flag = (Flag)_target;
    }

    private void OnCollectedGold()
    {
        CollectedResource?.Invoke(_gold);
        _mover.Stop();
        IsResourceCollected = false;
        IsStanding = true;
        _gold.StopFollow();
        _gold.Disable();
        _gold = null;
        _target = null;
    }

    public void SetInitialPosition(Vector3 position)
    {
        _initialPosition = position;
    }
}
