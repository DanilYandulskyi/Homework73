using UnityEngine;

public class Gold : MonoBehaviour, IUnitTarget
{
    public Transform Transform => transform;

    public void StartFollow(Transform target)
    {
        Transform.SetParent(target);
    }

    public void StopFollow()
    {
        Transform.SetParent(null);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}

public interface IUnitTarget 
{
    Transform Transform { get; }
}