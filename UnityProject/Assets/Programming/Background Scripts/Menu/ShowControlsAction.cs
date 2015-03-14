using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ShowControlsAction : MenuAction
{
	public GameObject toHide;
	public GameObject toShowOneHanded;
	public GameObject toShowTwoHanded;
	public Toggle toggle;
	
	public Transform earthModel;
	public Transform smallerModel;
	public Transform largerModel;
	
	public bool makeSmaller;
	
	public override void TakeAction()
	{
		if(toggle.isOn){
			toShowOneHanded.SetActive(true);
		}else{
			toShowTwoHanded.SetActive(true);
		}

		toHide.SetActive(false);
		
		if(earthModel!=null)
		{
			if(makeSmaller)
			{
				earthModel.position = smallerModel.position;
				earthModel.localScale = smallerModel.localScale;
			}
			else
			{
				earthModel.position = largerModel.position;
				earthModel.localScale = largerModel.localScale;
			}
		}
	}
}
