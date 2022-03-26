using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericEventTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private UnityEvent[] EventList; // Lista de Eventos a ser chamado

    [SerializeField]
    private bool shouldTriggerColiderCall; // Caso seja true, o método OnTriggerEnter() ira chamar um evento

    [SerializeField]
    private string[] collisionTagsToCompare;  // Caso maior que 0, o OnTriggerEnter() irá verificar se o objeto colidido tem uma das tags, caso vazio o OnTriggerEnter() Utiliza somente a prop anterior

    [SerializeField] 
    private int collisionTriggerCallKey; // Diz qual chave deverá ser utilizada pelo metodo OnTriggerEnter()
    public int CollisionTriggerCallKey { get => collisionTriggerCallKey; set => collisionTriggerCallKey = value; } // Acessor publico da chave utilizada pelo OnTriggerEnter()

    [SerializeField]
    private int collisionExitCallKey; // Diz qual chave deverá ser utilizada pelo metodo OnTriggerEnter()
    public int CollisionExitCallKey { get => collisionExitCallKey; set => collisionExitCallKey = value; } // Acessor publico da chave utilizada pelo OnTriggerEnter()




    public void CallEvent (int EventKey)
    {
        if (EventList.Length >= EventKey)
        {
            EventList[EventKey].Invoke();
        }
        else
        {
            Debug.Log("Não existe um evento de chave [" + EventKey + "] neste objeto!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("A");
        bool isCallable = false;

        if (shouldTriggerColiderCall)
        {
            if(collisionTagsToCompare.Length > 0)
            {
                foreach (string tag in collisionTagsToCompare)
                {
                    if (other.tag == tag)
                    {
                        isCallable = true;
                    }
                }
            }
            else
            {
                isCallable = true;
            }

            if(isCallable) CallEvent(collisionTriggerCallKey);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("B");
        bool isCallable = false;

        if (shouldTriggerColiderCall)
        {
            if (collisionTagsToCompare.Length > 0)
            {
                foreach (string tag in collisionTagsToCompare)
                {
                    if (other.tag == tag)
                    {
                        isCallable = true;
                    }
                }
            }
            else
            {
                isCallable = true;
            }

            if (isCallable) CallEvent(collisionExitCallKey);

        }
    }

    public void UpgradeKey()
    {
        collisionTriggerCallKey++;
    }

}
