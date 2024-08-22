using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatLinkerModel : MonoBehaviour
{
    List<PlayerModel> _linkedPlayers = new List<PlayerModel>();
    public void AddLinkedPlayer(PlayerModel model){
        _linkedPlayers.Add(model);
    }
    public List<PlayerModel> GetLinkedPlayers(){
        return _linkedPlayers;
    }
    public void DealDmgToAllLinkedPlayers(float dmgAmt){
        foreach (PlayerModel player in _linkedPlayers){
            player.DealSkillDamage(dmgAmt, false);
        }
    }
    public CharacterStat GetLinkedStatBuff(){
        CharacterStat characterLinkedStat = new CharacterStat();
        foreach(PlayerModel playerModel in _linkedPlayers){
            characterLinkedStat += StatsManager.GetFinalData(playerModel, false);
        }
        return characterLinkedStat;
    }
}
