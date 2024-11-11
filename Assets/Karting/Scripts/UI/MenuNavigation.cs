using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    public Selectable defaultSelection;
    public Canvas activeCanvas; // 現在アクティブなキャンバスを指定するための変数

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EventSystem.current.SetSelectedGameObject(null);
    }

    void LateUpdate()
    {
        // 現在アクティブなキャンバスがあるかどうかをチェック
        if (activeCanvas.gameObject.activeInHierarchy)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                if (Input.GetButtonDown(GameConstants.k_ButtonNameSubmit)
                    || Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal) != 0
                    || Input.GetAxisRaw(GameConstants.k_AxisNameVertical) != 0)
                {
                    EventSystem.current.SetSelectedGameObject(defaultSelection.gameObject);
                }
            }

            // エンターキーが押されたら、現在選択されているボタンをアクティベートします
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
            }
        }
    }
}

