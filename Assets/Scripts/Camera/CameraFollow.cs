using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    public GameObject objectToTarget;
    [SerializeField]
    private Transform target;
    [SerializeField] [Range(0.01f, 1f)]
    private float smoothSpeed = 0.125f;
    [SerializeField]
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    public bool isYLocked;
    private void LateUpdate() {
        target = objectToTarget.transform;
        Vector3 desiredPosition = target.position + offset;
        if(isYLocked){
            desiredPosition.y = transform.position.y;
        }
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }
}
