using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sword : Weapon
{
    public override void AttackScheme()
    {
        this.StartCoroutine(this.SwingModel(180,0.1f));
    }
    protected IEnumerator SwingModel(float angle, float duration)
        {
            this.model.transform.localPosition = new Vector3(0,0.5f,0);
            int sign = 0;
            if(this.transform.up.y >= 0) {
                sign = 1;
            }
            else sign = -1;
            Quaternion startRotation = transform.rotation *Quaternion.Euler(0, 0, -angle/2 * sign);
            Quaternion endRotation = transform.rotation * Quaternion.Euler(0, 0, angle/2 * sign);

            float elapsedTime = 0.0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                float t = elapsedTime / duration;

                transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

                yield return new WaitForSeconds(Time.deltaTime *1f);
            }
            transform.localRotation = Quaternion.Euler(0,0,0);
        }
}
