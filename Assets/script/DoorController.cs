using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotationController : MonoBehaviour
{
    public float openAngleRight = 90f;
    public float openAngleLeft = -90f;
    public float closeAngle = 0f;
    public float smooth = 2f;
    private bool isOpen = false;
    private bool isRight = true; // Menentukan arah bukaan pertama kali
    private bool inTrigger = false;

    void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
        }

        if (isOpen)
        {
            Quaternion targetRotation;
            if (isRight)
            {
                targetRotation = Quaternion.Euler(0, openAngleRight, 0);
            }
            else
            {
                targetRotation = Quaternion.Euler(0, openAngleLeft, 0);
            }
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(0, closeAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = false;
            isRight = !isRight; // Mengubah arah bukaan ketika pemain keluar dari area trigger
        }
    }
}
