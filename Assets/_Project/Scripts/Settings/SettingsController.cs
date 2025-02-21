using System;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    public delegate void TabChangedEvent(TabController tab);
    public static event TabChangedEvent OnTabChanged;
    
    
    [SerializeField] private TabController appearanceTab;
    [SerializeField] private TabController audioTab;
    [SerializeField] private TabController statsTab;
    [SerializeField] private TabController controlsTab;

    [SerializeField] private GameObject menu;


    public void ToggleMenuVisibility()
    {
        menu.SetActive(!menu.activeSelf);
        if (menu.activeSelf)
        {
            ChangeToAppearanceTab();
        }
    }
    
    public void ChangeSettingsTab(SettingsTab tabType)
    {
        OnTabChanged.Invoke(TabTypeToController(tabType));
    }
    public void ChangeToAppearanceTab() => ChangeSettingsTab(SettingsTab.APPEARANCE);
    public void ChangeToAudioTab() => ChangeSettingsTab(SettingsTab.AUDIO);
    public void ChangeToStatsTab() => ChangeSettingsTab(SettingsTab.STATS);
    public void ChangeToControlsTab() => ChangeSettingsTab(SettingsTab.CONTROLS);

    private TabController TabTypeToController(SettingsTab tabType)
    {
        switch (tabType)
        {
            case SettingsTab.APPEARANCE:
                return appearanceTab;
            case SettingsTab.AUDIO:
                return audioTab;
            case SettingsTab.STATS:
                return statsTab;
            case SettingsTab.CONTROLS:
                return controlsTab;
            default:
                return null;
        }
    }
}

public enum SettingsTab
{
    APPEARANCE,
    AUDIO,
    STATS,
    CONTROLS
}