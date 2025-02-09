using UnityEngine;

public class HorseVisual : MonoBehaviour
{
    [SerializeField] private HorseData data;
    [SerializeField] private float speed = 3;

    private Vector3 targetPosition;
    private void Update()
    {
        //targetPosition = new Vector3(data.GetTotalDistanceTraveled, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}