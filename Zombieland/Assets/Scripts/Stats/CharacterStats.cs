using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    // should I turn them into properties?

    public Stat Damage;
    public Stat MaxHealth;
    public Stat Armor;
    public Stat MovementSpeed;
    public Stat HealthRecoveringSpeed;
    public Stat CriticalHitChance;

    public float CalculateDamage(float receivedDamage)
    {
        receivedDamage -= Armor.GetValue();
        receivedDamage = Mathf.Clamp(receivedDamage, 0, int.MaxValue);
        return receivedDamage;
    }
}
