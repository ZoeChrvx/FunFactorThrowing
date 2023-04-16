using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Throwing : MonoBehaviour
{
    public static Throwing instance;

    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;
    private InputAction throwingAction;


    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        readyToThrow = true;

    }

    private void Update()
    {
    }

    public void ThrowingAction(InputAction.CallbackContext context)
    {
        if (readyToThrow && context.ReadValueAsButton())
        {
            Throw();
        }
    }

    public void Throw()
    {
        readyToThrow = false;

        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        Invoke("ResetThrow", throwCooldown);

        Destroy(projectile, 10f);
    }

    void ResetThrow()
    {
        readyToThrow = true;
    }


}
