using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    //Autorun PowerUp Hit
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == Constants.PlayerTag)
        {
            CharacterInput.autorun = true;
            UIManager.Instance.SetStatus(Constants.Autorun);
            StartCoroutine(autorunTimer());
        }
    }

    IEnumerator autorunTimer()
    {
        float timePassed = 0;
        while (timePassed < CharacterInput.duration)
        {
            UIManager.Instance.SetStatus(Constants.AutorunContinue);
            timePassed += Time.deltaTime;
            yield return null;
        }
        CharacterInput.autorun = false;
        UIManager.Instance.SetStatus(Constants.AutorunCease);
        Destroy(gameObject);
    }
}
