using UnityEngine;

public class AttackCollision : MonoBehaviour {
    Character Parent;
    private void Awake()
    {
        Parent = GetComponentInParent<Character>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            Parent.SetCanAttack(true);
            Parent.FirstTime = Time.time;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Parent.SetCanAttack(false);
            Parent.FirstTime = 0.0f;
        }
    }
}
