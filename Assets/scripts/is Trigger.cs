using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public int targetRoomIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 태그 없이 누구든 들어가면 실행됨
        FindObjectOfType<FadeManager>().FadeToRoom(targetRoomIndex);
    }
}