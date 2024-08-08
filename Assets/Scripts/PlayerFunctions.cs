using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class PlayerFunctions : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    [SerializeField]
    private Camera thirdPersonCamera;
    [SerializeField]
    private Camera firstPersonCamera;
    private Camera playerCamera;
    [SerializeField]
    private float playerHealth;
    [SerializeField]
    private float playerStamina;
    [SerializeField]
    private float playerShield;
    [SerializeField]
    private float playerAttack;
    [SerializeField]
    private float playerDefence;
    [SerializeField]
    private int maxPlayerHealth;
    [SerializeField]
    private int maxPlayerStamina;
    [SerializeField]
    private int maxPlayerShield;
    [SerializeField]
    private float walkSpeed = 5f;
    [SerializeField]
    private float rotationSpeed = 15f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float dashSpeed = 5.5f;
    [SerializeField]
    private float dashTime = 0.5f;
    [SerializeField]
    private float sprintSpeed = 7.5f;
    [SerializeField]
    private float dashCooldown = 2.0f;
    [SerializeField]
    private int dashStaminaCost = 10;
    [SerializeField]
    private float attackCooldown = 0.5f;
    [SerializeField]
    private int  attackStaminaCost = 5;
    private float lastAttackTime = -Mathf.Infinity;
    private float lastDashTime = -Mathf.Infinity;
    private bool groundedPlayer;
    private Vector3 playerVelocity;
    private Vector3 movementDirection;
    private bool dashing = false;
    private bool sprinting = false;
    private bool jumping = false;
    private bool moving = false;
    private bool attackingWithWeapon = false;
    private bool attackingWithoutWeapon = false;
    
    public AudioSource source;
    public AudioSource backgroundSource;
    public AudioClip clipAttack1;
    public AudioClip clipAttack2;
    public AudioClip clipWalk;
    public AudioClip clipJump;
    public AudioClip clipRun;   
    public AudioClip clipBackgroundMusic;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator  = GetComponentInChildren<Animator>();
        playerCamera = thirdPersonCamera;
        firstPersonCamera.enabled = false;
        firstPersonCamera.GetComponent<AudioListener>().enabled = false;
        PlayBackGroundMusic();
    }

    void Update()
    {   
        if(!GameManager.inputFieldActive){
            HandleCameraChange();
            HandleJump();
            HandleDash();
            HandleAttack();
            HandleMovement();
        }
    }

    private void HandleMovement(){

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -gravityValue * Time.deltaTime;
            
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float movementSpeed;

        Vector3 movementInput = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0) * new Vector3(moveHorizontal, 0, moveVertical);
        movementDirection = movementInput.normalized;

        moving = movementDirection != Vector3.zero;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprinting = true;
            movementSpeed = sprintSpeed;
        }else{
            movementSpeed = walkSpeed;
            sprinting = false;
        }

        if(!attackingWithWeapon && !attackingWithoutWeapon){
            controller.Move(movementSpeed * Time.deltaTime * movementDirection);
            PlayMoveSound();
        }   

        if( !(movementDirection == Vector3.zero || movementDirection == Vector3.back)){
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
    private void HandleJump(){    
        if (Input.GetButtonDown("Jump") && groundedPlayer && !dashing)
        {   
            animator.SetTrigger("isJumping");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void HandleDash(){
        groundedPlayer = controller.isGrounded;
        bool dashOnCooldown = Time.time < lastDashTime + dashCooldown;
        if (Input.GetMouseButtonDown(1) && groundedPlayer && !dashOnCooldown && !attackingWithoutWeapon && !attackingWithWeapon)
        {  
            dashing = true;
            lastDashTime = Time.time; 
            StartCoroutine(Dash());
            playerStamina -= dashStaminaCost;
        }

    }

    private void HandleAttack(){
        bool attackOnCooldown = Time.time < lastAttackTime + attackCooldown;

        if (Input.GetMouseButtonDown(0) && !dashing && !sprinting && !jumping && movementDirection == Vector3.zero)
        {   
            if (!attackOnCooldown){
                if(GameManager.hasSword){
                    attackingWithWeapon = true;
                    lastAttackTime = Time.time;
                    StartCoroutine(AttackWithWeapon());
                    playerStamina -= attackStaminaCost;
                }else{
                    attackingWithoutWeapon = true;
                    lastAttackTime = Time.time;
                    StartCoroutine(AttackWithoutWeapon());
                    playerStamina -= attackStaminaCost;
                }
            }
        }
    }

    private IEnumerator AttackWithWeapon(){
        while((Time.time < lastAttackTime + attackCooldown) && playerStamina > attackStaminaCost){
            PlayAttackSound();
            yield return new WaitForSeconds(0.55f);
        }
        attackingWithWeapon = false;
    }

    private IEnumerator AttackWithoutWeapon(){
       while((Time.time < lastAttackTime + attackCooldown) && playerStamina > attackStaminaCost){
            PlayAttackSound();
            yield return new WaitForSeconds(0.8f);
        }
        attackingWithoutWeapon = false;
    }

    private IEnumerator Dash(){
        float startTime = Time.time;    
        while((Time.time < startTime + dashTime) && playerStamina > dashStaminaCost){
            controller.Move(dashSpeed * Time.deltaTime * movementDirection);
            yield return null;
        }
        dashing = false;
    }

    private void PlayAttackSound(){
        int number = UnityEngine.Random.Range(1 , 3);
        if(number % 2 == 0){
            source.PlayOneShot(clipAttack1);
        }
        else{
            source.PlayOneShot(clipAttack2);
        }
    }

    private void PlayMoveSound(){
        if (moving && !source.isPlaying)
        {   
            source.clip = clipWalk;
            source.loop = true;
            source.volume = 0.3f;
            source.Play();
        }
        else if (!moving)
        {
            source.Stop();
        }
    }

    private void PlayBackGroundMusic(){
        backgroundSource.clip = clipBackgroundMusic;
        backgroundSource.loop = true;
        backgroundSource.volume = 0.1f;
        backgroundSource.Play();
    }
    
    private void HandleCameraChange(){
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (thirdPersonCamera.enabled)
            {
                thirdPersonCamera.enabled = false;
                thirdPersonCamera.GetComponent<AudioListener>().enabled = false;

                firstPersonCamera.enabled = true;
                firstPersonCamera.GetComponent<AudioListener>().enabled = true;

                playerCamera = firstPersonCamera;
                if(gameObject.transform.Find("Model/Armors/StarterClothes").gameObject.activeSelf == true){
                    gameObject.transform.Find("Model/Mesh/Body/Head").gameObject.SetActive(false);
                    gameObject.transform.Find("Model/Mesh/Body/Neck").gameObject.SetActive(false);  
                    gameObject.transform.Find("Model/Mesh/Body/Arms").gameObject.SetActive(false);
                    gameObject.transform.Find("Model/Armors/StarterClothes/Starter_Chest").gameObject.SetActive(false);
                    gameObject.transform.Find("Model/Mesh/Customization").gameObject.SetActive(false);
                }else if (gameObject.transform.Find("Model/Armors/Plate1").gameObject.activeSelf == true){
                    gameObject.transform.Find("Model/Mesh/Body/Head").gameObject.SetActive(false);
                    gameObject.transform.Find("Model/Mesh/Body/Neck").gameObject.SetActive(false);
                    gameObject.transform.Find("Model/Mesh/Customization").gameObject.SetActive(false);
                    gameObject.transform.Find("Model/Armors/Plate1/PlateSet1_Helmet").gameObject.SetActive(false);
                    gameObject.transform.Find("Model/Armors/Plate1/PlateSet1_Chest").gameObject.SetActive(false);
                    gameObject.transform.Find("Model/Armors/Plate1/PlateSet1_Shoulders").gameObject.SetActive(false);
                }else{
                    gameObject.transform.Find("Model/Mesh/Body/Head").gameObject.SetActive(false);
                    gameObject.transform.Find("Model/Mesh/Body/Neck").gameObject.SetActive(false);
                    gameObject.transform.Find("Model/Mesh/Body/Chest").gameObject.SetActive(false);
                    gameObject.transform.Find("Model/Mesh/Body/Arms").gameObject.SetActive(false);
                    gameObject.transform.Find("Model/Mesh/Customization").gameObject.SetActive(false);
                }

                GameManager.firstPersonCamera = true;
            }
            else
            {
                thirdPersonCamera.enabled = true;
                thirdPersonCamera.GetComponent<AudioListener>().enabled = true;

                firstPersonCamera.enabled = false;
                firstPersonCamera.GetComponent<AudioListener>().enabled = false;
                playerCamera = thirdPersonCamera;

                if(gameObject.transform.Find("Model/Armors/StarterClothes").gameObject.activeSelf == true){
                    gameObject.transform.Find("Model/Mesh/Body/Head").gameObject.SetActive(true);
                    gameObject.transform.Find("Model/Mesh/Body/Neck").gameObject.SetActive(true);
                    gameObject.transform.Find("Model/Armors/StarterClothes/Starter_Chest").gameObject.SetActive(true);
                    gameObject.transform.Find("Model/Mesh/Customization").gameObject.SetActive(true);
                }else if (gameObject.transform.Find("Model/Armors/Plate1").gameObject.activeSelf == true){
                    gameObject.transform.Find("Model/Mesh/Body/Head").gameObject.SetActive(true);
                    gameObject.transform.Find("Model/Mesh/Body/Neck").gameObject.SetActive(true);
                    gameObject.transform.Find("Model/Mesh/Customization").gameObject.SetActive(true);
                    gameObject.transform.Find("Model/Armors/Plate1/PlateSet1_Helmet").gameObject.SetActive(true);
                    gameObject.transform.Find("Model/Armors/Plate1/PlateSet1_Chest").gameObject.SetActive(true);
                    gameObject.transform.Find("Model/Armors/Plate1/PlateSet1_Shoulders").gameObject.SetActive(true);
                }else{
                    gameObject.transform.Find("Model/Mesh/Body/Head").gameObject.SetActive(true);
                    gameObject.transform.Find("Model/Mesh/Body/Neck").gameObject.SetActive(true);
                    gameObject.transform.Find("Model/Mesh/Body/Chest").gameObject.SetActive(true);
                    gameObject.transform.Find("Model/Mesh/Body/Arms").gameObject.SetActive(true);
                    gameObject.transform.Find("Model/Mesh/Customization").gameObject.SetActive(true);
                }

                GameManager.firstPersonCamera = false;
            }
        }
    }

    public void TakeDamage(int damage){
        float damage_received = damage * (1 - playerDefence/100);
        if(playerShield > damage_received){
            playerShield -= damage_received;
        }else{
            float damage_difference = damage_received - playerShield;
            playerShield = 0;
            playerHealth -= damage_difference;
        }
    }

    public Vector3 GetMovementDirection(){
        return movementDirection;
    }

    public Vector3 GetPlayerVelocity(){
        return playerVelocity;
    }

    public bool IsJumping(){
        return jumping;
    }

    public bool IsDashing(){
        return dashing;
    }

    public bool IsSprinting(){
        return sprinting;
    }

    public bool IsGrounded(){
        return groundedPlayer;
    }

    public bool IsAttackingWithWeapon(){
        return attackingWithWeapon;
    }

    public bool IsAttackingWithoutWeapon(){
        return attackingWithoutWeapon;
    }

    public float GetPlayerHealth(){
        return playerHealth;
    }

    public float GetPlayerStamina(){
        return playerStamina;
    }

    public float GetPlayerDefence(){
        return playerDefence;
    }

    public float GetPlayerAttack(){
        return playerAttack;
    }

    public int GetMaxPlayerHealth(){
        return maxPlayerHealth;
    }

    public int GetMaxPlayerStamina(){
        return maxPlayerStamina;
    }

    public void SetPlayerHealth(float health){
        playerHealth = health;
    }

    public void SetPlayerStamina(float stamina){
        playerStamina = stamina;
    }

    public void SetPlayerDefence(float defence){
        playerDefence = defence;
    }

    public void SetPlayerAttack(float attack){
        playerAttack = attack;
    }

    public void SetMaxPlayerHealth(int maxHealth){
        maxPlayerHealth = maxHealth;
    }

    public void SetMaxPlayerStamina(int maxStamina){
        maxPlayerStamina = maxStamina;
    }

    public void SetPlayerShield(float shield){
        playerShield = shield;
    }

    public float GetPlayerShield(){
        return playerShield;
    }

    public void SetMaxPlayerShiled(int maxShield){
        maxPlayerShield = maxShield;
    }

    public int GetMaxPlayerShield(){
        return maxPlayerShield;
    }

    public float GetAttackCooldown(){
        return attackCooldown;
    }

    public float GetLastAttackTime(){
        return lastAttackTime;
    }   

}
