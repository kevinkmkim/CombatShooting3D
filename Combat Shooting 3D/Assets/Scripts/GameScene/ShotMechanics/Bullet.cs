using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float delay = 5f;

    private float speed = 500f;

    void OnEnable()
    {
        transform.GetComponent<Rigidbody>().velocity =
            transform.forward * speed * -1;
        StartCoroutine(WaitAndDestroy());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.currentMode == GameManager.Mode.Marathon)
        {
            if (collision.gameObject.CompareTag("Terrain"))
            {
                GameManager.isGameOver = true;
            }
        }
        Destroy (gameObject);
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(delay);
        Destroy (gameObject);
    }
}
