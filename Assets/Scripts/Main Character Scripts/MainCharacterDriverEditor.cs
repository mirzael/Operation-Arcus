using System;
using UnityEditor;
using UnityEngine;

namespace MainCharacter
{
	[CustomEditor(typeof(MainCharacterDriver))]
	[CanEditMultipleObjects]
	public class MainCharacterDriverEditor : Editor
	{
		SerializedProperty shipSpeed;
		SerializedProperty timeToWin;
		SerializedProperty forms;

		void OnEnable () {
			// Setup the SerializedProperties
			shipSpeed = serializedObject.FindProperty ("shipSpeed");
			timeToWin = serializedObject.FindProperty ("timeToWin");
			forms = serializedObject.FindProperty ("forms");
		}

		public override void OnInspectorGUI() {
			// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
			serializedObject.Update ();

			EditorGUILayout.FloatField ("Ship Speed", shipSpeed.floatValue);
			EditorGUILayout.FloatField ("Time To Win", timeToWin.floatValue);
			EditorGUILayout.Toggle("Forms size", forms.isArray);
			/*
			// Show the custom GUI controls
			EditorGUILayout.IntSlider (damageProp, 0, 100, new GUIContent ("Damage"));
			// Only show the damage progress bar if all the objects have the same damage value:
			if (!damageProp.hasMultipleDifferentValues)
				ProgressBar (damageProp.intValue / 100.0, "Damage");
			EditorGUILayout.IntSlider (armorProp, 0, 100, new GUIContent ("Armor"));
			// Only show the armor progress bar if all the objects have the same armor value:
			if (!armorProp.hasMultipleDifferentValues)
				ProgressBar (armorProp.intValue / 100.0, "Armor");
			EditorGUILayout.PropertyField (gunProp, new GUIContent ("Gun Object"));
			// Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI. */
			serializedObject.ApplyModifiedProperties ();
		}
	}
}

