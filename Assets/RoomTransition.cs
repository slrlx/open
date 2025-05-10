using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 0.1f;

    [Header("방 전환 시 사용되는 검은 화면")]
    public GameObject blackScreen;

    [Header("Room1~Room5 오브젝트들")]
    public List<GameObject> rooms;

    [SerializeField] private int currentRoomIndex = 0;

    public void MoveToRoom(int index)
    {
        Debug.Log("MoveToRoom called with index: " + index);
        if (index >= 0 && index < rooms.Count && index != currentRoomIndex)
        {
            StartCoroutine(Transition(index));
        }
    }

    private IEnumerator Transition(int nextRoomIndex)
    {
        blackScreen.SetActive(true);
        Image image = blackScreen.GetComponent<Image>();
        if (image != null)
        {
            // Fade in the black screen
            for (float t = 0; t < 1f; t += Time.deltaTime / fadeDuration)
            {
                image.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, t));
                yield return null;
            }

            SetRoomActive(rooms[currentRoomIndex], false);
            SetRoomActive(rooms[nextRoomIndex], true);
            currentRoomIndex = nextRoomIndex;

            // Fade out the black screen
            for (float t = 0; t < 1f; t += Time.deltaTime / fadeDuration)
            {
                image.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, t));
                yield return null;
            }

            blackScreen.SetActive(false);
        }
    }

    private void Start()
    {
        // 시작 시 모든 Room 비활성화하고 현재 것만 활성화
        for (int i = 0; i < rooms.Count; i++)
        {
            SetRoomActive(rooms[i], i == currentRoomIndex);
        }

        blackScreen.SetActive(false);
    }

    // 이 함수가 모든 자식까지 포함해서 SetActive를 조절함
    private void SetRoomActive(GameObject room, bool isActive)
    {
        room.SetActive(isActive);

        // 방 안에 Canvas가 있다면 재활성화만 해준다 (UI 렌더링 복구용)
        Canvas canvas = room.GetComponentInChildren<Canvas>();
        if (canvas != null)
        {
            canvas.enabled = false;
            canvas.enabled = true;
        }
    }
}