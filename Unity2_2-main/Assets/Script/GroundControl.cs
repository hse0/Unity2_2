using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundControl : MonoBehaviour
{
    public Transform player;   // �÷��̾��� Transform, Ȥ�� ī�޶��� Transform�� ����� ���� ����
    public Transform[] backgrounds;  // 9���� ��� Ÿ�ϵ��� ��� �ִ� Transform �迭 (3x3 Ÿ�� �׸���)
    private float backgroundWidth;   // ��� Ÿ�� �ϳ��� ���� ���̸� ������ ����
    private float backgroundHeight;  // ��� Ÿ�� �ϳ��� ���� ���̸� ������ ����

    // �߰�: ��� Ÿ���� ������ ���� (���� 9, ���� 16�� ���)
    public float aspectRatioX = 9f;  // ���� ����
    public float aspectRatioY = 16f; // ���� ����

    void Start()
    {
        backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
        backgroundHeight = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.y;

        // ��� Ÿ���� ����, ���� ������ �����Ͽ� ũ�� ����
        backgroundWidth *= aspectRatioX / aspectRatioY;  // ���� ������ ����
        backgroundHeight *= aspectRatioY / aspectRatioX; // ���� ������ ����
    }

    void Update()
    {
        // �� �����Ӹ��� ��� ��� Ÿ�Ͽ� ���� ��ġ�� Ȯ���ϰ� �ʿ��ϸ� �̵���Ŵ
        foreach (Transform background in backgrounds)
        {
            // �÷��̾ ���� ��� Ÿ���� �������� �Ѿ�� (��� Ÿ���� x ��ġ + Ÿ���� �ʺ� �ʰ�)
            if (player.position.x > background.position.x + backgroundWidth)
            {
                MoveBackgroundRight(background);  // �ش� Ÿ���� ���������� �̵���Ŵ
            }
            // �÷��̾ ���� ��� Ÿ���� ������ �Ѿ�� (��� Ÿ���� x ��ġ - Ÿ���� �ʺ� �̸�)
            else if (player.position.x < background.position.x - backgroundWidth)
            {
                MoveBackgroundLeft(background);   // �ش� Ÿ���� �������� �̵���Ŵ
            }

            // �÷��̾ ���� ��� Ÿ���� ������ �Ѿ�� (��� Ÿ���� y ��ġ + Ÿ���� ���� �ʰ�)
            if (player.position.y > background.position.y + backgroundHeight)
            {
                MoveBackgroundUp(background);     // �ش� Ÿ���� �������� �̵���Ŵ
            }
            // �÷��̾ ���� ��� Ÿ���� �Ʒ����� �Ѿ�� (��� Ÿ���� y ��ġ - Ÿ���� ���� �̸�)
            else if (player.position.y < background.position.y - backgroundHeight)
            {
                MoveBackgroundDown(background);   // �ش� Ÿ���� �Ʒ������� �̵���Ŵ
            }
        }
    }

    // ��� Ÿ���� ���������� �̵���Ű�� �Լ�
    void MoveBackgroundRight(Transform background)
    {
        float newX = background.position.x + backgroundWidth * 3;
        background.position = new Vector3(newX, background.position.y, background.position.z);
    }

    // ��� Ÿ���� �������� �̵���Ű�� �Լ�
    void MoveBackgroundLeft(Transform background)
    {
        float newX = background.position.x - backgroundWidth * 3;
        background.position = new Vector3(newX, background.position.y, background.position.z);
    }

    // ��� Ÿ���� �������� �̵���Ű�� �Լ�
    void MoveBackgroundUp(Transform background)
    {
        float newY = background.position.y + backgroundHeight * 3;
        background.position = new Vector3(background.position.x, newY, background.position.z);
    }

    // ��� Ÿ���� �Ʒ������� �̵���Ű�� �Լ�
    void MoveBackgroundDown(Transform background)
    {
        float newY = background.position.y - backgroundHeight * 3;
        background.position = new Vector3(background.position.x, newY, background.position.z);
    }
}
