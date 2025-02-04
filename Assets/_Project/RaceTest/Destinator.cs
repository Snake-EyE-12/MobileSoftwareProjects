using System.Collections.Generic;
using UnityEngine;

public class Destinator : MonoBehaviour
{
    [SerializeField] private float moveRate;
    private Queue<PathPoint> desiredPositions = new();
    private Vector3 previousEndPos;

    private void Awake()
    {
        previousEndPos = transform.position;
    }

    public void Move(Vector3 finalPosition)
    {
        desiredPositions.Enqueue(new PathPoint() { final = finalPosition, start = previousEndPos });
        previousEndPos = finalPosition;
    }

    private float timeOnQueue = 0;
    private void Update()
    {
        if (desiredPositions.Count > 0)
        {
            timeOnQueue += Time.deltaTime;
            float t = timeOnQueue / moveRate;
            transform.position = Vector3.Lerp(desiredPositions.Peek().start, desiredPositions.Peek().final, t);
            if (t >= 1)
            {
                desiredPositions.Dequeue();
                timeOnQueue = 0;
            }
        }
    }
    struct PathPoint
    {
        public Vector3 final;
        public Vector3 start;
    }
}