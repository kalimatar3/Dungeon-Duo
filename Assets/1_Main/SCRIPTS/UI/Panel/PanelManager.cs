using System;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : LazySingleton<PanelManager>
{
    [SerializeField] protected List<BasePanel> ListPanels;
    public string Curpanel;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadListpanel();
    }
    public List<BasePanel> getListPanels()
    {
        return this.ListPanels;
    }
    protected void LoadListpanel()
    {
        if (ListPanels.Count > 0) return;
        foreach (Transform panel in transform) 
        {
            if(panel.GetComponent<BasePanel>()) ListPanels.Add(panel.GetComponent<BasePanel>());
        }
    }
    private void OnEnable() {
        this.Curpanel = ListPanels[0].name; 
    }
    public void ReturntoMainMenu()
    {
        this.DeActiveAll();
        ListPanels[0].gameObject.SetActive(true);
    }
    public void DeActiveAll()
    {
        foreach (var ele in ListPanels)
        {
            ele.gameObject.SetActive(false);
        }
    }
    public Transform getPanelbyName(string panelname) {
        this.Curpanel = panelname;
        foreach(var ele in ListPanels)
        {
            if (ele.name == panelname) return ele.transform;
        }
        Debug.Log(this.transform.name + "Cant found " + panelname);
        return null;
    }
    public Transform DeActivePanel(string panelname)
    {
        foreach (var ele in ListPanels)
        {
            if (ele.name == panelname)
            {
                ele.gameObject.SetActive(false);
                return ele.transform;
            }
        }
        Debug.Log(this.transform.name + "Cant found " + panelname);
        return null;
    }

}
