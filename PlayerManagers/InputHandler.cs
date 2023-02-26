using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    public class InputHandler : MonoBehaviour
    {
        [HideInInspector] public float horizontal;
        [HideInInspector] public float vertical;
        [HideInInspector] public float moveAmount;
        [HideInInspector] public float mouseX;
        [HideInInspector] public float mouseY;

        PlayerControls inputActions;
        PlayerManager playerManager;
        UIManager uiManager;

        Vector2 movementInput;
        Vector2 cameraInput;

        public Transform criticalAttackRaycastStartPoint;

        //controller inputs/keyboard inputs
        [HideInInspector] public bool b_input;
        [HideInInspector] public bool RB_input;
        [HideInInspector] public bool LB_input;
        [HideInInspector] public bool RT_input;
        [HideInInspector] public bool LT_input;
        [HideInInspector] public bool a_input;
        [HideInInspector] public bool x_input;
        [HideInInspector] public bool jump_input;
        [HideInInspector] public bool inventory_input;
        [HideInInspector] public bool y_input;
        [HideInInspector] public bool d_pad_up;
        [HideInInspector] public bool d_pad_down;
        [HideInInspector] public bool d_pad_left;
        [HideInInspector] public bool d_pad_right;
        [HideInInspector] public bool lockOn_Input;
        [HideInInspector] public bool right_Stick_Right_input;
        [HideInInspector] public bool right_Stick_Left_input;
        [HideInInspector] public bool critical_attack_input;



        [HideInInspector] public bool rollFlag;
        [HideInInspector] public bool sprintFlag;
        [HideInInspector] public bool comboFlag;
        [HideInInspector] public bool inventory_flag;
        [HideInInspector] public bool TwoHand_Flag;
        [HideInInspector] public bool lockOn_flag;
        [HideInInspector] public float rollInputTimer;

        // Start is called before the first frame update
        private void Awake()
        {
           
            playerManager = GetComponent<PlayerManager>();
            uiManager = FindObjectOfType<UIManager>();

            
        }

        private void OnEnable()
        {
            if(inputActions == null){
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
                inputActions.PlayerActions.RB.performed += i => RB_input = true;

                inputActions.PlayerActions.LB.performed += i => LB_input = true;
                inputActions.PlayerActions.LB.canceled += i => LB_input = false;

                inputActions.PlayerActions.RT.performed += i => RT_input = true;
                inputActions.PlayerActions.LT.performed += i => LT_input = true;
                inputActions.PlayerMovement.DPadRight.performed += i => d_pad_right = true;
                inputActions.PlayerMovement.DPadLeft.performed += i => d_pad_left = true;
                inputActions.PlayerMovement.DPadUp.performed += i => d_pad_up = true;
                inputActions.PlayerMovement.DPadDown.performed += i => d_pad_down = true;
                inputActions.PlayerActions.A.performed += i => a_input = true;
                inputActions.PlayerActions.X.performed += i => x_input = true;
                inputActions.PlayerActions.Jump.performed += i => jump_input = true;
                inputActions.PlayerActions.Inventory.performed += i => inventory_input = true;
                inputActions.PlayerActions.Y.performed += i => y_input = true;
                inputActions.PlayerActions.CriticalAttack.performed += i => critical_attack_input = true;

               inputActions.PlayerActions.Roll.performed += i => b_input = true;
               inputActions.PlayerActions.Roll.canceled += i => b_input = false;

            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput()
        {
            if (playerManager.stats.isDead) return;

            MoveInput();
            HandleRollingInput();

            HandleCriticalAttackInput();
            HandleHoldLBInput();

            HandleTapRBInput();
            HandleTapLBInput();
        
            HandleTapRTInput();
            HandleTapLTInput();

           

            HandleQuickSlotsInput();
            HandleInventoryInput();

            HandleTwoHandInput();
            
            HandleUseConsumableInput();
         //   HandleAInput();
        }

        #region Handle Different Types of input
        private void MoveInput()
        {
            if (inventory_flag) return;
            if (uiManager.levelUpWindow.activeSelf == true)return;
            

            if (movementInput.x >= 0.5)
                horizontal = 1.0f;
            else if (movementInput.x < 0.5 && movementInput.x >0.2 )
                horizontal = 0.5f;

            else if (movementInput.x <= 0.2 && movementInput.x >= -0.2)
                horizontal = 0.0f;
            else if (movementInput.x < -0.2 && movementInput.x > -0.5)
                horizontal = -0.5f;
            else if (movementInput.x <= -0.5)
                horizontal = -1.0f;

            else
                horizontal = 0.0f;

            if (movementInput.y >= 0.5)
                vertical = 1.0f;
            else if (movementInput.y < 0.5 && movementInput.y > 0.2 )
                vertical = 0.5f;
            else if (movementInput.y <= 0.2 && movementInput.y >= -0.2)
                vertical = 0.0f;
            else if (movementInput.y < -0.2 && movementInput.y > -0.5)
                vertical = -0.5f;
            else if (movementInput.y <= -0.5)
                vertical = -1.0f;



            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + (Mathf.Abs(vertical)));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }


        private void LockOnInput(){
            if(lockOn_Input){
                lockOn_flag = !lockOn_flag;
            }
    
        }

        private void HandleRollingInput()
        {


            if (inventory_flag) {return;}
            if (uiManager.levelUpWindow.activeSelf == true)return;
            if (b_input)
            {
                rollInputTimer += Time.deltaTime;

                if (playerManager.stats.currentStamina<=0)
                {
                    b_input = false;
                    sprintFlag = false;
                }
                if(moveAmount >0 && playerManager.stats.currentStamina>0)
                {
                    sprintFlag = true;
                }
            }
            else
            {
                sprintFlag = false;
                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    rollFlag = true;

                }
                rollInputTimer = 0;
            }
        }

        private void HandleTapRBInput()
        {
            if (inventory_flag) return;
            if (uiManager.levelUpWindow.activeSelf == true)return;

            //right hand inputs
            if (RB_input)
            {

                playerManager.ResetHandBeingUsed(false);
                if (playerManager.inventory.rightWeapon.Tap_RB_action != null)
                    playerManager.inventory.rightWeapon.Tap_RB_action.PerformAction(playerManager);
            }

        }
        private void HandleTapLBInput()
        {
            if (inventory_flag) return;
            if (uiManager.levelUpWindow.activeSelf == true)return;

            //right hand inputs
            if (LB_input)
            {

                playerManager.ResetHandBeingUsed(true);
                if (playerManager.inventory.leftWeapon.Tap_LB_action != null)
                    playerManager.inventory.leftWeapon.Tap_LB_action.PerformAction(playerManager);
            }

        }

        private void HandleTapRTInput()
        {
            if (inventory_flag) return;
            if (uiManager.levelUpWindow.activeSelf == true)return;

            if (RT_input)
            {

                playerManager.ResetHandBeingUsed(false);
                if (playerManager.inventory.rightWeapon.Tap_RT_action != null)
                    playerManager.inventory.rightWeapon.Tap_RT_action.PerformAction(playerManager);
            }
    
        }

        private void HandleTapLTInput()
        {
            if (inventory_flag) return;
            if (uiManager.levelUpWindow.activeSelf == true)return;

            if (LT_input)
            {
                if (TwoHand_Flag)
                {
                    playerManager.ResetHandBeingUsed(false);
                    if (playerManager.inventory.rightWeapon.Tap_LT_action != null)
                        playerManager.inventory.rightWeapon.Tap_LT_action.PerformAction(playerManager);
                }
                else
                {
                    playerManager.ResetHandBeingUsed(true);
                    if (playerManager.inventory.leftWeapon.Tap_LT_action != null)
                        playerManager.inventory.leftWeapon.Tap_LT_action.PerformAction(playerManager);

                }

            }
        }
        private void HandleHoldLBInput() 
        {
            if (inventory_flag) return;
            if (uiManager.levelUpWindow.activeSelf == true)return;
            //left hand inputs
            if (playerManager.isInAir || playerManager.isSprinting|| playerManager.isFiringSpell)
            {
                LB_input = false;
                return;
            }

            if (LB_input)
            {
                if (TwoHand_Flag)
                {
                    playerManager.ResetHandBeingUsed(false);
                    if (playerManager.inventory.rightWeapon.hold_LB_action != null)
                        playerManager.inventory.rightWeapon.hold_LB_action.PerformAction(playerManager);
                }
                else
                {
                    playerManager.ResetHandBeingUsed(true);
                    if (playerManager.inventory.leftWeapon.hold_LB_action != null)
                        playerManager.inventory.leftWeapon.hold_LB_action.PerformAction(playerManager);

                }
                ;
            }

            else
            {
                playerManager.ResetHandBeingUsed(false);

                playerManager.isBlocking = false;
                playerManager.equipmentManager.CloseBlockingCollider();
            }

            
        }

        //Handle Hold Rb input
        private void HandleCriticalAttackInput()
        {
            if (inventory_flag) return;
            if (uiManager.levelUpWindow.activeSelf == true)return;

            if (critical_attack_input)
            {
                playerManager.ResetHandBeingUsed(false);
                if (playerManager.inventory.rightWeapon.hold_RB_action!= null)
                    playerManager.inventory.rightWeapon.hold_RB_action.PerformAction(playerManager);
                critical_attack_input = false;
            }
        }



        private void HandleQuickSlotsInput()
        {
            if (inventory_flag) return;
            if (uiManager.levelUpWindow.activeSelf == true)return;

            if (d_pad_right)
            {
                if (TwoHand_Flag)
                {
                    TwoHand_Flag = false;
                    playerManager.weaponSlotManager.LoadWeaponOnSlot(playerManager.inventory.leftWeapon, true);

                }
                playerManager.inventory.ChangeRightWeapon();
            }
            if (d_pad_left)
            {
                if (TwoHand_Flag)
                {
                    TwoHand_Flag = false;
                    playerManager.weaponSlotManager.LoadWeaponOnSlot(playerManager.inventory.rightWeapon, false);

                }
                playerManager.inventory.ChangeLeftWeapon();
            }
            if (d_pad_up)
            {
                playerManager.inventory.ChangeCurrentSpell();
            }
            if (d_pad_down)
            {
                playerManager.inventory.ChangeCurrentConsumable();
            }
        }

        private void HandleInventoryInput() 
        {
            if (uiManager.levelUpWindow.activeSelf == true)return;

            if (uiManager.hudWindow.activeSelf == true) inventory_flag = false;
            if (inventory_flag) uiManager.UpdateUI();
            if (inventory_input)
            {
                inventory_flag = !inventory_flag;
                if (inventory_flag)
                {
                    uiManager.hudWindow.SetActive(false);
                    uiManager.OpenSelectWindow();
                    //uiManager.GetComponent<MainMenu>().set_menu_item(uiManager.firstButton);
                    uiManager.UpdateUI();
                    
                }
                else
                {
                    uiManager.CloseSelectWindow();
                    uiManager.CloseAllInventoryWindows();
                    uiManager.hudWindow.SetActive(true);
                    uiManager.GetComponent<MainMenu>().set_menu_item(null);
                }

            }
        }

        

        private void HandleTwoHandInput()
        {
            if (inventory_flag) return;
            if (uiManager.levelUpWindow.activeSelf == true)return;

            if (y_input)
            {
                y_input = false;
                TwoHand_Flag = !TwoHand_Flag;
                if (TwoHand_Flag)
                {
                    playerManager.weaponSlotManager.LoadWeaponOnSlot(playerManager.inventory.rightWeapon,false);
                }
                else
                {
                    playerManager.weaponSlotManager.LoadWeaponOnSlot(playerManager.inventory.leftWeapon, true);
                    playerManager.weaponSlotManager.LoadWeaponOnSlot(playerManager.inventory.rightWeapon, false);
                }
            }
        }




        private void HandleUseConsumableInput()
        {
            if (inventory_flag) return;
            if (uiManager.levelUpWindow.activeSelf == true)return;
            if (x_input)
            {
                x_input = false;
                if (playerManager.inventory.currentConsumable.currentItemAmount>0)
                    //use consumable item
                    playerManager.inventory.currentConsumable.AttemptToConsumeItem(playerManager.animatorManager, playerManager.weaponSlotManager, playerManager.effectsManager, TwoHand_Flag);
            }
        }

        #endregion


        private void HandleLockOn()
        {
            if(!lockOn_flag) return;

        }

    }
}
