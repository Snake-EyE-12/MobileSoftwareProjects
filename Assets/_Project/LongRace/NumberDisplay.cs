using TMPro;
using UnityEngine;

public class NumberDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text textAsset;
    [SerializeField] private float fadeOutTime;
    [SerializeField] private AnimationCurve curve;

    private void Update()
    {
        float t = fadeOutTime - (Time.time - timeOfLastUpdate) / fadeOutTime;
        float alpha = Mathf.Clamp(curve.Evaluate(t), 0, 1);
        textAsset.color = new Color(textAsset.color.r, textAsset.color.g, textAsset.color.b, alpha);
    }

    private float timeOfLastUpdate;
    public void SetData(string text, Color color)
    {
        textAsset.color = color;
        textAsset.text = text;
        timeOfLastUpdate = Time.time;
    }
}