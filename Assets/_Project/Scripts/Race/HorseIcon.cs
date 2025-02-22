using UnityEngine;
using UnityEngine.UI;

public class HorseIcon : MonoBehaviour
{
    private HorseController attachedHorse;
    private Transform start;
    private Transform end;
    [SerializeField] private Image image;
    [SerializeField] private Sprite playerImage;
    public void Initialize(HorseController horse, Transform start, Transform end)
    {
        attachedHorse = horse;
        this.start = start;
        this.end = end;
        if (horse.IsPlayer)
        {
            image.sprite = playerImage;
            image.transform.localScale *= (1.3f);
        }
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(start.position, end.position, attachedHorse.PercentDistanceTraveled);
    }
}