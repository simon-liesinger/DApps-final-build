using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        float playerDirection = (transform.rotation.eulerAngles.y == 180f) ? 1f : -1f;
        bulletObj.transform.localScale = new Vector3(playerDirection, 1, 1);
    }
}