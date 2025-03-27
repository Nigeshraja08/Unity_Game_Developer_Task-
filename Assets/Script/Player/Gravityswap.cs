using Unity.Cinemachine;
using UnityEngine;

public class Gravityswap : MonoBehaviour
{
    [SerializeField] Transform halogram;
    [SerializeField] float hallowgramOffset = 2;

    PlayerInputs playerInputs;
    Vector2 selectedGravity;
    Vector2 lastSelectedGravity;
  
    private void Awake()
    {
        playerInputs = GetComponent<PlayerInputs>();
        halogram.gameObject.SetActive(false);
    }

    void Update()
    {
        HandleGravity();
        
    }

    private void HandleGravity()
    {
        selectedGravity = Vector2.zero;
        if (playerInputs.changeGravity.y > 0) selectedGravity = new Vector2(0, 9.81f);
        if (playerInputs.changeGravity.y < 0) selectedGravity = new Vector2(0, -9.81f); ;
        if (playerInputs.changeGravity.x > 0) selectedGravity = new Vector2(-9.81f, 0);
        if (playerInputs.changeGravity.x < 0) selectedGravity = new Vector2(9.81f, 0);



        if (selectedGravity != Vector2.zero)
        {
          
            UpdateHologram();
            lastSelectedGravity = selectedGravity;

            if (playerInputs.Isconfirm == true)
            {
                              
                ApplyGravity(selectedGravity);

            }

        }
        else
        {
            halogram.gameObject.SetActive(false);
        }

    }

    private void UpdateHologram()
    {
        if (lastSelectedGravity == Vector2.zero) return; // No update needed
        
        // Convert 2D gravity direction to 3D space
        Vector3 gravityDir = new Vector3(lastSelectedGravity.x, lastSelectedGravity.y, 0).normalized;

        // Offset the hologram position in the direction of gravity
        halogram.position = transform.position + gravityDir * hallowgramOffset;

        // Rotate the hologram to point towards the gravity direction
        halogram.rotation = Quaternion.LookRotation(-Vector3.forward,- gravityDir);

        halogram.gameObject.SetActive(true);

    }

    private void ApplyGravity(Vector2 selectedGravity)
    {
        Physics.gravity = selectedGravity;

    }


}
