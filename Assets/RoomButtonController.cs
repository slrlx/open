using UnityEngine;
using UnityEngine.EventSystems;

public class RoomButtonController : MonoBehaviour
{
    public GameObject moveButtons;      // 이동 버튼 묶음
    public GameObject hintCoinButtons;  // 힌트 코인 버튼 묶음

    private bool isMoveButtonsActive = false;

    // 토글 스위치 눌렀을 때 호출
    public void ToggleMoveButtons()
    {
        isMoveButtonsActive = !moveButtons.activeSelf;

        // 이동 버튼은 토글 상태에 맞게 활성화
        moveButtons.SetActive(isMoveButtonsActive);

        // 힌트 코인 버튼은 이동 버튼이 켜지면 꺼지고, 꺼지면 켜짐
        hintCoinButtons.SetActive(!isMoveButtonsActive);
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

                // 힌트 코인 버튼 다시 켜기
                hintCoinButtons.SetActive(true);
            }
        }
    }
}