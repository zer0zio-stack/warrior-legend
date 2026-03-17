using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 要跟随的玩家
    public float smoothTime = 0.3f; // 跟随的平滑时间，越小越灵敏
    public Vector3 offset; // 相机与玩家的偏移量 (例如: 0, 0, -10)

    private Vector3 velocity = Vector3.zero;
    
    // 用于边界限制的变量
    public Vector2 minBounds; 
    public Vector2 maxBounds;
    public bool limitMovement = false; // 是否开启边界限制

    // LateUpdate 在Update之后执行，确保玩家位置更新后再移动相机，避免抖动
    void LateUpdate()
    {
        if (target is null) return;

        // 1. 计算期望位置：玩家位置 + 偏移量
        Vector3 desiredPosition = target.position + offset;

        // 2. 边界限制（可选）：将计算出的位置限制在预设的范围内
        if (limitMovement)
        {
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);
        }

        // 3. 使用 SmoothDamp 实现平滑移动
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }
}