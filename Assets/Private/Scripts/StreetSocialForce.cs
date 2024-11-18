using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class PedestrianAgent : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] targets; // 目的地
    private int targetPoint = 0; // 目的地の番号
    public List<Transform> agentObstacles; // エージェント障害物のリスト
    public Transform[] ObstacleAgentGroups; // 複数のAgentgroupをインスペクターから割り当てる
    private int AgentRepulsionCoefficient = 200; // エージェント反発力の係数
    private int ambulanceRepulsionCoefficient = 600; // 救急車反発力の係数
    public Transform ambulanceObstacle; // 避けるべき障害物としての救急車
    public List<Transform> attractions; // 引力を及ぼす対象のリスト
    private float personalSpaceRadius = 3f; // 個人の空間の半径
    private float ambulanceSpaceRadius = 20f; // 救急車に対する個人の空間の半径
    public float attractionStrength = 1.0f; // 引力の強さ

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // エージェントの最大速度をランダムに設定
        agent.speed = Random.Range(0.5f, 5.0f);

        // エージェントにランダムタグがついていたら目的地をランダムに設定
        if (this.gameObject.tag == "Random")
        {
            targetPoint = Random.Range(0, targets.Length);
        }
        else // それ以外の場合は順番に目的地を設定
        {
            agent.destination = targets[0].position;
        }

        // 各Agentgroupの子オブジェクトをagentObstaclesリストに追加
        foreach (Transform group in ObstacleAgentGroups)
        {
            foreach (Transform agent in group)
            {
                // 自分自身を除外
                if (agent != this.transform)
                {
                    agentObstacles.Add(agent);
                }
            }
        }
    }

    private void Update()
    {
        // 目的地への加速度
        Vector3 desiredVelocity = (targets[targetPoint].position - transform.position).normalized * agent.speed;
        Vector3 acceleration = (desiredVelocity - agent.velocity) / Time.deltaTime;

        // 他の歩行者や障害物からの反発力
        foreach (Transform obstacle in agentObstacles)
        {
            Vector3 directionToObstacle = transform.position - obstacle.position;
            float Distance = directionToObstacle.magnitude;
            // 障害物が個人の空間内にある場合、反発力を計算
            if (Distance < personalSpaceRadius * personalSpaceRadius)
            {
                float strength = Mathf.Clamp01((personalSpaceRadius - Distance) / personalSpaceRadius) * AgentRepulsionCoefficient;
                Vector3 repulsionForce = directionToObstacle.normalized * strength;
                acceleration += repulsionForce;
            }
        }

        //救急車からの反発力
        Vector3 directionToAmbulanceObstacle = transform.position - ambulanceObstacle.position;
        float ambulanceDistance = directionToAmbulanceObstacle.magnitude;
        // 障害物が個人の空間内にある場合、反発力を計算
        if (ambulanceDistance < ambulanceSpaceRadius * ambulanceSpaceRadius) // 2乗された値で比較
        {
            float strength = Mathf.Clamp01((ambulanceSpaceRadius - ambulanceDistance) / ambulanceSpaceRadius) * ambulanceRepulsionCoefficient;
            Vector3 repulsionForce = directionToAmbulanceObstacle.normalized * strength;
            acceleration += repulsionForce;
        }


        // 引力効果
        foreach (Transform attraction in attractions)
        {
            Vector3 directionToAttraction = attraction.position - transform.position;
            float distance = directionToAttraction.magnitude;
            // 距離に応じて引力を計算
            float strength = attractionStrength / distance;
            Vector3 attractionForce = directionToAttraction.normalized * strength;
            acceleration += attractionForce;
        }

        // NavMeshAgentに力を適用
        agent.velocity += acceleration * Time.deltaTime;

        // エージェントが現目標地点に近づいてきたら、次の目標地点を選択します
        if ((targets[targetPoint].position-transform.position).magnitude < 2)
            GotoNextTarget();
        
    }

    void GotoNextTarget() {
        // 地点がなにも設定されていないときに返します
        if (targets.Length == 0)
            return;

        // エージェントが現在設定された目標地点に行くように設定
        agent.destination = targets[targetPoint].position;

        // エージェントにランダムタグがついていたら目的地をランダムに設定
        if (this.gameObject.tag == "Random")
        {
            targetPoint = Random.Range(0, targets.Length);
        }
        else // それ以外の場合は順番に目的地を設定
        {
            targetPoint = (targetPoint + 1) % targets.Length;
        }
    }

}
