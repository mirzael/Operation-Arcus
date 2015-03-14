using UnityEngine;
using System.Collections;
using InControl;

public abstract class MultiplayerCharacterDriver : CharacterDriver {
	protected SecondaryForm greenForm;
	protected SecondaryForm orangeForm;
	protected SecondaryForm purpleForm;
	protected SecondaryForm defenseGreenForm;
	protected SecondaryForm defenseOrangeForm;
	protected SecondaryForm defensePurpleForm;

	public new void Start(){
		base.Start ();
		greenForm = GetComponent<GreenForm>();
		orangeForm = GetComponent<OrangeForm>();
		purpleForm = GetComponent<PurpleForm>();

		defenseGreenForm = GetComponent<MPDGreenForm> ();
		defenseOrangeForm = GetComponent<MPDOrangeForm> ();
		defensePurpleForm = GetComponent<MPDPurpleForm> ();
	}

    //Find and assign to device a controller
    //Takes a playerNum (either 1 or 2) as input, and uses either the first or second controller found
    //Returns whether a controller was found this way
    protected bool CheckForController(int playerNum)
    {
        foreach(var aDevice in InputManager.Devices)
        {
            if(!aDevice.Name.ToLower().Contains("keyboard"))
            {
                //found an attached device that is not keyboard
                playerNum -= 1;
                if(playerNum<=0)
                {
                    this.device = aDevice;
                    return true;
                }
            }
        }
        return false;
    }

    //ZH These methods are called from MultiplayerCoordinator
    //  Also ActivateGreen should be called UseOffensiveGreen or somesuch
    public abstract void ActivateGreen();
    public abstract void ActivateOrange();
    public abstract void ActivatePurple();

	public abstract void PressDefensiveGreen ();
	public abstract void PressDefensiveOrange();
	public abstract void PressDefensivePurple();

    protected override void GameOver()
    {
        base.GameOver();
        MultiplayerCoordinator.Instance.GameOver();
    }
}
