using System;
using UnityEngine;

public class SpringPad : MonoBehaviour
{
    [SerializeField] private int uses;
    private int activationCount;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activationCount >= uses) return;
        if (other.transform.parent.TryGetComponent(out HorseController controller))
        {
            activationCount++;
            Activate(controller);
        }
    }

    private void Activate(HorseController horse)
    {
        horse.SpringForward();
    }
}
