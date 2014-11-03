using System;
using UnityEngine;

namespace MainCharacter
{
	[System.Serializable]
	public enum ShipColor{BLUE, RED, YELLOW, ORANGE, GREEN, PURPLE, RAINBOW};

	[System.Serializable]
	public class Form {
		[SerializeField]
		public float cooldown;
		[SerializeField]
		private float originalCooldown;
		[SerializeField]
		public GameObject projectile;
		[SerializeField]
		public float projectileSpeed;
		[SerializeField]
		private float originalSpeed;
		[SerializeField]
		public Material material;
		[SerializeField]
		public float formSpeed;
		[SerializeField]
		public ShipColor shipColor;

		public Form(ShipColor color){
			shipColor = color;
		}

		public Form(float formSpeed, float cooldown, GameObject projectile, float projectileSpeed, Material material, ShipColor shipColor){
			this.formSpeed = formSpeed;
			this.cooldown = cooldown;
			this.originalCooldown = cooldown;
			this.projectile = projectile;
			this.projectileSpeed = projectileSpeed;
			this.originalSpeed = projectileSpeed;
			this.material = material;
			this.shipColor = shipColor;
		}

		public float getCooldown()
		{
			return this.cooldown;
		}

		public float getSpeed()
		{
			return this.projectileSpeed;
		}

		public void setCooldown(float cooldown)
		{
			this.cooldown = cooldown;
		}

		public void resetCooldown()
		{
			this.cooldown = this.originalCooldown;
		}

		public void setSpeed(float speed)
		{
			this.projectileSpeed = speed;
		}

		public void resetSpeed()
		{
			this.projectileSpeed = this.originalSpeed;
		}
	}
}

