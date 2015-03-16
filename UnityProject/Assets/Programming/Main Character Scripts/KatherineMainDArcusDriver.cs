//
// Copyright © 2014 Spectrum Studios, All Rights Reserved
//
// THIS SOFTWARE IS PROVIDED BY THE AUTHORS "AS IS" AND ANY EXPRESS OR
// IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
// OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY DIRECT, INDIRECT,
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
// NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//

using UnityEngine;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading;
using MainCharacter;
using InControl;

namespace Spectrum
{
    /// <summary>
    /// GUI Scaling Controller
    /// Revises the scaling of GUI elements based on window size.
    /// </summary>
    public class KatherineMainDArcusDriver : MultiplayerCharacterDriver
    {
        /**********************/
        /** Script Constants **/
        /**********************/
        private const float ALPHA_PER_SEC = 0.1f;

        /**********************/
        /**    Model Data    **/
        /**********************/
        public List<GameObject> colorPieces = new List<GameObject>();
        float currentCooldown = 0;

        public float invulnTime;
        public float invulnCounter = 0;
        bool pause = false;
		public bool canMove = true;

        // Arcus Animator
       // Animator anim;

        // Used for returning to the form we were in before switching to secondary
        public static Form lastPrimaryForm;

        private bool isInSecondary = false;

        public static string arcusName = "";

        //Sounds
        public AudioClip bulletSound;
        public AudioClip bumpSound;
        public AudioClip absorbSound;

        /**********************/
        /**   Initializers   **/
        /**********************/
        // Initialization Code
        new void Start()
        {
			base.Start ();
            // Set the Application target framerate and shut down V-Sync.
            // V-Sync will override targetFrameRate if active, so disable this.
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;

            // Get the name of the GameObject
            arcusName = gameObject.name;

            // Retrieve the Animator
            //anim = GetComponent<Animator>();

            // Retrieve the GUI Components
			GetColorPiecesRecursive (transform);

            // Retrieve Color Form Scripts from this object
            redForm = GetComponent<RedForm>();
            blueForm = GetComponent<BlueForm>();
            yellowForm = GetComponent<YellowForm>();

            // Assign a default Color Form and update ship form
            currentForm = redForm;
            lastPrimaryForm = redForm;
            switchForm(currentForm);

            // Set health and color bars to zero for each round
            health = 100;
            ColorPower.Instance.powerRed = 0;
            ColorPower.Instance.powerBlue = 0;
            ColorPower.Instance.powerYellow = 0;

            // Retrieve and Set the UI Driver based on default Color Form
            uiDriver = gameObject.GetComponent<UIDriver>();
            uiDriver.RotateToRed();
            uiDriver.UpdateBars();

            // Set Game Loss Flag
            lostGame = false;

			MultiplayerCoordinator.Instance.DarcusDriver = this;

			if (ControlScheme.isOneHanded) {
				device = new UnityInputDevice (new KeyboardPlayerTwoAlternateProfile ());
			}else{
				device = new UnityInputDevice (new KeyboardPlayerTwoProfile ());
			}

            if(!CheckForController(2))
            {
                InputManager.AttachDevice(device);
            }
            //foreach(InputDevice device in InputManager.Devices)
            {
              //  Debug.Log("device " + device.Name);
            }
        }

        /**********************/
        /**     Updating     **/
        /**********************/
        // Update is called once per frame
        public void Update()
        {
            // Stop updating if the game has ended
            if (gameOver)
            { return; }

            // Decrease Invulnerability Time left
            invulnCounter -= Time.deltaTime;

            // Handle invulnerability for the ship
            if (invulnCounter > 0)
            {
                // Flicker Effect when undergoing an invincibility timer
                foreach (Renderer obj in GetComponentsInChildren<Renderer>())
                { obj.enabled = !obj.enabled; }
            }
            else
            {
                // Terminate the flicker effect
                foreach (Renderer obj in GetComponentsInChildren<Renderer>())
                { obj.enabled = true; }
            }

            //Get the most recent input device from incontrol
            //Keyboard controls can be represented as an InputDevice using a CustomController

            //Get where to move given user input
            PressMove(device.Direction.X, device.Direction.Y);

            //change the cooldown of the main weapon, as one frame has passed
            currentCooldown -= Time.deltaTime;

            // Handle the return from secondary form to primary
            if (isInSecondary)
            {
                if (((SecondaryForm)currentForm).isDeactivated())
                {
                    switchForm(lastPrimaryForm);
                    isInSecondary = false;
                }
            }

            /**********************/
            /**  Input Handling  **/
            /**********************/
            //
            // Will be updated later with Event Delegate and Input Handler
            //
            if (device.RightTrigger)
            { PressFire(); }

            if (device.Action4)
            { PressYellow(); }

            if (device.Action3)
            { PressBlue(); }

            if (device.Action2)
            { PressRed(); }

            //ZH 3-14: Moved code to actually pause to BackGround UI
            //Not sure what this does though, leaving it
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pause = !pause;
            }
            base.Update();
        }

        public override void PressMove(float horizontal, float vertical)
        {
			if (!canMove) {
				return;
			}
            base.PressMove(horizontal, vertical);
        }

