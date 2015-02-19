using UnityEngine;
using System.Collections;

public abstract class MultiplayerCharacterDriver : CharacterDriver {
	protected SecondaryForm greenForm;
	protected SecondaryForm orangeForm;
	protected SecondaryForm purpleForm;
	protected SecondaryForm defenseGreenForm;
	protected SecondaryForm defenseOrangeForm;
	protected SecondaryForm defensePurpleForm;

	public void Start(){
		base.Start ();
		greenForm = GetComponent<GreenForm>();
		orangeForm = GetComponent<OrangeForm>();
		purpleForm = GetComponent<PurpleForm>();

		defenseGreenForm = GetComponent<MPDGreenForm> ();
		defenseOrangeForm = GetComponent<MPDOrangeForm> ();
		defensePurpleForm = GetComponent<MPDPurpleForm> ();
	}

	public abstract void PressDefensiveGreen ();
	public abstract void PressDefensiveOrange();
	public abstract void PressDefensivePurple();
}
