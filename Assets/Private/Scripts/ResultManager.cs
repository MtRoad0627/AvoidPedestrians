using UnityEngine;
using TMPro;

public class ResultDisplay : MonoBehaviour
{
    public GameObject PerfectCanvas;
    public GameObject WinCanvas;
    public GameObject NotClearCanvas;
    
    public TextMeshProUGUI[] collisionCountTexts; // 衝突回数を表示するUIテキストの配列

    void Start()
    {
        // PlayerPrefsから衝突回数を取得
        int collisionCount = PlayerPrefs.GetInt("CollisionCount", 0);

        // 衝突回数に応じてキャンバスの状態を設定
        PerfectCanvas.SetActive(collisionCount == 0);
        WinCanvas.SetActive(collisionCount > 0 && collisionCount < 10);
        NotClearCanvas.SetActive(collisionCount >= 10);

        // UIテキストに衝突回数を表示
        foreach (var text in collisionCountTexts)
        {
            text.text = collisionCount.ToString() + " 人";
        }
    }
}

