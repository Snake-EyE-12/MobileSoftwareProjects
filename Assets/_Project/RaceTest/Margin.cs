using UnityEngine;

public class Margin : MonoBehaviour
{
    [SerializeField] private float distance;
    public void SetMarginDistance(float distance) => this.distance = distance;
    private float marginStartX;
    public float SetMarginStartX(float startX)
    {
        marginStartX = startX;
        return startX + distance;
    }

    private void Update()
    {
        transform.position = new Vector3(marginStartX + distance, transform.position.y, transform.position.z);
    }
}