using UnityEngine;


public class CharacterStats : MonoBehaviour
{
    public Stat MaxHealth;
    public Stat Speed;
    public Stat Armor;
    public Stat CriticalHitChance;

    public float CalculateDamage(float receivedDamage)
    {
        receivedDamage -= Armor.GetValue();
        receivedDamage = Mathf.Clamp(receivedDamage, 0, int.MaxValue);
        return receivedDamage;
    }

    public void UpdateArmor(int value)
    {
        Armor.IncreaseValue();
    }
}
