using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrigger : MonoBehaviour
{
    [SerializeField] private GameObject spikeToSpawn;
    [SerializeField] private GameObject movingPlatform;

    private bool isSpawned;

    private void Start()
    {
        isSpawned = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isSpawned)
            {
                var SpikeTrigger = Instantiate(spikeToSpawn, transform.position, Quaternion.identity);
                SpikeTrigger.transform.SetParent(movingPlatform.transform);
                isSpawned = true;
            }
        }
    }
}
