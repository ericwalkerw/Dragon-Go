using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [Header("SpikeHead Atributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private bool attacking;
    private float checkTImer;
    private Vector3 destination;
    private Vector3[] direction = new Vector3[4];

    [SerializeField] private AudioClip spikeHeadSound;
    private void OnEnable()
    {
        Stop();
    }
    private void Update()
    {
        if (attacking)
        {
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else
        {
            checkTImer += Time.deltaTime;
            if (checkTImer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }
    private void CheckForPlayer()
    {
        CaculateDirection();

        for (int i = 0; i < direction.Length; i++)
        {
            Debug.DrawRay(transform.position, direction[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = direction[i];
                checkTImer = 0;
            }
        }
    }

    private void CaculateDirection()
    {
        direction[0] = transform.right * range;
        direction[1] = -transform.right * range;
        direction[2] = transform.up * range;
        direction[3] = -transform.up * range;
    }
    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }
    private new void OnTriggerEnter2D(Collider2D other)
    {
        SoundManager.instance.PlaySound(spikeHeadSound);
        base.OnTriggerEnter2D(other);
        Stop();
    }
}
