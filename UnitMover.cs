using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _moveDirection;
    
    private float _initialSpeed;
    private Transform _transform;
    
    private void Awake()
    {
        _transform = transform;
        _initialSpeed = _speed;
    }

    public void Move(Vector3 direction)
    {
        if (_moveDirection != Vector3.zero)
        {
            direction = _moveDirection;
        }

        _speed = _initialSpeed;
        Vector3 offset = direction.normalized * (_speed * Time.deltaTime);

        _transform.Translate(offset);
    }

    public void SetDirection(Vector3 direction)
    {
        _moveDirection = direction;
    }

    public void Stop()
    {
        _speed = 0;
        _moveDirection = Vector3.zero;
    }
}
