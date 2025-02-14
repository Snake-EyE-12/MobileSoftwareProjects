using NaughtyAttributes;
using UnityEngine;

public class OakTree : MonoBehaviour
{
    [SerializeField, MinMaxSlider(0, 30)] private Vector2 scale;
    [SerializeField] private float offsetRadius;

    private void Awake()
    {
        float scale = Random.Range(this.scale.x, this.scale.y);
        int xDirection = (Random.Range(0, 2) == 0) ? 1 : -1;
        transform.localScale = new Vector3(scale * xDirection, scale, scale);
        transform.Translate(Random.insideUnitCircle * offsetRadius);
    }
}