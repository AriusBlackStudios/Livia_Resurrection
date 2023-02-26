using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using Cinemachine;

namespace Livia {
    public class PlayerManager : CharacterManager
    {
        
        InteractableUI interactableUI;
        
        Animator anim;
        [Header("Interaction Animations")]
        public string WorldGateInteraction = "World_Gate";
        public string ChestInteraction = "Open Chest";
        public string FogWallInteraction = "Pass Through Fog";
        public string LargeDoorInteraction = "OpenLargeDoor";
        public string PickUpItem = "Pick Up Item";




        [HideInInspector] public PlayerLocomotion locomotion;
        [HideInInspector] public PlayerStats stats;
        [HideInInspector] public PlayerInventory inventory;
        [HideInInspector] public PlayerAnimatorManager animatorManager;
        [HideInInspector] public InputHandler inputHandler;
        [HideInInspector] public PlayerEffectsManager effectsManager;
        [HideInInspector] public PlayerCombatManager combatManager;
        [HideInInspector] public WeaponSlotManager weaponSlotManager;
        [HideInInspector] public PlayerEquipmentManager equipmentManager;
        [HideInInspector] public PlayerFateManager fateManager;
        [HideInInspector] public Animator animator;
        [HideInInspector] public Camera main_camera;
        [HideInInspector] public EquiptmentWindowUI equipmentWindowUI;
        
        [HideInInspector] public bool isSprinting;


        [Header("Audio Settings")]
        [SerializeField][Range(0,1)]private float musicVolume = 0.07f;
        [SerializeField][Range(0, 1)] private float crossFadeTime = 1;
        public AudioSource combatAudioSource;
        public AudioSource levelAudioSource;
        public AudioMixer mixer;

        [Space(5)]
        public string masterVol_s = "volume";
        public float masterVol = 0;

        [Space(5)]
        public string musicvol_s = "MusicVolume";
        public float musicvol = 0;

        [Space(5)]
        public string FXvol_s = "FXvolume";
        public float FXvol = 0;

        [Space(5)]
        public string magicVol_s = "MagicVolume";
        public float magicVol = 0;



        
        

        private Coroutine xfadeRoutine;
        


        [Header("item interaction Game objects")]
        public GameObject interactableUIGameObject;
        public GameObject itemInteractableGameObject;
        public GameObject collectableDisplay;
        public GameObject collectableDisplay_image;

        [Header("item interaction values")]
        [SerializeField][Range(0, 1)] private float interactonDistance = 1;
        [SerializeField][Range(0, 1)] private float interactionRadius = 1;












        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            animatorManager=GetComponent<PlayerAnimatorManager>();
            animatorManager.Initialize();
            inputHandler = GetComponent<InputHandler>();
            inventory = GetComponent<PlayerInventory>();
            anim = GetComponent<Animator>();
            locomotion = GetComponent<PlayerLocomotion>();
            effectsManager = GetComponent<PlayerEffectsManager>();
            combatManager = GetComponent<PlayerCombatManager>();
            weaponSlotManager = GetComponent<WeaponSlotManager>();
            equipmentManager= GetComponent<PlayerEquipmentManager>();
            fateManager = GetComponent<PlayerFateManager>();
            animator = GetComponent<Animator>();
            equipmentWindowUI = FindObjectOfType<EquiptmentWindowUI>();
            

            interactableUI = GameObject.FindObjectOfType<InteractableUI>();


            stats = GetComponent<PlayerStats>();
            main_camera = Camera.main;



        }
        private void Start()
        {
            Debug.Log(WorldSaveGameManager.instance);
            WorldSaveGameManager.instance.player = this;

        }




        public void Update()
        {
          

            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");
            base.isUsingLeftHand = anim.GetBool("isUsingLeftHand");
            isInvulnerable = anim.GetBool("isInvulnerable");
            isFiringSpell = anim.GetBool("isFiringSpell");
            anim.SetBool("isBlocking", isBlocking);
            anim.SetBool("isInAir", isInAir);
            anim.SetBool("isDead", stats.isDead);
            inputHandler.TickInput();
            animatorManager.canRotate = anim.GetBool("canRotate");
            locomotion.handleRollingAndSprinting();
            locomotion.handleJumping(locomotion.moveDirection);


             stats.RegenerateStanima();
            
            CheckForInteractableObject();


        }
        private void FixedUpdate()
        {
            locomotion.HandleMovement();
            locomotion.HandleRotation();
            locomotion.handleFalling(locomotion.moveDirection);
            CheckForTrackChange();
        }



        public void CheckForTrackChange()
        {
            if (xfadeRoutine != null)
            {
                return; // exit early if already cross fading.
            }
            if (isInCombat && combatAudioSource.isPlaying == false)
            {
                xfadeRoutine = StartCoroutine(crossFade(levelAudioSource, combatAudioSource));
            }
            else if (isInCombat == false && levelAudioSource.isPlaying == false)
            {
                xfadeRoutine = StartCoroutine(crossFade(combatAudioSource, levelAudioSource));
            }
        }

