using System;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private bool isAlive = true;
    public static event Action OnPlayerDeath;
    private static Vector2 respawnPoint = Vector2.zero;
    public static event Action onPlayerReachGoal;

    private bool isShielded = false;
    [SerializeField] private float shieldDuration;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isShielded)
        {
            StopCoroutine(BecomeInvincibleCoroutine());
            StartCoroutine(BecomeInvincibleCoroutine());
        }
    }

    private IEnumerator BecomeInvincibleCoroutine()
    {
        isShielded = true;
        Debug.Log(isShielded);
        yield return new WaitForSeconds(shieldDuration);
        isShielded = false;
        Debug.Log(isShielded);
    }
}
