using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedingMachine : MonoBehaviour
{
    #region English Comments
    /*Conveyor belt in Unity controlled with Arduino Shield by GameDevTraum
     * https://gamedevtraum.com/en/
     * https://youtube.com/c/GameDevTraum
     * Visit the page and the channel to find more solutions like this!
     * 
     * Communication between Unity and Arduino is done with a modified version of
     * "WRMHL" from relativty (https://www.relativty.net/)
     * Here you can find the original files:
     * https://github.com/relativty/wrmhl
     * 
     */
    #endregion
    #region Spanish Comments
    /*Cinta Transportadora en Unity controlada por Arduino Shield por GameDevTraum
     * https://gamedevtraum.com/es/
     * https://youtube.com/c/GameDevTraum
     * Visita la página y el canal para encontrar más soluciones como esta!
     * 
     * La comunicación entre Unity y Arduino se hace con una versión modificada de
     * "WRMHL" from relativty (https://www.relativty.net/)
     * Aquí pueden encontrar los archivos originales:
     * https://github.com/relativty/wrmhl
     * 
     */
    #endregion

    [SerializeField]
    private GameObject objectPrefab;

    [SerializeField]
    private float timeBetweenObjects;

    [SerializeField]
    private float randomTimeFactor;

    [SerializeField]
    private bool activated;

    private bool functionInvoked=false;


    // Start is called before the first frame update
    void Start()
    {
        if (activated)
        {
            //Invoke("PlaceANewObject", timeBetweenObjects + randomTimeFactor * Random.value);
    
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(activated && !functionInvoked)
        {
            //Invoke("PlaceANewObject", timeBetweenObjects + randomTimeFactor * Random.value);
            functionInvoked = true;
        }else if (!activated)
        {
            CancelInvoke();
            functionInvoked = false;
        }


    }

    public void SetActive(bool b)
    {
        activated = b;

    }

    private void PlaceANewObject()
    {
        Quaternion rotation = Quaternion.Euler(objectPrefab.transform.eulerAngles + new Vector3(0, Random.Range(-80f,80f), 0));
        Instantiate(objectPrefab, gameObject.transform.position, rotation);
        Invoke("PlaceANewObject", timeBetweenObjects + randomTimeFactor * Random.value);
    }

}
