using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleTextMovement : MonoBehaviour
{
    private float moveDistance = 70.0f; // The distance the text should move up and down

    private float moveSpeed = 1.0f; // The speed at which the text should move

    private float delayTime = 1.0f; // The time to wait before starting the movement

    private float moveDuration = 1.0f; // The duration of each movement

    private TextMeshProUGUI textObject; // The text object to move

    private Vector3 startPosition; // The starting position of the text object

    private void Start()
    {
        textObject = GetComponent<TextMeshProUGUI>();
        startPosition = textObject.transform.position;

        StartCoroutine(MoveText());
    }

    private IEnumerator MoveText()
    {
        // Wait for the delay time before starting the movement
        yield return new WaitForSeconds(delayTime);

        while (true)
        {
            // Move the text object up
            float t = 0f;
            while (t < moveDuration)
            {
                t += Time.deltaTime;
                float y =
                    Mathf
                        .Lerp(startPosition.y,
                        startPosition.y + moveDistance,
                        t / moveDuration);
                textObject.transform.position =
                    new Vector3(startPosition.x, y, startPosition.z);
                yield return null;
            }

            // Move the text object down
            t = 0f;
            while (t < moveDuration)
            {
                t += Time.deltaTime;
                float y =
                    Mathf
                        .Lerp(startPosition.y + moveDistance,
                        startPosition.y,
                        t / moveDuration);
                textObject.transform.position =
                    new Vector3(startPosition.x, y, startPosition.z);
                yield return null;
            }

            // Wait for a short time before starting the next movement
            yield return new WaitForSeconds(moveDuration * 0.5f / moveSpeed);
        }
    }
}
