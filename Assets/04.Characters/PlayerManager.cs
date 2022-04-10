using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum Power
    {
        DOUBLE_JUMP,
        INVINCIBLE
    }

    private static Vector2 respawnPoint = Vector2.zero;
    public static event Action OnPlayerDeath;
    public static Power power = Power.DOUBLE_JUMP;

    private void Start()
    {
        transform.position = respawnPoint;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Spike")
        {
            OnPlayerDeath?.Invoke();
        }
        if (other.gameObject.tag == "RespawnPoint")
        {
            respawnPoint = other.gameObject.transform.position;
            respawnPoint.y += 3f;
        }
    }
}
