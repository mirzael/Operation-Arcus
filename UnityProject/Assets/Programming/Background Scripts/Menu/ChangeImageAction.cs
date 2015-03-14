using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageAction : MenuAction
{
	public Image image;
	public Sprite spriteToChange;

	
	public override void TakeAction()
	{
		image.sprite = spriteToChange;
	}
}
