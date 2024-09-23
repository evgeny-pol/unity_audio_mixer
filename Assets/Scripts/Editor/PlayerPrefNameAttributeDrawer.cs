using UnityEditor;

[CustomPropertyDrawer(typeof(PlayerPrefNameAttribute))]
public class PlayerPrefNameAttributeDrawer : StringAttributePopupDrawer
{
    private static readonly string[] s_values = new string[]
    {
        PlayerPrefName.SoundsEnabled,
        PlayerPrefName.GeneralVolume,
        PlayerPrefName.UIVolume,
        PlayerPrefName.MusicVolume
    };

    protected override string[] Values => s_values;
}
