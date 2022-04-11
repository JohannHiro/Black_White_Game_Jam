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
    public static event Action OnPlayerDeath;
    private static Vector2 respawnPoint = Vector2.zero;
    public static event Action onPlayerReachGoal;

    public static Power power = Power.DOUBLE_JUMP;
    private bool isShielded = false;
    private bool canShield = true;
    [SerializeField] private float shieldDuration;
    [SerializeField] private float shieldCooldown;

    private void OnEnable()
    {
        OnPlayerDeath += ChangePower;
    }

    private void OnDisable()
    {
        OnPlayerDeath -= ChangePower;
    }

    private void Start()
    {
        transform.position = respawnPoint;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && canShield && !isShielded && power == Power.SHIELD)
        {
            StopCoroutine(BecomeInvincibleCoroutine());
            StartCoroutine(BecomeInvincibleCoroutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "RespawnPoint")
        {
            respawnPoint = other.gameObject.transform.position;
            respawnPoint.y += 3f;
        }
        if (other.gameObject.tag == "Goal")
        {
            onPlayerReachGoal?.Invoke();
            respawnPoint = Vector2.zero;
        }
        if (other.gameObject.tag == "DeathZone")
        {
            OnPlayerDeath?.Invoke();
            isAlive = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Spike" && !isShielded && isAlive)
        {
            OnPlayerDeath?.Invoke();
            isAlive = false;
        }
    }

    private IEnumerator BecomeInvincibleCoroutine()
    {
        isShielded = true;
        canShield = false;
        Debug.Log("Shield activate");
        yield return new WaitForSeconds(shieldDuration);
        isShielded = false;
        Debug.Log("Shield close");
        yield return new WaitForSeconds(shieldCooldown);
        canShield = true;
        Debug.Log("Can shield");
    }

    private void ChangePower()
    {
        switch (power)
        {
            case Power.DOUBLE_JUMP:
                power = Power.SHIELD;
                break;
            case Power.SHIELD:
                power = Power.DOUBLE_JUMP;
                break;
        }

        Debug.Log(power);
    }
}
