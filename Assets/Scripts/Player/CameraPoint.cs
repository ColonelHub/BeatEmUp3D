using UnityEngine;

public class CameraPoint : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Transform _transform;

    private void Start()
    {
        _transform = transform; //cache the transform for performance
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            FollowTarget();
        }
    }
    private void FollowTarget()
    {
        _transform.position = new Vector3(target.position.x, _transform.position.y, _transform.position.z);
    }
}
