//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;

namespace MainCharacter
{
		public class WeaponDamage
		{
			public string tag{ get; set; }
			public float damage{ get; set; }
			public Vector3 hitLocation { get; set ; }

			public WeaponDamage(){}
			public WeaponDamage (string tag, float damage, Vector3 hitLocation)
			{
				this.tag = tag;
				this.damage = damage;
				this.hitLocation = hitLocation;
			}
		}
}

