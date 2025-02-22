using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour {
    [SerializeField] RawImage img;
    [SerializeField] float _x;

    void Update() {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(_x, 0) * Time.deltaTime, img.uvRect.size);   
    }
}