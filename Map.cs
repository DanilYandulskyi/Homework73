using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Base _base;

    private void OnMouseDown()
    {
        _base.HandleMouseButtonDown();
    }
}
