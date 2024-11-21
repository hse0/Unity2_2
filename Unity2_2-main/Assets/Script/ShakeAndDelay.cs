using UnityEngine;
using System.Collections;

public class ShakeAndDelay : MonoBehaviour
{
    public GameObject imageObject;  // ���� ���� �̹��� ������Ʈ
    public float shakeAmount = 0.1f; // ��鸮�� ����
    public float shakeDuration = 1.0f; // ��鸮�� �ð�
    public float delayTime = 0.0f;  // ��� ��鸮���� 0���� ����

    private Vector3 originalPosition; // ���� ��ġ

    void Start()
    {
        // ó�� ��ġ ����
        if (imageObject != null)
        {
            originalPosition = imageObject.transform.position;
            StartCoroutine(ShakeAndShow());
        }
    }

    IEnumerator ShakeAndShow()
    {
        // ���� ����
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            // ��鸲 ȿ��: �����ϰ� ��ġ�� �̵�
            float xOffset = Random.Range(-shakeAmount, shakeAmount);
            float yOffset = Random.Range(-shakeAmount, shakeAmount);
            imageObject.transform.position = originalPosition + new Vector3(xOffset, yOffset, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // ���� ������ ���� ��ġ�� ���ư���
        imageObject.transform.position = originalPosition;
    }
}
