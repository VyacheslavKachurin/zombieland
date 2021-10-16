using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    // should I turn them into properties?

    public Stat damage;
    public Stat MaxHealth;
    public Stat armor;
    public Stat movementSpeed;
    public Stat healthRecoveringSpeed;
    public Stat criticalHitChance;

    public float CalculateDamage(float receivedDamage)
    {
        receivedDamage -= armor.GetValue();
        receivedDamage = Mathf.Clamp(receivedDamage, 0, int.MaxValue);
        return receivedDamage;
    }
}
