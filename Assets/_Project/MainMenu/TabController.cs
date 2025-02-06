using UnityEngine;

public class TabController : MonoBehaviour
{
    [SerializeField] private GameObject page;
    public void SetFront()
    {
        page.SetActive(true);
    }

    public void SetBack()
    {
        page.SetActive(false);
    }

    public void ListenToChange(TabController controller)
    {
        if(controller == this) SetFront();
        else SetBack();
    }

    private void Awake()
    {
        SettingsController.OnTabChanged += ListenToChange;
        SetBack();
    }

    private void OnDestroy()
    {
        SettingsController.OnTabChanged -= ListenToChange;
    }
}