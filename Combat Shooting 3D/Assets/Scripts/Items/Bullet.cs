using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Bullet")]
public class Bullet : ScriptableObject
{
    public GameObject bulletPrefab;
    public float speed;
}


// private void OnCollisionEnter(Collision collision)
// {
//     if (GameManager.currentMode == GameManager.Mode.Marathon)
//     {
//         if (collision.gameObject.CompareTag("Terrain"))
//         {
//             GameManager.isGameOver = true;
//         }
//     }
//     Destroy (gameObject);
// }

// IEnumerator WaitAndDestroy()
// {
//     yield return new WaitForSeconds(delay);
//     Destroy (gameObject);
// }