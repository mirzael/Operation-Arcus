using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ChangeControlsAction : MenuAction
{
	public Toggle toggle;
	
	public override void TakeAction()
	{
		ControlScheme.isOneHanded = toggle.isOn;
	}
}
