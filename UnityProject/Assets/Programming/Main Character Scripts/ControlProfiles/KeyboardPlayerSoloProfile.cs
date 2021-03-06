using System;
using System.Collections;
using UnityEngine;
using InControl;


// This custom profile is enabled by adding it to the Custom Profiles list
// on the InControlManager component, or you can attach it yourself like so:
// InputManager.AttachDevice( new UnityInputDevice( "KeyboardProfile" ) );
// 

 public class KeyboardPlayerSoloProfile : UnityInputDeviceProfile
 {
     public KeyboardPlayerSoloProfile()
     {
         Name = "Solo Keyboard";
         Meta = "A keyboard and mouse combination for op arcus on its own.";

         // This profile only works on desktops.
         SupportedPlatforms = new[]
 		{
		    "Windows",
		    "Mac",
		    "Linux"
    	};

         Sensitivity = 1.0f;
         LowerDeadZone = 0.0f;
         UpperDeadZone = 1.0f;

         ButtonMappings = new[]
	    {
		    new InputControlMapping
		    {
			    Handle = "Fire",
			    Target = InputControlType.RightTrigger,
			    // KeyCodeButton fires when any of the provided KeyCode params are down.
			    Source = KeyCodeButton( KeyCode.Space )
		    },
		    new InputControlMapping
		    {
			    Handle = "Yellow",
			    Target = InputControlType.Action4,
			    Source = KeyCodeButton( KeyCode.K )
		    },
		    new InputControlMapping
		    {
			    Handle = "Red",
			    Target = InputControlType.Action2,
			    // KeyCodeComboButton requires that all KeyCode params are down simultaneously.
			    Source = KeyCodeButton( KeyCode.J )
		    },
		    new InputControlMapping
		    {
			    Handle = "Blue",
			    Target = InputControlType.Action3,
			    Source = KeyCodeButton( KeyCode.L )
		    },
		    new InputControlMapping
		    {
			    Handle = "Green",
			    Target = InputControlType.RightBumper,
			    Source = KeyCodeButton( KeyCode.I)
		    },
		    new InputControlMapping
		    {
			    Handle = "Purple",
			    Target = InputControlType.LeftTrigger,
			    Source = KeyCodeButton( KeyCode.O)
		    },
		    new InputControlMapping
		    {
			    Handle = "Orange",
			    Target = InputControlType.LeftBumper,
			    Source = KeyCodeButton( KeyCode.U)
		    }
	    };

         AnalogMappings = new[]
	    {
		    new InputControlMapping
		    {
			    Handle = "Move X",
			    Target = InputControlType.LeftStickX,
			    // KeyCodeAxis splits the two KeyCodes over an axis. The first is negative, the second positive.
			    Source = KeyCodeAxis( KeyCode.A, KeyCode.D )
		    },
		    new InputControlMapping
		    {
			    Handle = "Move Y",
			    Target = InputControlType.LeftStickY,
			    // Notes that up is positive in Unity, therefore the order of KeyCodes is down, up.
			    Source = KeyCodeAxis( KeyCode.S, KeyCode.W )
		    }
	    };
     }
 }

