using UnityEngine;

public class PooledObjectController : MonoBehaviour
{
    private float moveSpeed = 25.0f;
    private float safeDistance = 100.0f;
    private bool isHuman = false;
    private bool isCollidingWithFence = false; // Fences 충돌 상태
    private GameObject player;
    private Vector3 lastPosition;
    Animator ani;
    SpriteRenderer sr;

    void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();

        if (ani != null) // Animator가 존재할 경우에만 동작
        {
            float sprite_select = Random.Range(0, 6);
            switch (sprite_select)
            {
                case 0: ani.SetTrigger("MAN1"); break;
                case 1: ani.SetTrigger("MAN2"); break;
                case 2: ani.SetTrigger("MAN3"); break;
                case 3: ani.SetTrigger("WOMEN1"); break;
                case 4: ani.SetTrigger("WOMEN2"); break;
                case 5: ani.SetTrigger("WOMEN3"); break;
            }
        }
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (CompareTag("human"))
        {
            MoveAwayFromPlayer();
        }
    }

    void MoveAwayFromPlayer()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance < safeDistance)
            {
                Vector3 directionAway = (transform.position - player.transform.position).normalized;
                transform.position += directionAway * moveSpeed * Time.deltaTime;

                if (!isCollidingWithFence) // Fences와 충돌 중이 아닐 때만 flipX 업데이트
                {
                    Vector3 movementDirection = transform.position - lastPosition;

                    if (movementDirection.x > 0)
                    {
                        sr.flipX = true;
                    }
                    else if (movementDirection.x < 0)
                    {
                        sr.flipX = false;
                    }
                }

                lastPosition = transform.position;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Fences"))
        {
            Debug.Log("Fences와 충돌: Sprite 방향 업데이트 중지");
            isCollidingWithFence = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Fences"))
        {
            Debug.Log("Fences와의 충돌 종료: Sprite 방향 업데이트 재개");
        }
    }
}
