using UnityEngine;

public class ShowIfEnumAttribute : PropertyAttribute
{
    public readonly string EnumPropertyName;
    public readonly int Enum;

    public ShowIfEnumAttribute(string name, int _enum)
    {
        EnumPropertyName = name;
        Enum = _enum;
    }
}
