using UnityEngine;

public class InstantiateTransform : MonoBehaviour
{
    public static InstantiateTransform instance;
    public GameObject SpawnObject;

    private void Awake() { instance = this; }
}
