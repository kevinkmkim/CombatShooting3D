using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItem : MonoBehaviour
{
    private float delay = 2.0f;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.currentMode == GameManager.Mode.Marathon)
        {
            if (collision.gameObject.CompareTag("Terrain"))
            {
                GameManager.isGameOver = true;
            }
        }
        Destroy(gameObject);
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
