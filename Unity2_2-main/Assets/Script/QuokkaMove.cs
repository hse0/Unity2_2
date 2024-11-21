using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuokkaMove : MonoBehaviour
{
    public RectTransform ComboImage;
    private Vector2 startPosition = new Vector2(400, -250); 
    private Vector2 targetPosition = new Vector2(-200, -250);
    private float speed;
    private float ImageSpeed;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI itemText;
    public GameObject bananaPeelPrefab; 

    private float Banana_PeelPosMin = -30.0f; 
    private float Banana_PeelPosMax = 30.0f;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;

    public int score = 0;
    public int itemsCollected = 0; 

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ImageSpeed = 400.0f;
        speed = 50.0f; // 쿼카 이동 속도
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(movement.x, movement.y).normalized;

        transform.Translate(direction * speed * Time.deltaTime);

        bool isMoving = movement.sqrMagnitude > 0;
        animator.SetBool("isMoving", isMoving);

        if (isMoving)
        {
            if (movement.x > 0)
            {
                animator.SetTrigger("IsRight");
            }
            else if (movement.x < 0)
            {
                animator.SetTrigger("IsLeft");
            }
            else if (movement.y > 0)
            {
                animator.SetTrigger("IsUp");
            }
            else if (movement.y < 0)
            {
                animator.SetTrigger("IsDown");
            }
        }

        if (!isMoving)
        {
            animator.ResetTrigger("IsRight");
            animator.ResetTrigger("IsLeft");
            animator.ResetTrigger("IsUp");
            animator.ResetTrigger("IsDown");
        }

        scoreText.text = "score : " + score;
        itemText.text = " x  " + itemsCollected;

        if (Input.GetKeyDown(KeyCode.Space) && itemsCollected > 0)
        {
            PlaceBananaPeel();
        }
    }

    private void PlaceBananaPeel()
    {
        Vector3 spawnPos = GetRandomSpawnPosition(gameManager.quokka);
        Instantiate(bananaPeelPrefab, spawnPos, Quaternion.identity);

        itemsCollected--;
    }

    Vector3 GetRandomSpawnPosition(GameObject referenceObject)
    {
        float randomX = Random.Range(Banana_PeelPosMin, Banana_PeelPosMax);
        float randomY = Random.Range(Banana_PeelPosMin, Banana_PeelPosMax);
        return new Vector3(referenceObject.transform.position.x + randomX, referenceObject.transform.position.y + randomY, -1);
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("banana"))
        {
            itemsCollected += 1;
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("human"))
        {
            score += 1;
            collision.gameObject.SetActive(false);
            StartCoroutine(MoveAndReturn());
        }
    }

    IEnumerator MoveAndReturn()
    {
        yield return StartCoroutine(MoveToPosition(targetPosition));
        
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(MoveToPosition(startPosition));
    }

    IEnumerator MoveToPosition(Vector2 destination)
    {
        while (Vector2.Distance(ComboImage.anchoredPosition, destination) > 0.1f)
        {
            ComboImage.anchoredPosition = Vector2.MoveTowards(ComboImage.anchoredPosition, destination, ImageSpeed * Time.deltaTime);
            yield return null;
        }

        ComboImage.anchoredPosition = destination;
    }
}
