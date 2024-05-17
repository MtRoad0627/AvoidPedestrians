using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    public Transform player; // プレイヤーのTransform
    public float distanceUp = 15f; // プレイヤーから上にどれだけ離れるか
    public float distanceBack = 10f; // プレイヤーから後ろにどれだけ離れるか
    public float smooth = 5f; // カメラの動きを滑らかにするための値
    private Vector3 targetPosition; // カメラが移動すべき目標位置

    void LateUpdate() {
        // プレイヤーの位置に基づいてカメラの目標位置を計算する
        targetPosition = player.position + Vector3.up * distanceUp - player.forward * distanceBack;
        
        // カメラの位置を目標位置に滑らかに移動させる
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
        
        // カメラをプレイヤーの方向に向ける
        transform.LookAt(player);
    }
}
