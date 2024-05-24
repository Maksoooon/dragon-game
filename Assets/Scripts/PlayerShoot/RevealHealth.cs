using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealHealth : MonoBehaviour
{

    RaycastHit hit;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit))

        {
            if (hit.transform.gameObject.CompareTag("Dragon"))
            {
                hit.transform.gameObject.GetComponent<DragonMovement>().ChangeTextAlpha(1f);
            }
        }
    }
}
