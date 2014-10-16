using System;
using UnityEngine;

namespace MainCharacter
{
	public enum ShipColor{BLUE, RED, YELLOW, ORANGE, GREEN, PURPLE};

	public class Form {
		public float cooldown;
		private float originalCooldown;
		public GameObject projectile;
		public float projectileSpeed;
		private float originalSpeed;
		public Material material;
		public float formSpeed;
		public ShipColor shipColor;
		
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

