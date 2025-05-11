using UnityEngine;
using UnityEngine.EventSystems;

public class RoomButtonController : MonoBehaviour
{
    public GameObject moveButtons; // 이동 버튼 묶음
    private bool isMoveButtonsActive = false;

    public void ToggleMoveButtons()
    {
        isMoveButtonsActive = !moveButtons.activeSelf;
        moveButtons.SetActive(isMoveButtonsActive);
    }

    private void Update()
    {
        if (isMoveButtonsActive && Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭한 곳이 UI 요소인지 확인
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                // UI 외부 클릭 → 이동 버튼 숨김
                moveButtons.SetActive(false);
                isMoveButtonsActive = false;
            }
        }
    }
}