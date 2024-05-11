using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class ExplodeCollidedObject : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayers;

    // 衝突回数を記録する変数
    private int collisionCount = 0;

    // 衝突回数を表示するためのUIテキスト
    public TextMeshProUGUI collisionCountText;

    private void Start()
    {
        // PlayerPrefsの衝突回数をリセット
        PlayerPrefs.SetInt("CollisionCount", 0);

        // UIテキストを更新
        if (collisionCountText != null)
        {
            collisionCountText.text = "衝突人数: " + collisionCount;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 衝突対象のレイヤがtargetLayersに含まれている場合のみ実行
        if ((targetLayers & (1 << other.gameObject.layer)) == 0) return;
        
        // NavMeshAgentを無効化
        var agent = other.GetComponent<NavMeshAgent>();
        if (agent.enabled == true)
        {
            // 衝突サウンドを再生
            GetComponent<AudioSource>().Play(); 
            agent.enabled = false;

            // 衝突回数を増やす
            collisionCount++;

            // 衝突回数をPlayerPrefsに保存（本当はシーンが変わる直前にやるべきだろう）
            PlayerPrefs.SetInt("CollisionCount", collisionCount);
            PlayerPrefs.Save(); // 変更を確実に保存

            // UIテキストを更新
            if (collisionCountText != null)
            {
                collisionCountText.text = "衝突人数: " + collisionCount;
            }
        }

        //PedestrianAgentを無効化
        var PedestrianAgent = other.GetComponent<PedestrianAgent>();
        if (PedestrianAgent != null)
        {
            PedestrianAgent.enabled = false;
        }

        //MainstreetAgentを無効化
        var MainstreetAgent = other.GetComponent<MainstreetAgent>();
        if (MainstreetAgent != null)
        {
            MainstreetAgent.enabled = false;
        }
    }
}