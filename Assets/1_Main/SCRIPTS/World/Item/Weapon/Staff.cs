using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Staff : Weapon
{
    [SerializeField] protected Transform guntip;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadguntip();
    }
    protected void Loadguntip() {
        this.guntip = this.transform.Find("Guntip");
    }
        public override void AttackScheme()
    {
        this.StartCoroutine(this.SwingModel(180,0.1f));
    }
    protected IEnumerator SwingModel(float angle, float duration)
        {
            //this.model.transform.localPosition = new Vector3(0,0.5f,0);
            int sign = 0;
            if(this.transform.up.y >= 0) {
                sign = 1;
            }
            else sign = -1;
            Quaternion startRotation = model.transform.rotation *Quaternion.Euler(0, 0, -angle/2 * sign);
            Quaternion endRotation = model.transform.rotation * Quaternion.Euler(0, 0, angle/2 * sign);
            float elapsedTime = 0.0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                float t = elapsedTime / duration;

                model.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

                yield return new WaitForSeconds(Time.deltaTime *1f);
            }
            model.transform.localRotation = Quaternion.Euler(0,0,0);
        }
}
