using UnityEngine;
using UnityEngine.AI;

public class ExplodeCollidedObject : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayers;

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
        }

        //SmoothWanderingAIを無効化
        var smoothWanderingAI = other.GetComponent<SmoothWanderingAI>();
        if (smoothWanderingAI != null)
        {
            smoothWanderingAI.enabled = false;
        }
    }
}