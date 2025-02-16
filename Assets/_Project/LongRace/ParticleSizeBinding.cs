using System;
using UnityEngine;

public class ParticleSizeBinding : MonoBehaviour
{
    private Vector3 startingScale;
    private void Awake()
    {
        startingScale = transform.localScale;
    }

    private void Update()
    {
        Vector3 updatedScale = startingScale * SettingsDataBinding.ParticleSizeValue;
        transform.localScale = updatedScale;
    }
}
