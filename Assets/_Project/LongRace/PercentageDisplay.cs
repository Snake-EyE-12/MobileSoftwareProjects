using System;
using UnityEngine;

public class PercentageDisplay : MonoBehaviour
{
    [SerializeField] private Transform beginningPoint;
    [SerializeField] private Transform endPoint;
    private void OnEnable()
    {
        RaceController.OnRaceStart += InitializeIcons;
    }

    private void OnDisable()
    {
        RaceController.OnRaceStart -= InitializeIcons;
    }

    [SerializeField] private HorseIcon iconPrefab;
    public void InitializeIcons()
    {
        foreach (var horse in HorseController.horses)
        {
            HorseIcon icon = Instantiate(iconPrefab, transform);
            icon.Initialize(horse, beginningPoint, endPoint);
        }
    }
}