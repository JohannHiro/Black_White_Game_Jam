using UnityEngine;

public class RespawnPointManager : MonoBehaviour
{
    [SerializeField] private Sprite activatedRespawnPoint;
    private SpriteRenderer spriteRenderer;
    private static bool isActivated = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (isActivated)
            spriteRenderer.sprite = activatedRespawnPoint;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !isActivated)
        {
            spriteRenderer.sprite = activatedRespawnPoint;
            isActivated = true;
        }
    }
}
