using UnityEngine;
using UnityEngine.AI;

public class SmoothWanderingAI : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;

    private NavMeshAgent agent;
    private float timer;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        // 初期の目的地を設定
        SetNewDestination();
    }

    private void Update()
    {
        // ここに通常の Update() の処理を記述
        timer += Time.deltaTime;

        // 目的地に近づいたら新しい目的地を設定
        if (Vector3.Distance(transform.position, agent.destination) < 2.0f || timer >= wanderTimer)
        {
            SetNewDestination();
            timer = 0;
        }
    }

    // 新しい目的地を設定するメソッド
    private void SetNewDestination()
    {
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.SetDestination(newPos);
    }

    // NavMesh上のランダムな位置を返すメソッド
    private Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
}
