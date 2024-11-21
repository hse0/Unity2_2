using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZooKeeperController : MonoBehaviour
{
    float moveSpeed = 30.0f; // 사육사 이동 속도   
    private GameObject quokka;
    SpriteRenderer sr;
    Animator ani;
    public int sprite_select;

    void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        sprite_select = Random.Range(0, 2);
        switch (sprite_select)
        {
            case 0:
                ani.SetTrigger("ZooKeepMan");
                break;
            case 1:
                ani.SetTrigger("ZooKeepWomen");
                break;
        }
    }

    private void Start()
    {
        quokka = GameObject.FindGameObjectWithTag("Player");  
    }

    void Update()
    {
        Vector3 direction = (quokka.transform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // 현재 ZooKeeper의 x 좌표가 quokka 기준 양수/음수인지 확인하여 flipX 설정
        if (transform.position.x < quokka.transform.position.x)
        {
            sr.flipX = true; // ZooKeeper가 quokka의 왼쪽에 있으면 flipX true
        }
        else
        {
            sr.flipX = false; // ZooKeeper가 quokka의 오른쪽에 있으면 flipX false
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("EndScene");
        }
        
        else if (collision.CompareTag("bananapeel"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
