using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    PlayerManager playerManager;
    CharacterStats myStats;

    private void Start() {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }
    public override void Interact(int a) {
        base.Interact(0);
         CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();

        if (a == 0)
            a = 1;
        //Attack Enemy
        if (playerCombat != null) {
            playerCombat.Attack(myStats,a);

        }
        
    }
}
