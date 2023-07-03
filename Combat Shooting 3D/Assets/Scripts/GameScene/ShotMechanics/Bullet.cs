using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float delay = 5f;

    private float speed = 500f;

    void Start()
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
                // Collision with terrain detected
                // Debug.Log("Collision with terrain detected");
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