        public void PressFire()
        {
            //FIRE!!!
            if (currentCooldown <= 0)
            {
                currentCooldown = currentForm.getCooldown();
                Fire();
            }
        }

        //Switch to Yellow Form
        public void PressYellow()
        {
            if (!isInSecondary)
            {
                switchForm(yellowForm);
                uiDriver.RotateToYellow();
            }
        }

        //Switch to Red Form
        public void PressRed()
        {
            if (!isInSecondary)
            {
                switchForm(redForm);
                uiDriver.RotateToRed();
            }
        }

        //Switch to Blue Form
        public void PressBlue()
        {
            if (!isInSecondary)
            {
                switchForm(blueForm);
                uiDriver.RotateToBlue();
            }
        }

        public override void UseGreen()
        {
            MultiplayerCoordinator.Instance.UseDefensiveGreen();
        }

        public override void UseOrange()
        {
            MultiplayerCoordinator.Instance.UseDefensiveOrange();
        }

        public override void UsePurple()
        {
            MultiplayerCoordinator.Instance.UseDefensivePurple();
        }

        //Switch to PURPLE FORM
        public override void ActivatePurple()
        {
			if (isInSecondary) {
				return;
			}
			
            switchForm(purpleForm);
            purpleForm.Activate();
            uiDriver.UpdateBars();
			isInSecondary = true;
        }

        //Switch to GREEN FORM
        public override void ActivateGreen()
        {
			if (isInSecondary) {
				return;
			}
			
            //For Green form, we just want to heal and not do stuff
            switchForm(greenForm);
            greenForm.Activate();
            uiDriver.UpdateBars();
            isInSecondary = true;
        }

        //Switch to ORANGE Form
        public override void ActivateOrange()
        {
			if (isInSecondary) {
				return;
			}
			
	        switchForm(orangeForm);
	        orangeForm.Activate();
	        uiDriver.UpdateBars();
			isInSecondary = true;
        }

		public override void PressDefensivePurple(){
			if (isInSecondary) {
				return;
			}
			
			if (GameObject.Find("oArcus") == null) {
				PressPurple();
			}
			
			switchForm (defensePurpleForm);
			defensePurpleForm.Activate ();
			uiDriver.UpdateBars ();
			isInSecondary = true;
		}

		public override void PressDefensiveGreen(){
			if (isInSecondary) {
				return;
			}
			
			switchForm (defenseGreenForm);
			defenseGreenForm.Activate ();
			uiDriver.UpdateBars ();
			isInSecondary = true;
		}

		public override void PressDefensiveOrange(){
			if (isInSecondary) {
				return;
			}
			
			switchForm (defenseOrangeForm);
			defenseOrangeForm.Activate ();
			uiDriver.UpdateBars ();
			isInSecondary = true;
		}

        public void OnCollisionEnter(Collision col)
        {

            // Form.TakeHit() returns true if the bullet cannot be absorbed, else it returns false
            if (/*col.gameObject.layer == LayerMask.NameToLayer("Enemy") ||*/ currentForm.TakeHit(col))
            {

                // Only handle hit if not invulnerable
                if (invulnCounter <= 0)
                {

                    // Set invulnerability
                    invulnCounter = currentForm.shipColor == ShipColor.RAINBOW ? 0 : invulnTime;
                    audio.PlayOneShot(bumpSound);

                    // If in a secondary form, switch back to the previous form
                    /*if (currentForm.shipColor == ShipColor.PURPLE || currentForm.shipColor == ShipColor.ORANGE || currentForm.shipColor == ShipColor.GREEN) {
                        switchForm(previousForm);
				
                    // Only take damage if not in rainbow mode
                    } else */

                    TakeDamage();
                }

                // Absorbed the bullet
            }
            else
            {
                ColorPower.Instance.powerBlue = blueForm.power;
                ColorPower.Instance.powerRed = redForm.power;
                ColorPower.Instance.powerYellow = yellowForm.power; 
				MultiplayerCoordinator.Instance.UpdateUI();
                audio.PlayOneShot(absorbSound);
            }
        }

        void Fire()
        {
            audio.PlayOneShot(bulletSound);
            currentForm.Fire();
        }

        void switchForm(Form form)
        {
            lastPrimaryForm = currentForm;
            currentForm = form;
            for (int i = 0; i < colorPieces.Count; i++)
            {
                colorPieces[i].renderer.material = currentForm.material;
            }
            currentCooldown = currentForm.getCooldown();
            //anim.SetInteger("TransformVar", currentForm.animationNum);
        }

        public void ResetForm()
        {
            switchForm(redForm);
        }

        private void setRedPower(float p)
        {
            redForm.power = p;
            ColorPower.Instance.powerRed = p;
        }

        private void setBluePower(float p)
        {
            blueForm.power = p;
            ColorPower.Instance.powerBlue = p;
        }

        private void setYellowPower(float p)
        {
            yellowForm.power = p;
            ColorPower.Instance.powerYellow = p;
        }

		private void GetColorPiecesRecursive(Transform trans){
            Transform child = trans.GetComponentInChildren<Transform>();
            if (child == null)
            {
                return;
            }
            foreach (Transform t in child)
            {
                if (t.tag == "ArcusColor")
                {
					colorPieces.Add(t.gameObject);
				}
				GetColorPiecesRecursive(t);
			}
		}
    }
}