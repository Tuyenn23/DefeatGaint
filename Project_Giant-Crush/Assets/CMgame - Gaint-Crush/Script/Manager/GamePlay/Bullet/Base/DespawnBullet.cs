using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnBullet : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DeSpawnBullet());
    }


    IEnumerator DeSpawnBullet()
    {
        yield return new WaitForSeconds(0.2f);
        
        SimplePool.Despawn(gameObject);
    }
}
