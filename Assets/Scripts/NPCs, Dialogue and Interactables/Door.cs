using UnityEngine;

public class Door : BaseInteractable
{
    [SerializeField] private Transform otherDoor;
    private GameObject player;
    
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void TeleportToDoor()
    {
        player.transform.position = otherDoor.position;
    }

    public override void GetInteractedWith()
    {
        TeleportToDoor();
        whenInteractedWith.Invoke();
    }
}
