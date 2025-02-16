using UnityEngine;
using UnityEngine.UI;

public class Spinner : MonoBehaviour {
    [SerializeField] Image[] imgs;
    [SerializeField] float z;

    void Update() {
        foreach (Image i in imgs) {
            i.rectTransform.rotation = new Quaternion();
        }
    }
}
