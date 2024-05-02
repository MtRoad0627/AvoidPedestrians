using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomAgentMovement : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float avoidanceDistance = 2f; // 障害物を避ける距離

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // 目的地への方向を計算
        Vector3 desiredDirection = target.position - transform.position;

        // 障害物を避けるためにRaycastを使用
        RaycastHit hit;
        if (Physics.Raycast(transform.position, desiredDirection, out hit, avoidanceDistance))
        {
            // 障害物が近くにある場合、避ける方向を計算
            Vector3 avoidDirection = Vector3.Cross(Vector3.up, hit.normal);
            desiredDirection += avoidDirection * avoidanceDistance;
        }

        // エージェントに目的地を設定
        agent.SetDestination(transform.position + desiredDirection);
    }
}



// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;

// public class AgentTest : MonoBehaviour
// {

//   [SerializeField]
//   Transform target;

//   NavMeshAgent agent;

//     // Start is called before the first frame update
//     void Start()
//     {
//     agent = GetComponent<NavMeshAgent>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//     agent.SetDestination(target.position);
//   }
// }