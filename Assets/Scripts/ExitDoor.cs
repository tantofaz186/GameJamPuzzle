using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private List<GenericEventTrigger> gottenKeys;
    private bool isOpen = false;
    [SerializeField] private int goalKeyAmmount;
    [SerializeField] private Animator animController;




    // Start is called before the first frame update
    void Start()
    {
        gottenKeys = new List<GenericEventTrigger>();
        LevelControl.Instance.CallAnnouncer("Encontre " + goalKeyAmmount + " chaves para abrir a saída!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KeyUp(GenericEventTrigger target)
    {
        if(!gottenKeys.Contains(target))gottenKeys.Add(target);

        if (gottenKeys.Count >= goalKeyAmmount && !isOpen)
        {
            OpenTheGates(true);
            LevelControl.Instance.CallAnnouncer("Porta aberta!");

        }
        else
        {

            LevelControl.Instance.CallAnnouncer("Chave Coletada! Restam " + (goalKeyAmmount - gottenKeys.Count) + " chaves para abrir a saída!");
        }
    }

    public void ResetKeys()
    {
        if (gottenKeys.Count <= 0) return;
        foreach(GenericEventTrigger a in gottenKeys)
        {
            a.CallEvent(1);
        }
        gottenKeys = new List<GenericEventTrigger>();
        if (isOpen) OpenTheGates(false);

        LevelControl.Instance.CallAnnouncer("Chaves retornadas a posição original! Restam " + (goalKeyAmmount - gottenKeys.Count) + " chaves para abrir a saída!");
    }


    public void OpenTheGates(bool state)
    {
        isOpen = state;
        transform.GetComponent<Collider2D>().enabled = state;
        animController.SetBool("OpenState", state);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isOpen && gottenKeys.Count >= goalKeyAmmount)
        {
            LevelControl.Instance.Victory();
        }
    }
}
