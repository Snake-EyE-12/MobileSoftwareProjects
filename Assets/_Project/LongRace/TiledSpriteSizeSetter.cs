using UnityEngine;

public class TiledSpriteSizeSetter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public void SetHeight(float height)
    {
        spriteRenderer.size = new Vector2(spriteRenderer.size.x, height);
    }

    public void SetWidth(float width)
    {
        spriteRenderer.size = new Vector2(width, spriteRenderer.size.y);
    }
}