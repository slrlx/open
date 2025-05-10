using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public GameObject currentRoom;
    public GameObject nextRoom;
    public FadeManager fadeManager;

    private void OnMouseDown()
    {
        fadeManager.FadeIn(() =>
        {
            currentRoom.SetActive(false);
            nextRoom.SetActive(true);
            fadeManager.FadeOut();
        });
    }
}