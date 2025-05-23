using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1.5f;

    [Header("방 전환 시 사용되는 검은 화면")]
    public GameObject blackScreen;

    [Header("Room1~Room5 오브젝트들")]
    public List<GameObject> rooms;

    [Header("전환 시 재생할 효과음")]
    public AudioClip footstepSFX;
    private AudioSource audioSource;

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
        // ⛔ 트랜지션 시작하자마자 버튼 숨기기
        Transform currentMoveButtons = rooms[currentRoomIndex].transform.Find("MoveButtons");
        if (currentMoveButtons != null) currentMoveButtons.gameObject.SetActive(false);

        blackScreen.SetActive(true);

        if (footstepSFX != null && audioSource != null)
        {
            audioSource.PlayOneShot(footstepSFX);
        }

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
            Transform nextMoveButtons = rooms[nextRoomIndex].transform.Find("MoveButtons");
            if (nextMoveButtons != null) nextMoveButtons.gameObject.SetActive(true);
            currentRoomIndex = nextRoomIndex;

            // Fade out the black screen
            for (float t = 0; t < 1f; t += Time.deltaTime / fadeDuration)
            {
                image.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, t));
                yield return null;
            }

            // blackScreen.SetActive(false);
        }
    }

    private void Start()
    {
        // 시작 시 모든 Room 비활성화하고 현재 것만 활성화
        for (int i = 0; i < rooms.Count; i++)
        {
            SetRoomActive(rooms[i], i == currentRoomIndex);

            // 모든 MoveButtons 오브젝트는 시작 시 비활성화
            Transform moveButtons = rooms[i].transform.Find("MoveButtons");
            if (moveButtons != null)
            {
                moveButtons.gameObject.SetActive(false);
            }
        }

        blackScreen.SetActive(false);

        // Disable raycast blocking on the black screen image after hiding it
        Image blackImage = blackScreen.GetComponent<Image>();
        if (blackImage != null)
        {
            blackImage.raycastTarget = false;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
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