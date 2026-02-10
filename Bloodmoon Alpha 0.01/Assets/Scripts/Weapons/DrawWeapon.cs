using UnityEngine;

public class DrawWeapon : MonoBehaviour
{
    Animator animator;
    bool weaponDrawn = false;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void drawWeapon()
    {
        if (!weaponDrawn && Input.GetKey(KeyCode.R)) 
        {
            animator.SetTrigger("DrawWeapon");
            weaponDrawn = true; 
        }
    }
}
