using System;
using UnityEngine;

namespace MainCharacter
{
	public class Form {
		public float cooldown;
		public GameObject projectile;
		public float projectileSpeed;
		public Material material;
		public float formSpeed;
		
		public Form(float formSpeed, float cooldown, GameObject projectile, float projectileSpeed, Material material){
			this.formSpeed = formSpeed;
			this.cooldown = cooldown;
			this.projectile = projectile;
			this.projectileSpeed = projectileSpeed;
			this.material = material;
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

		public void setSpeed(float speed)
		{
			this.projectileSpeed = speed;
		}
	}
}

