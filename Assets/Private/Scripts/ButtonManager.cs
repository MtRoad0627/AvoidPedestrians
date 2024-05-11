using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonEnterTrigger : MonoBehaviour
{
    private Button button;

    void Start()
    {
        // Buttonコンポーネントを取得
        button = GetComponent<Button>();
    }

    void Update()
    {
        // エンターキーが押されたかをチェック
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // Buttonがインタラクティブな状態であれば、OnClickイベントを呼び出す
            if (button.interactable)
            {
                button.onClick.Invoke();
            }
        }
    }
}
