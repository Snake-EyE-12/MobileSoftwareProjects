using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float targetAngle;
    [SerializeField] private float rotationSpeed;
    private bool isOpen;
    public void Open()
    {
        isOpen = true;
    }

    private void Update()
    {
        if(!isOpen) return;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, targetAngle), rotationSpeed * Time.deltaTime);
    }
}