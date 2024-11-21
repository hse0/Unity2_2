using UnityEngine;
using System.Collections;

public class ShakeAndDelay : MonoBehaviour
{
    public GameObject imageObject;  // 흔들고 싶은 이미지 오브젝트
    public float shakeAmount = 0.1f; // 흔들리는 강도
    public float shakeDuration = 1.0f; // 흔들리는 시간
    public float delayTime = 0.0f;  // 즉시 흔들리도록 0으로 설정

    private Vector3 originalPosition; // 원래 위치

    void Start()
    {
        // 처음 위치 저장
        if (imageObject != null)
        {
            originalPosition = imageObject.transform.position;
            StartCoroutine(ShakeAndShow());
        }
    }

    IEnumerator ShakeAndShow()
    {
        // 흔들기 시작
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            // 흔들림 효과: 랜덤하게 위치를 이동
            float xOffset = Random.Range(-shakeAmount, shakeAmount);
            float yOffset = Random.Range(-shakeAmount, shakeAmount);
            imageObject.transform.position = originalPosition + new Vector3(xOffset, yOffset, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 흔들기 끝내고 원래 위치로 돌아가기
        imageObject.transform.position = originalPosition;
    }
}
