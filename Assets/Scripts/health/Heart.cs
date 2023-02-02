using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private float value;

    [SerializeField] private AudioClip heartSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(heartSound);
            collision.GetComponent<Health>().AddHealth(value);
            gameObject.SetActive(false);
        }
    }
}
