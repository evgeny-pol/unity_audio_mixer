using UnityEditor;

[CustomPropertyDrawer(typeof(AudioParamNameAttribute))]
public class AudioParamNameAttributeDrawer : StringAttributePopupDrawer
{
    private static readonly string[] s_values = new string[]
    {
        AudioParamName.MasterVolume,
        AudioParamName.GeneralVolume,
        AudioParamName.MusicVolume,
        AudioParamName.UIVolume
    };

    protected override string[] Values => s_values;
}
