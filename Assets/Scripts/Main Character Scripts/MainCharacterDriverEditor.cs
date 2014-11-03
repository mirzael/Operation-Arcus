using System;
using UnityEditor;
using UnityEngine;

namespace MainCharacter
{
	[CustomEditor(typeof(MainCharacterDriver))]
	public class MainCharacterDriverEditor : Editor
	{

		public override void OnInspectorGUI() {
			var driver = (MainCharacterDriver)target;
			driver.timeToWin = EditorGUILayout.FloatField ("Time To Win", driver.timeToWin);

			ShowWeaponDropdown ("Red", ref driver.redForm.formSpeed, ref driver.redForm.cooldown, ref driver.redForm.projectileSpeed, ref driver.redForm.material, ref driver.redForm.projectile);
			ShowWeaponDropdown ("Blue", ref driver.blueForm.formSpeed, ref driver.blueForm.cooldown, ref driver.blueForm.projectileSpeed, ref driver.blueForm.material, ref driver.blueForm.projectile);
			ShowWeaponDropdown ("Yellow", ref driver.yellowForm.formSpeed, ref driver.yellowForm.cooldown, ref driver.yellowForm.projectileSpeed, ref driver.yellowForm.material, ref driver.yellowForm.projectile);
			ShowWeaponDropdown ("Green", ref driver.greenForm.formSpeed, ref driver.greenForm.cooldown, ref driver.greenForm.projectileSpeed, ref driver.greenForm.material, ref driver.greenForm.projectile);
			ShowWeaponDropdown ("Orange", ref driver.orangeForm.formSpeed, ref driver.orangeForm.cooldown, ref driver.orangeForm.projectileSpeed, ref driver.orangeForm.material, ref driver.orangeForm.projectile);
			ShowWeaponDropdown ("Purple", ref driver.purpleForm.formSpeed, ref driver.purpleForm.cooldown, ref driver.purpleForm.projectileSpeed, ref driver.purpleForm.material, ref driver.purpleForm.projectile);
			ShowWeaponDropdown ("Rainbow", ref driver.rainbowForm.formSpeed, ref driver.rainbowForm.cooldown, ref driver.rainbowForm.projectileSpeed, ref driver.rainbowForm.material, ref driver.rainbowForm.projectile);

			if (GUI.changed)EditorUtility.SetDirty (target);
		}

		public void ShowWeaponDropdown(string label, ref float shipSpeed, ref float cooldown, ref float projectileSpeed, ref Material material, ref GameObject bullet){
			EditorGUILayout.LabelField (label);
			EditorGUI.indentLevel++;
			shipSpeed = EditorGUILayout.FloatField ("Ship Speed", shipSpeed);
			cooldown = EditorGUILayout.FloatField ("Cooldown", cooldown);
			projectileSpeed = EditorGUILayout.FloatField ("Projectile Speed", projectileSpeed);
			var allowSceneObjects = !EditorUtility.IsPersistent (target);
			material = (Material)EditorGUILayout.ObjectField("Ship Material", material, typeof(Material), allowSceneObjects);
			bullet = (GameObject)EditorGUILayout.ObjectField ("Bullet", bullet, typeof(GameObject), allowSceneObjects);
			EditorGUI.indentLevel--;
		}
	}
}