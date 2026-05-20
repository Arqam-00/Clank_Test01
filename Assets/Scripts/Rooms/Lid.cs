using UnityEngine;

public class Lid : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;
    //[SerializeField] private PlayerMovement player;


    private void Awake()
    {
        cam = Camera.main.GetComponent<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.y < transform.position.y)
            {
                cam.MoveToNewRoom(nextRoom);
                //player.PushUp(10.0f);
                //nextRoom.GetComponent<Room>().ActivateRoom(true);
                //previousRoom.GetComponent<Room>().ActivateRoom(false);
            }
            else
            {
                cam.MoveToNewRoom(previousRoom);
                //previousRoom.GetComponent<Room>().ActivateRoom(true);
                //nextRoom.GetComponent<Room>().ActivateRoom(false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.position.y > transform.position.y)
            {
                cam.MoveToNewRoom(nextRoom);
                //nextRoom.GetComponent<Room>().ActivateRoom(true);
                //previousRoom.GetComponent<Room>().ActivateRoom(false);
            }
            else
            {
                cam.MoveToNewRoom(previousRoom);
                //previousRoom.GetComponent<Room>().ActivateRoom(true);
                //nextRoom.GetComponent<Room>().ActivateRoom(false);
            }
        }
    }
}