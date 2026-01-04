using System;
using UnityEngine;

public class PlaceableItemBehaviour : MonoBehaviour
{
    [SerializeField] bool isGrounded;
    [SerializeField] bool isOverlappingItems;
    [SerializeField] bool canBePlaced;
    [SerializeField] BoxCollider solidCollider;
    
    float groundCheckDistance = 0.1f;

    public BoxCollider SolidCollider => solidCollider;


    void Update()
    {
        CheckIfGrounded();
        canBePlaced = isGrounded && !isOverlappingItems;
        print("Can be placed: " + canBePlaced);

    }

    private void CheckIfGrounded()
    {
        float rayLength = SolidCollider.bounds.extents.y + groundCheckDistance; // half height + small buffer

        // Debug line
        Debug.DrawRay(transform.position, Vector3.down * rayLength, isGrounded ? Color.green : Color.red);

        isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit groundHit, rayLength, LayerMask.GetMask("PlaceableGround"));

        if (isGrounded)
        {
            transform.rotation = Quaternion.FromToRotation(transform.up, groundHit.normal) * transform.rotation;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactables"))// || other.CompareTag("Pickable"))
        {
            isOverlappingItems = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactables"))// || other.CompareTag("Pickable"))
        {
            isOverlappingItems = false;
        }
    }
}
