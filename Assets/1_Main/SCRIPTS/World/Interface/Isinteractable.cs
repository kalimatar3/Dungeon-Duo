using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface Iinteractable
{
    public void OnInteract(Player player);
    public Transform GetTransform();
}
