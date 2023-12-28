using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    [SerializeField]
    private Transform target;
    [SerializeField] [Range(0.01f, 1f)]
    private float smoothSpeed = 0.125f;
    [SerializeField]
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    public bool isYLocked;
    private void LateUpdate() {
        Vector3 desiredPosition = target.position + offset;
        if(isYLocked){
            desiredPosition.y = transform.position.y;
        }
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }
}
