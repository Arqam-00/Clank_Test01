using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.01f;
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        targetPosition = new Vector3(_newRoom.position.x, _newRoom.position.y, transform.position.z);
    }
}