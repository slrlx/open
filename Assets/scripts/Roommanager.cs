using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private GameObject[] rooms;

    void Start()
    {
        // Roommanager의 모든 자식 오브젝트(room들)를 배열로 자동 수집
        int childCount = transform.childCount;
        rooms = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            rooms[i] = transform.GetChild(i).gameObject;
        }

        // index 0 (room1)만 활성화, 나머지는 비활성화
        MoveToRoom(0);
    }

    public void MoveToRoom(int index)
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            rooms[i].SetActive(i == index);
        }
    }
}