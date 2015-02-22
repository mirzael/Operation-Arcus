using System;
using UnityEngine;
using MainCharacter;

public enum ShipColor{RED = 1, BLUE, YELLOW, GREEN, ORANGE, PURPLE, RAINBOW};

public abstract class Form : MonoBehaviour {
	public float damage;
	public float cooldown;
	public float originalCooldown;
	public GameObject projectile;
	public float projectileSpeed;
	public float originalSpeed;
	public Material material;
	public float formSpeed;
	public ShipColor shipColor;
	public int animationNum;
	public float power;
	
	protected const float POWER_MAX = 100.0f;
	protected const float POWER_INC = 5.0f;
	protected const int PROJECTILE_DISTANCE = 2;

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
	
	public bool atMaxPower()
	{
		return power >= POWER_MAX;
	}
	
	public virtual void setPower(float amount)
	{
		power = amount;
		if (power < 0) {
			power = 0;
		} else if (power > POWER_MAX) {
			power = POWER_MAX;
		}
	}
	
	public abstract void Fire();
	public abstract bool TakeHit(Collision col);
}
