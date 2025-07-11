using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }
    private void Update()
    {
        HandleRootation();
    }
    private void HandleRootation()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {
            float yAngle = horizontalInput > 0 ? 0f : 180f;

            _transform.rotation = Quaternion.Euler(0f, yAngle, 0f);
        }
    }
}
