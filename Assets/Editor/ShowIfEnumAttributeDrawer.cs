using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ShowIfEnumAttribute))]
public class ShowIfEnumAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attribute = (ShowIfEnumAttribute)base.attribute;
        var enumProperty = property.serializedObject.FindProperty(attribute.EnumPropertyName);
        //Debug.Log("Outside");
        if (enumProperty != null && enumProperty.propertyType == SerializedPropertyType.Enum)
        {
            Debug.Log("Inside1");
            if (attribute.Enum.Equals(enumProperty.intValue))
            {
                Debug.Log("Inside2");
                return;
            }
        }
        EditorGUI.PropertyField(position, property, label, true);
    }
}
