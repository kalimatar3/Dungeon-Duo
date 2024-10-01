using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class TriggerBox : MyBehaviour
{
    [SerializeField] protected List<Itriggerable> listtrigger;
    public List<Itriggerable> Listtrigger {get {return listtrigger;}}
    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.GetComponent<Player>()) return;
        foreach(var ele in listtrigger) {
            ele.Ontrigger();
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(!other.transform.GetComponent<Player>()) return;
        foreach(var ele in listtrigger) {
            ele.Ontrigger();
        }
    }
}
