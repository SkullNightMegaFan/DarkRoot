using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour {

    public string ItemName = "NAMELESS_ITEM";
    public Sprite ItemHUDSprite = null;
    public float CooldownSeconds = 1.0f;
    float CurrentCooldownSeconds = 0.0f;

    public GameObject PickupItemPrefab;


    public void UpdateCooldowns () {
		if(CurrentCooldownSeconds > 0.0f)
        {
            CurrentCooldownSeconds -= Time.deltaTime;
            if(CurrentCooldownSeconds < 0.0f)
            {
                CurrentCooldownSeconds = 0.0f;
            }
        }
	}

    public float GetCurrentCooldownSeconds()
    {
        return CurrentCooldownSeconds;
    }

    public void UseItem(PlayerController playerController)
    {
        if(CurrentCooldownSeconds <= 0.0f)
        {
            OnItemUsed(playerController);
            CurrentCooldownSeconds = CooldownSeconds;
        }
    }

    virtual protected void OnItemUsed(PlayerController playerController)
    {

    }

    public ItemBase Clone()
    {
        return (ItemBase)MemberwiseClone();
    }
}
