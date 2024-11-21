using UnityEngine;

public class TriggerColliderController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player와 충돌: Human 비활성화 처리");
            transform.parent.gameObject.SetActive(false); // Human 객체 비활성화
        }
    }
}