using System;
using UnityEngine;

public class AttackDmg : MonoBehaviour
{
    [SerializeField] private float Attack_dmg = 1f; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Enemy")
        {
            Debug.Log("Enemy hit :" +  collision.gameObject.name);
        }
        if(collision != null)
            Debug.Log("Enemy hit :" + collision.gameObject.name);
    }
}
