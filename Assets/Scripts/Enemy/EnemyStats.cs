
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;

public class EnemyStats : CharacterStats
{
    public GameObject Kasa,itemsParent,lvlParti;
    public string mobName;
    itemManager itManager;
    public float Exp, Sp;
    public int Level;
    [HideInInspector]
    public int ratio,ratio1,rand;

    private void Start() {

        itemsParent = StaticMethods.FindInActiveObjectByName("ItemsParent");
        Exp = this.GetComponent<enemyInfos>().expMob[Level - 1];
        mobName = this.GetComponent<enemyInfos>().names[Level - 1];
        armor.baseValue = this.GetComponent<enemyInfos>().deffance[Level - 1];
        damage.baseValue = this.GetComponent<enemyInfos>().damage[Level - 1];
        maxHealth = this.GetComponent<enemyInfos>().maxHealth[Level - 1];
    }

    public override void Die() {
        base.Die();
        if (gameObject.layer == 11)
            this.GetComponent<Animator>().SetBool("isDead", true);
        GameObject player = GameObject.Find("Player");
        player.GetComponent<Player>().AddExperience(Exp);
        player.GetComponent<Player>().AddSkillPoint(Sp);

        GameObject logViewer = GameObject.Find("logContent");
        Color renk = Color.green;
        Color renk2 = Color.yellow;
        logViewer.GetComponent<logViewer>().entryLog(Exp + "\t Experience gained.",renk);
        logViewer.GetComponent<logViewer>().entryLog(Sp + "\t Skill Point gained.",renk2);

        

        #region Item Drop
        Transform trans = this.transform;
        trans.position = new Vector3(trans.position.x,trans.position.y + 0.42f,trans.position.z);
        GameObject itemler = GameObject.Find("ItemManager");
        itManager = itemler.GetComponent<itemManager>();
        ratio = (int)Random.Range(0f,1f);
        ratio1 = (int)Random.Range(0f,1f);
        rand = (int)Random.Range(0f, itManager.items.Count);
        Debug.Log(ratio + " \t"+ ratio1 + " \t" + rand);
        if(ratio == ratio1) {
            GameObject itemKasa = Instantiate(Kasa, trans.transform.position,Quaternion.identity) as GameObject;
            itemKasa.GetComponent<ItemPickup>().item = itManager.items[rand];
            itemKasa.GetComponent<ItemPickup>().itemsParent = itemsParent;
            Destroy(itemKasa, 60);
        }
        #endregion

        
        Destroy(gameObject,3);
        player.GetComponent<PlayerControl>().focus = null;
        GameObject.Find("MobUI").SetActive(false);

        #region Spawner Reset
        int indexLvl = gameObject.GetComponent<EnemyStats>().Level;
        indexLvl--;
        GameObject.Find("SpawnerManager").GetComponent<SpawnerManager>().spawnerList[indexLvl].gameObject.GetComponent<spawner>().amount -=1;
        GameObject.Find("SpawnerManager").GetComponent<SpawnerManager>().spawnerList[indexLvl].gameObject.GetComponent<spawner>().startCorotine();

        #endregion
    }

}
