using UnityEditor;
using System;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class DynamicIntegerFieldAttribute : Attribute { }

#if UNITY_EDITOR
[DrawerPriority(0, 0, 1)]
public class DynamicIntegerFieldDrawer : OdinAttributeDrawer<DynamicIntegerFieldAttribute, int>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        EditorGUI.BeginChangeCheck();

        int inputValue = this.ValueEntry.SmartValue;

        bool isMixedType = false;

        // Check if the value is mixed across selected objects
        if (Selection.objects.Length > 1)
        {
            SerializedObject serializedObject = new SerializedObject(Selection.objects);
            SerializedProperty property = serializedObject.FindProperty(this.ValueEntry.Property.Path);
            isMixedType = property.hasMultipleDifferentValues;
        }

        EditorGUI.showMixedValue = isMixedType;

        // Draw the integer input field
        int newValue = EditorGUILayout.IntField(label, inputValue);

        EditorGUI.showMixedValue = false;

        if (EditorGUI.EndChangeCheck())
        {
            this.ValueEntry.SmartValue = Mathf.Max(0, newValue);
        }

        // Store the dynamically generated values in a list
        List<int> additionalValues = new List<int>();
        for (int i = 0; i < inputValue; i++)
        {
            int additionalValue = EditorGUILayout.IntField("Additional Field " + (i + 1), 0);
            additionalValues.Add(additionalValue);
        }

        // Now 'additionalValues' contains the dynamically generated integer values.
        // You can use or store them as needed in your script.
    }
}
#endif
