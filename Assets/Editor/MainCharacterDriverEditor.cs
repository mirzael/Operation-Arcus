using System;
using UnityEditor;
using UnityEngine;

namespace MainCharacter
{
	[CustomEditor(typeof(MainCharacterDriver))]
	public class MainCharacterDriverEditor : Editor
	{
		bool redFold, blueFold, yellowFold, greenFold, orangeFold, purpleFold, rainbowFold;
		public override void OnInspectorGUI() {
			var driver = (MainCharacterDriver)target;
			driver.timeToWin = EditorGUILayout.FloatField ("Time To Win", driver.timeToWin);
			driver.lives = EditorGUILayout.IntField ("Ship Lives", driver.lives);
			driver.invulnTime = EditorGUILayout.FloatField ("Time of Invulnerability", driver.invulnTime);
			ShowWeaponDropdown ("Red", ref redFold, ref driver.redForm.formSpeed, ref driver.redForm.cooldown, ref driver.redForm.projectileSpeed, ref driver.redForm.material, ref driver.redForm.projectile, RedWeaponLayout);
			ShowWeaponDropdown ("Blue", ref blueFold, ref driver.blueForm.formSpeed, ref driver.blueForm.cooldown, ref driver.blueForm.projectileSpeed, ref driver.blueForm.material, ref driver.blueForm.projectile, null);
			ShowWeaponDropdown ("Yellow", ref yellowFold, ref driver.yellowForm.formSpeed, ref driver.yellowForm.cooldown, ref driver.yellowForm.projectileSpeed, ref driver.yellowForm.material, ref driver.yellowForm.projectile, YellowWeaponLayout);
			ShowWeaponDropdown ("Green", ref greenFold, ref driver.greenForm.formSpeed, ref driver.greenForm.cooldown, ref driver.greenForm.projectileSpeed, ref driver.greenForm.material, ref driver.greenForm.projectile, GreenWeaponLayout);
			ShowWeaponDropdown ("Orange", ref orangeFold, ref driver.orangeForm.formSpeed, ref driver.orangeForm.cooldown, ref driver.orangeForm.projectileSpeed, ref driver.orangeForm.material, ref driver.orangeForm.projectile, OrangeWeaponLayout);
			ShowWeaponDropdown ("Purple", ref purpleFold, ref driver.purpleForm.formSpeed, ref driver.purpleForm.cooldown, ref driver.purpleForm.projectileSpeed, ref driver.purpleForm.material, ref driver.purpleForm.projectile, PurpleWeaponLayout);
			ShowWeaponDropdown ("Rainbow", ref rainbowFold, ref driver.rainbowForm.formSpeed, ref driver.rainbowForm.cooldown, ref driver.rainbowForm.projectileSpeed, ref driver.rainbowForm.material, ref driver.rainbowForm.projectile, null);

			if (GUI.changed)EditorUtility.SetDirty (target);
		}

		public void ShowWeaponDropdown(string label, ref bool isFold, ref float shipSpeed, ref float cooldown, ref float projectileSpeed, ref Material material, ref GameObject bullet, Action specialWeaponLayout){
			isFold = EditorGUILayout.Foldout(isFold, label);
			if (isFold) {
				EditorGUI.indentLevel++;
					shipSpeed = EditorGUILayout.FloatField ("Ship Speed", shipSpeed);
					cooldown = EditorGUILayout.FloatField ("Cooldown", cooldown);
					projectileSpeed = EditorGUILayout.FloatField ("Projectile Speed", projectileSpeed);
					var allowSceneObjects = !EditorUtility.IsPersistent (target);
					material = (Material)EditorGUILayout.ObjectField ("Ship Material", material, typeof(Material), allowSceneObjects);
					bullet = (GameObject)EditorGUILayout.ObjectField ("Primary Bullet", bullet, typeof(GameObject), allowSceneObjects);
					if (specialWeaponLayout != null) specialWeaponLayout ();
				EditorGUI.indentLevel--;
			}
		}

		public void PurpleWeaponLayout(){
			var driver = (MainCharacterDriver)target;
			driver.purpleMirv = (GameObject)EditorGUILayout.ObjectField("Mirv Bullet", driver.purpleMirv, typeof(GameObject), !EditorUtility.IsPersistent(target));
			driver.purpleTimeBeforeExplosion = EditorGUILayout.FloatField ("Time before explosion", driver.purpleTimeBeforeExplosion);;
		}	

		public void GreenWeaponLayout(){
			var driver = (MainCharacterDriver)target;
			driver.greenEmpRadius = EditorGUILayout.FloatField ("EMP Radius", driver.greenEmpRadius);
			driver.greenEmpDuration = EditorGUILayout.FloatField ("EMP Duration", driver.greenEmpDuration);
			driver.greenSinAmplitude = EditorGUILayout.FloatField ("Sin Wave Amplitude", driver.greenSinAmplitude);
		}

		public void OrangeWeaponLayout(){
			var driver = (MainCharacterDriver)target;
			driver.orangeRotationSpeed = EditorGUILayout.FloatField ("Rotation Speed", driver.orangeRotationSpeed);
			driver.orangeExplosionRadius = EditorGUILayout.FloatField ("Orange Explosion Radius", driver.orangeExplosionRadius);
			driver.orangeGravityRadius = EditorGUILayout.FloatField ("Orange Gravity Field Radius", driver.orangeGravityRadius);
			driver.orangeGravityForce = EditorGUILayout.FloatField ("Orange Gravity Field Force", driver.orangeGravityForce);
		}

		public void RedWeaponLayout(){
			var driver = (MainCharacterDriver)target;
			driver.redExplosionRadius = EditorGUILayout.FloatField ("Red base explosion radius", driver.redExplosionRadius);
			driver.redRadiusPerPoint = EditorGUILayout.FloatField ("Red radius/point increase", driver.redRadiusPerPoint);
		}

		public void YellowWeaponLayout(){
			var driver = (MainCharacterDriver)target;
			driver.yellowPointsPerBullet = EditorGUILayout.FloatField ("Yellow Points/Bullet", driver.yellowPointsPerBullet);
		}
	}
}