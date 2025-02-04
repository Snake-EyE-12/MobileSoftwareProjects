using UnityEngine;

public class Margin : MonoBehaviour
{
    [SerializeField] private float distance;
    public void SetMarginDistance(float distance) => this.distance = distance;
    private float marginStartX;
    public void SetMarginStartX(float startX) => marginStartX = startX;

    private void Update()
    {
        transform.position = new Vector3(marginStartX + distance, transform.position.y, transform.position.z);
    }
}