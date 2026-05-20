using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Mana playermana;
    [SerializeField] private Image maxmana;
    [SerializeField] private Image currentmana;

    private void Start()
    {
        maxmana.fillAmount = playermana.current_mana / 10;
    }
    private void Update()
    {
        currentmana.fillAmount = playermana.current_mana / 10;
    }
}