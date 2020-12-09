using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Equipment" , menuName ="Inventory/Equipment")]
public class Equipment : Item{

    public EquipmentSlot equipSlot;
    public WeaponType wepType;
    public ArmorType armorType;
    public Gender gender;

    public int armorModifier, damageModifier, level=1, degree=1, critical=1, durability, block;
    public int strBuff=0, intBuff=0, durBuff=0, plus=0;
    public float range;

    public override void Use() {
        base.Use();
        EquipmentManager.instance.Equip(this);
        removeFromInventory();
        //Equip the item
        //Remove item from inventory
    }
}

public enum EquipmentSlot { Weapon, Shield, Head, Chest , Shoulder, Hand, Legs, Feet}
public enum WeaponType {none, OneHanded,TwoHanded,DoubleHand,Bow}
public enum ArmorType { none, HeavyArmor, LightArmor, Garment }
public enum Gender { none,Male,Female}
