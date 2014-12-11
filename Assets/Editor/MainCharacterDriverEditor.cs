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
			driver.absorbSound = (AudioClip)EditorGUILayout.ObjectField ("Absorb Sound",driver.absorbSound, typeof(AudioClip), !EditorUtility.IsPersistent (target));
			driver.bumpSound = (AudioClip)EditorGUILayout.ObjectField ("Bump Sound",driver.bumpSound, typeof(AudioClip), !EditorUtility.IsPersistent (target));
			driver.bulletSound = (AudioClip)EditorGUILayout.ObjectField ("Bullet Sound",driver.bulletSound, typeof(AudioClip), !EditorUtility.IsPersistent (target));
			driver.health = EditorGUILayout.IntField ("Ship Health", driver.health);
			driver.invulnTime = EditorGUILayout.FloatField ("Time of Invulnerability", driver.invulnTime);
			ShowWeaponDropdown ("Red", ref redFold, ref driver.redForm.formSpeed, ref driver.redForm.cooldown, ref driver.redForm.originalCooldown, ref driver.redForm.projectileSpeed, ref driver.redForm.originalSpeed, ref driver.redForm.material, ref driver.redForm.projectile, ref driver.redForm.damage, RedWeaponLayout);
			ShowWeaponDropdown ("Blue", ref blueFold, ref driver.blueForm.formSpeed, ref driver.blueForm.cooldown, ref driver.blueForm.originalCooldown, ref driver.blueForm.projectileSpeed, ref driver.blueForm.originalSpeed, ref driver.blueForm.material, ref driver.blueForm.projectile, ref driver.blueForm.damage, null);
			ShowWeaponDropdown ("Yellow", ref yellowFold, ref driver.yellowForm.formSpeed, ref driver.yellowForm.cooldown, ref driver.yellowForm.originalCooldown, ref driver.yellowForm.projectileSpeed, ref driver.yellowForm.originalSpeed, ref driver.yellowForm.material, ref driver.yellowForm.projectile, ref driver.yellowForm.damage, YellowWeaponLayout);
			ShowWeaponDropdown ("Green", ref greenFold, ref driver.greenForm.formSpeed, ref driver.greenForm.cooldown, ref driver.greenForm.originalCooldown, ref driver.greenForm.projectileSpeed, ref driver.greenForm.originalSpeed, ref driver.greenForm.material, ref driver.greenForm.projectile, ref driver.greenForm.damage, GreenWeaponLayout);
			ShowWeaponDropdown ("Orange", ref orangeFold, ref driver.orangeForm.formSpeed, ref driver.orangeForm.cooldown, ref driver.orangeForm.originalCooldown, ref driver.orangeForm.projectileSpeed, ref driver.orangeForm.originalSpeed, ref driver.orangeForm.material, ref driver.orangeForm.projectile, ref driver.orangeForm.damage, OrangeWeaponLayout);
			ShowWeaponDropdown ("Purple", ref purpleFold, ref driver.purpleForm.formSpeed, ref driver.purpleForm.cooldown, ref driver.purpleForm.originalCooldown, ref driver.purpleForm.projectileSpeed, ref driver.purpleForm.originalSpeed, ref driver.purpleForm.material, ref driver.purpleForm.projectile, ref driver.purpleForm.damage, PurpleWeaponLayout);
			ShowWeaponDropdown ("Rainbow", ref rainbowFold, ref driver.rainbowForm.formSpeed, ref driver.rainbowForm.cooldown, ref driver.rainbowForm.originalCooldown, ref driver.rainbowForm.projectileSpeed, ref driver.rainbowForm.originalSpeed, ref driver.rainbowForm.material, ref driver.rainbowForm.projectile, ref driver.rainbowForm.damage, null);

			if (GUI.changed)EditorUtility.SetDirty (target);
		}

		public void ShowWeaponDropdown(string label, ref bool isFold, ref float shipSpeed, ref float cooldown, ref float originalCooldown, ref float projectileSpeed, ref float originalSpeed, ref Material material, ref GameObject bullet, ref float damage, Action specialWeaponLayout){
			isFold = EditorGUILayout.Foldout(isFold, label);
			if (isFold) {
				EditorGUI.indentLevel++;
					shipSpeed = EditorGUILayout.FloatField ("Ship Speed", shipSpeed);
					cooldown = EditorGUILayout.FloatField ("Cooldown", cooldown);
					originalCooldown = EditorGUILayout.FloatField("Original Cooldown", originalCooldown);
					projectileSpeed = EditorGUILayout.FloatField ("Projectile Speed", projectileSpeed);
					originalSpeed = EditorGUILayout.FloatField("Original Speed", originalSpeed);
					var allowSceneObjects = !EditorUtility.IsPersistent (target);
					material = (Material)EditorGUILayout.ObjectField ("Ship Material", material, typeof(Material), allowSceneObjects);
					bullet = (GameObject)EditorGUILayout.ObjectField ("Primary Bullet", bullet, typeof(GameObject), allowSceneObjects);
					damage = (float)EditorGUILayout.FloatField("Damage", damage);
					if (specialWeaponLayout != null) specialWeaponLayout ();
				EditorGUI.indentLevel--;
			}
		}

		public void PurpleWeaponLayout(){
			var driver = (MainCharacterDriver)target;
			driver.purpleMirv = (GameObject)EditorGUILayout.ObjectField("Mirv Bullet", driver.purpleMirv, typeof(GameObject), !EditorUtility.IsPersistent(target));
			driver.purpleTimeBeforeExplosion = EditorGUILayout.FloatField ("Time before explosion", driver.purpleTimeBeforeExplosion);
			driver.purpleExplosionSize = EditorGUILayout.FloatField ("Mirv Explosion Size", driver.purpleExplosionSize);
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