using UnityEngine;
using UnityEngine.EventSystems;

public class RoomButtonController : MonoBehaviour
{
    public GameObject moveButtons;      // 이동 버튼 묶음
    public GameObject moveImage;         // 이동 이미지

    private bool isMoveButtonsActive = false;

    // 토글 스위치 눌렀을 때 호출
    public void ToggleMoveButtons()
    {
        isMoveButtonsActive = !moveButtons.activeSelf;

        // 이동 버튼은 토글 상태에 맞게 활성화
        moveButtons.SetActive(isMoveButtonsActive);

        if (moveImage != null)
        {
            moveImage.SetActive(isMoveButtonsActive);
        }
    }

    private void Start()
    {
        if (moveButtons != null)
        {
            moveButtons.SetActive(false);
        }

        if (moveImage != null)
        {
            moveImage.SetActive(false);
        }
    }

    private void Update()
    {
        if (isMoveButtonsActive && Input.GetMouseButtonDown(0))
        {
            // UI 밖 클릭 시
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                isMoveButtonsActive = false;
                moveButtons.SetActive(false);
            }
        }
    }
    // 새로운 방에 들어갈 때 이동 버튼을 숨기기 위한 메서드
    public void HideMoveButtons()
    {
        isMoveButtonsActive = false;

        if (moveButtons != null)
        {
            moveButtons.SetActive(false);
        }

        if (moveImage != null)
        {
            moveImage.SetActive(false);
        }
    }
}