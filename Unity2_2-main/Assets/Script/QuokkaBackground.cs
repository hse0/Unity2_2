using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QuokkaBackground : MonoBehaviour
{
    public Image quokkaImage;         // 쿼카 이미지
    public float moveDuration = 4f;   // 쿼
    public Vector2 startPosition;     // ���� ��ġ
    public Vector2 endPosition;       // �� ��ġ
    public float fadeinDuration = 2f; // ���̵� �� �ð�
    private bool isRunning = false;   // �ִϸ��̼� ������ Ȯ��

    private Renderer objectRenderer;  // ������Ʈ�� ������

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        OnButtonClick();
    }

    public void OnButtonClick()
    {
        if (isRunning) return;

        isRunning = true;

        if (objectRenderer != null)
        {
            objectRenderer.enabled = false;
        }

        quokkaImage.gameObject.SetActive(true);
        quokkaImage.color = new Color(quokkaImage.color.r, quokkaImage.color.g, quokkaImage.color.b, 0f);
        quokkaImage.rectTransform.anchoredPosition = startPosition;

        quokkaImage.DOColor(new Color(quokkaImage.color.r, quokkaImage.color.g, quokkaImage.color.b, 1f), fadeinDuration);

        StartCoroutine(MoveQuokka());
    }

    IEnumerator MoveQuokka()
    {
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            quokkaImage.rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        quokkaImage.rectTransform.anchoredPosition = endPosition;

        yield return new WaitForSeconds(1f);
        quokkaImage.gameObject.SetActive(false);

        if (objectRenderer != null)
        {
            objectRenderer.enabled = true;
        }

        isRunning = false;
    }
}
