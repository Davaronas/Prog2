using UnityEngine;

public class PlayerModifiers : MonoBehaviour
{
   [SerializeField] private int damageReduction = 0;
   [SerializeField] private int chanceToNotUseAmmo = 0;
   [SerializeField] private int movementSpeedIncrease = 0;


    private int ammoUse_ = 0;

    public void SetDamageReduction(int _m)
    {
        damageReduction = _m;
    }

    public void SetChanceToNotUseAmmo(int _m)
    {
        chanceToNotUseAmmo = _m;
    }

    public void SetMovementSpeedIncrease( int _m)
    {
        movementSpeedIncrease = _m;
    }




    public float GetDamageReductionPercent()
    {
        return 1 - ((float)damageReduction / 100);
    }

    public int GetMovementSpeedIncrease()
    {
       return movementSpeedIncrease;
    }

    public bool IsAmmoSpared()
    {
        ammoUse_ = Random.Range(0, 100);

        if(ammoUse_ <= chanceToNotUseAmmo)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
