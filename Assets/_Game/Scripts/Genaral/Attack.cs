using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;

    public float range;

    //攻击频率，每隔多少秒攻击一下
    public float frequency;

    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Charactor>()?.TakeDamage(this);
    }
}