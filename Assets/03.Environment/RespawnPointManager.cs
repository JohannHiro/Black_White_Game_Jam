using UnityEngine;

public class RespawnPointManager : MonoBehaviour
{
    [SerializeField] private Sprite inactivatedRespawnPoint;
    [SerializeField] private Sprite activatedRespawnPoint;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (gameObject.name == GameManager.Instance.currentRespawnPointName)
            spriteRenderer.sprite = activatedRespawnPoint;
        else
            spriteRenderer.sprite = inactivatedRespawnPoint;
    }
}
