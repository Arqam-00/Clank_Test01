using UnityEngine;
using System.Collections;

public class Mana : MonoBehaviour
{
    [Header("Mana")]
    [SerializeField] private float MaxMana = 10f;
    public float current_mana { get; private set; }
    private void Awake()
    {
        current_mana =MaxMana;
    }

    public void DecreaseMana(float mp)
    {
        current_mana = Mathf.Clamp(current_mana - mp, 0, MaxMana);
    }
    public void AddMana(float _value)
    {
        current_mana = Mathf.Clamp(current_mana + _value, 0, MaxMana);
    }
    
}