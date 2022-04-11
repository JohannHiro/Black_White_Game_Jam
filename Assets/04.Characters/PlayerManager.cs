using System;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum Power
    {
        DOUBLE_JUMP,
        SHIELD
    }

    private bool isAlive = true;
    private static Vector2 respawnPoint = Vector2.zero;
    public static event Action OnPlayerDeath;
    public static Power power = Power.DOUBLE_JUMP;
    private bool isInvincible = false;
    [SerializeField] private float invincibilityDuration;

    private void Start()
    {
        transform.position = respawnPoint;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "RespawnPoint")
        {
            respawnPoint = other.gameObject.transform.position;
            respawnPoint.y += 3f;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Spike" && !isInvincible && isAlive)
        {
            OnPlayerDeath?.Invoke();
            isAlive = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isInvincible)
        {
            StopCoroutine(BecomeInvincibleCoroutine());
            StartCoroutine(BecomeInvincibleCoroutine());
        }
    }

    private IEnumerator BecomeInvincibleCoroutine()
    {
        isInvincible = true;
        Debug.Log(isInvincible);
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
        Debug.Log(isInvincible);
    }
}
