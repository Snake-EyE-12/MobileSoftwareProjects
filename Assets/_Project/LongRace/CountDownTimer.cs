using System;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] private RaceController controller;
    [SerializeField, Min(1)] private int startNumber;

    [SerializeField] private NumberDisplay numberDisplay;


    private float timeOfNextNumber;
    private int currentNumber;


    private void Awake()
    {
        currentNumber = startNumber;
    }

    private void Update()
    {
        if (timeOfNextNumber > Time.time) return;
        timeOfNextNumber = Time.time + 1;
        numberDisplay.SetData(GetDisplayValue(currentNumber), GetDisplayColor(currentNumber));
        if (currentNumber == 0)
        {
            this.enabled = false;
        }
        currentNumber--;
    }

    private string GetDisplayValue(int value)
    {
        if (value == 0)
        {
            return "GO!";
        }
        return value.ToString();
    }
    private Color GetDisplayColor(int value)
    {
        return Color.Lerp(Color.green, Color.red, value / (float)startNumber);
    }

    private void OnDisable()
    {
        controller.BeginRace();
    }
}