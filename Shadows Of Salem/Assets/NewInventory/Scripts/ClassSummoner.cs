using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClassSummoner : MonoBehaviour
{
    [HideInInspector]public Transform summonOrigin;
    // Start is called before the first frame update
    void Start()
    {
        SummonParentClasses(summonOrigin); 
    }
    private void SummonParentClasses(Transform summonOrigin) 
    {
        // Obtiene componentes de bloqueo y dependencias del objeto
        Key keyClass = summonOrigin.gameObject.GetComponent<Key>();
        ObjectCombination objectCombinationClass = summonOrigin.gameObject.GetComponent<ObjectCombination>();
        PopUpManager popUpManagerClass = summonOrigin.gameObject.GetComponent<PopUpManager>();
        // Si el objeto está bloqueado, muestra mensaje y devuelve falso
        if (keyClass != null)
        {
            Key thisKey = this.gameObject.AddComponent<Key>();
            thisKey.keyID = keyClass.keyID;
        }
        if (objectCombinationClass != null)
        {
            ObjectCombinationInInventory thisObjectCombinationInInventory = this.gameObject.AddComponent<ObjectCombinationInInventory>();
            thisObjectCombinationInInventory.objetosCombinables = objectCombinationClass.objetosCombinables;
        }
        if (popUpManagerClass != null)
        {
            PopUpManager thisPopUpManager = this.gameObject.AddComponent<PopUpManager>();
        }
    }  
}
