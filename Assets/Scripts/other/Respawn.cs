using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] Transform Room;
    [SerializeField] Transform Player;
    [SerializeField] CameraController cam;
    private void Awake()
    {
        cam = Camera.main.GetComponent<CameraController>();
    }
    public void respawn_()
    {
        Player.position = transform.position;
        cam.MoveToNewRoom(Room);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null || collision.tag != "Player") return; 
        collision.GetComponent<PlayerState>().Set_respawn(this);
    }

}
