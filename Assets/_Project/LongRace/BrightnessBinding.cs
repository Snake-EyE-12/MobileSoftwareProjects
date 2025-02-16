using System;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessBinding : MonoBehaviour
{
    [SerializeField] private Image panel;

    private float alpha;
    private void Awake()
    {
        alpha = panel.color.a;
    }

    private void Update()
    {
        float newAlpha = alpha * (1 - SettingsDataBinding.BrightnessValue);
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, newAlpha);
        Debug.Log("NEW ALPHA: " + newAlpha);
        Debug.Log("BV: " + SettingsDataBinding.BrightnessValue);
    }
}