        IEnumerator crossFade(AudioSource fromSource, AudioSource toSource)
        {
            float time = 0;
            toSource.volume = 0;
            toSource.Play();

            while (time < crossFadeTime)
            {
                time += Time.deltaTime;
                fromSource.volume = Mathf.Lerp(musicVolume, 0, time / crossFadeTime);
                toSource.volume = Mathf.Lerp(0, musicVolume, time / crossFadeTime);
                yield return null; // loop next frame
            }

            fromSource.volume = 0;
            fromSource.Stop();
            xfadeRoutine = null;
        }



        private void LateUpdate()
        {

            inputHandler.rollFlag = false;
            inputHandler.RB_input = false;

            inputHandler.RT_input = false;
            inputHandler.LT_input = false;
            inputHandler.a_input = false;
            inputHandler.inventory_input = false;
            inputHandler.jump_input = false;
            inputHandler.d_pad_up = false;
            inputHandler.d_pad_down = false;
            inputHandler.d_pad_left = false;
            inputHandler.d_pad_right=false;
            

            float _delta = Time.fixedDeltaTime;

            if (isInAir)
            {
                locomotion.inAirTimer = locomotion.inAirTimer + Time.deltaTime;
            }
        }


        #region player interactions
        public void CheckForInteractableObject()
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, interactionRadius, transform.forward, out hit, interactonDistance)) {

                if (hit.collider.tag == "Interactable")
                {
         
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();
                    if(interactableObject != null)
                    {

                        string interactableText = interactableObject.interactableText;
                        interactableUI.interactableText.text = interactableText;
                        interactableUIGameObject.SetActive(true);
                        ///set the text pop up to true
                        ///
                        if (inputHandler.a_input || interactableObject.AutoInteract)
                        {
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                }
            }
            else
            {
                if (interactableUIGameObject != null)
                {
                    interactableUIGameObject.SetActive(false);
                    collectableDisplay.SetActive(false);
                    itemInteractableGameObject.SetActive(false);
                }

                //if(itemInteractableGameObject != null && inputHandler.a_input)
                //{
                //    itemInteractableGameObject.SetActive(false);
                //    collectableDisplay.SetActive(false);
                //}
            }
        }

        public void InteractableAnimtionTransformReset(Transform playerStandsHere,string _anim)
        {
            locomotion.rigidbody.velocity = Vector3.zero; //stops the player from ice skating
            transform.position = playerStandsHere.position;
            Vector3 rotationDirection = playerStandsHere.transform.forward;
            Quaternion turnRotation = Quaternion.LookRotation(rotationDirection);
            transform.rotation = turnRotation;
            animatorManager.PlayTargetAnimation(_anim, true);
        }

        #endregion
        public void GoToNebulasDream()
        {


            SceneManager.LoadScene("nexus");

        }
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }



        public void exitGame()
        {
          
            SceneManager.LoadScene("StartScreen");
        }

        public void SaveCharacterDataToCurrentSaveData(ref CharacterSaveData currentCharacterData)
        {
            if (mixer != null){
            mixer.GetFloat(magicVol_s, out magicVol);
            mixer.GetFloat(masterVol_s, out masterVol);
            mixer.GetFloat(FXvol_s, out FXvol);
            mixer.GetFloat(musicvol_s, out musicvol);
            }
            currentCharacterData.character_level = stats.char_lvl;
            currentCharacterData.shardCount = stats.ShardCount;

            currentCharacterData.music_volume = musicvol;
            currentCharacterData.FX_volume = FXvol;
            currentCharacterData.magic_volume = magicVol;
            currentCharacterData.master_volume = masterVol;


            currentCharacterData.xPosition = transform.position.x;
            currentCharacterData.yPosition = transform.position.y;
            currentCharacterData.zPosition = transform.position.z;

            currentCharacterData.vitalityLevel = stats.vitalityLevel;
            currentCharacterData.EnduranceLevel = stats.EnduranceLevel;
            currentCharacterData.SanityLevel = stats.SanityLevel;
            currentCharacterData.StrengthLevel = stats.StrengthLevel;
            currentCharacterData.DexterityLevel = stats.DexterityLevel;
            currentCharacterData.MagicLevel = stats.MagicLevel;
            currentCharacterData.BlasphemyLevel = stats.BlasphemyLevel;
            currentCharacterData.LuckLevel = stats.LuckLevel;


            currentCharacterData.rightHandQickSlot0ItemID = inventory.weaponsInRightHandSlots[0].itemID;
            currentCharacterData.rightHandQickSlot1ItemID = inventory.weaponsInRightHandSlots[1].itemID;

            currentCharacterData.leftHandQickSlot0ItemID = inventory.weaponsInLeftHandSlots[0].itemID;
            currentCharacterData.leftHandQickSlot1ItemID = inventory.weaponsInLeftHandSlots[1].itemID;


            if(inventory.rightWeapon!=null)
                currentCharacterData.currentRightHandWeaponID = inventory.rightWeapon.itemID;
            else
                currentCharacterData.currentRightHandWeaponID=-1;
            
            
            if(inventory.leftWeapon!=null)
                currentCharacterData.currentLeftHandWeaponID = inventory.leftWeapon.itemID;
            else
                currentCharacterData.currentLeftHandWeaponID=-1;

            if(fateManager.card_one!=null)
                currentCharacterData.currentTarotA_ID =fateManager.card_one.itemID;
            else
                currentCharacterData.currentTarotA_ID=-1;
            
            if(fateManager.card_two!=null)
                currentCharacterData.currentTarotB_ID =fateManager.card_two.itemID;
            else
                currentCharacterData.currentTarotB_ID=-1;

            if(fateManager.card_Three!=null)
                currentCharacterData.currentTarotC_ID =fateManager.card_Three.itemID;
            else
                currentCharacterData.currentTarotC_ID=-1;

            if(fateManager.card_four!=null)
                currentCharacterData.currentTarotD_ID =fateManager.card_four.itemID;
            else
                currentCharacterData.currentTarotD_ID=-1;

            if(fateManager.card_Five!=null)
                currentCharacterData.currentTarotE_ID =fateManager.card_Five.itemID;
            else
                currentCharacterData.currentTarotE_ID=-1;

        }

        public void LoadCharacterDataFromCurrentCharacterSaveData(ref CharacterSaveData currentCharacterData)
        {

            stats.char_lvl = currentCharacterData.character_level;
            stats.ShardCount = currentCharacterData.shardCount;
            stats.soulCountBar.SetSoulCountText(stats.ShardCount);

            transform.position= new Vector3( currentCharacterData.xPosition, currentCharacterData.yPosition, currentCharacterData.zPosition);


            stats.vitalityLevel = currentCharacterData.vitalityLevel;
            stats.EnduranceLevel = currentCharacterData.EnduranceLevel;
            stats.SanityLevel = currentCharacterData.SanityLevel;
            stats.StrengthLevel = currentCharacterData.StrengthLevel;
            stats.DexterityLevel = currentCharacterData.DexterityLevel;
            stats.MagicLevel = currentCharacterData.MagicLevel;
            stats.BlasphemyLevel = currentCharacterData.BlasphemyLevel;
            stats.LuckLevel = currentCharacterData.LuckLevel;

            if( mixer != null){
                mixer.SetFloat(musicvol_s, currentCharacterData.music_volume);
                mixer.SetFloat(FXvol_s, currentCharacterData.FX_volume);
                mixer.SetFloat(magicVol_s, currentCharacterData.magic_volume);
                mixer.SetFloat(masterVol_s,currentCharacterData.master_volume);

            }
            
            inventory.weaponsInRightHandSlots[0] = ItemDatabase.instance.GetWeaponItemByID(currentCharacterData.rightHandQickSlot0ItemID);
            inventory.weaponsInRightHandSlots[1] = ItemDatabase.instance.GetWeaponItemByID(currentCharacterData.rightHandQickSlot1ItemID);
            

            inventory.weaponsInLeftHandSlots[0] = ItemDatabase.instance.GetWeaponItemByID(currentCharacterData.leftHandQickSlot0ItemID);
            inventory.weaponsInLeftHandSlots[1] = ItemDatabase.instance.GetWeaponItemByID(currentCharacterData.leftHandQickSlot1ItemID);


            Debug.Log(ItemDatabase.instance.GetWeaponItemByID(currentCharacterData.currentRightHandWeaponID));
            inventory.rightWeapon =ItemDatabase.instance.GetWeaponItemByID(currentCharacterData.currentRightHandWeaponID);
            inventory.leftWeapon = ItemDatabase.instance.GetWeaponItemByID(currentCharacterData.currentLeftHandWeaponID);
            weaponSlotManager.LoadBothWeaponsOnSlots();
            Debug.Log(inventory.rightWeapon);

            fateManager.card_one = ItemDatabase.instance.GetTarotCardItemByID(currentCharacterData.currentTarotA_ID);
            fateManager.card_two=ItemDatabase.instance.GetTarotCardItemByID(currentCharacterData.currentTarotB_ID);
            fateManager.card_Three=ItemDatabase.instance.GetTarotCardItemByID(currentCharacterData.currentTarotC_ID);
            fateManager.card_four=ItemDatabase.instance.GetTarotCardItemByID(currentCharacterData.currentTarotD_ID);
            fateManager.card_Five =ItemDatabase.instance.GetTarotCardItemByID(currentCharacterData.currentTarotE_ID);
            stats.SetMaxFocusFromFocusLevel();
            stats.SetMaxHealthFromHealthLevel();
            stats.SetMaxStaminaFromStaminaLevel();
            stats.SetUpStrengthSubStats();



            foreach(KeyValuePair<int,int> wep in currentCharacterData.primaryWeaponItemInventory){
                inventory.primaryWeaponInventory.Add(ItemDatabase.instance.GetWeaponItemByID(wep.Key));
            }

            Debug.Log(" EquiptmentWindowUI: " + equipmentWindowUI);
            Debug.Log(" inventory: " + inventory);
            Debug.Log(" fate: " + fateManager);



        }


    }
}
