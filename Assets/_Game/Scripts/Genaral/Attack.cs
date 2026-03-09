using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;

    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Charactor>()?.TakeDamage(this);
    }
}