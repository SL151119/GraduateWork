using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
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

    #region FIELDS
    public enum UVDistribution
    {
        HORIZONTAL,
        VERTICAL,
    };
    [SerializeField]
    private UVDistribution uVDistribution;

    [SerializeField]
    private float velocity;

    [SerializeField]
    private Material conveyorBeltMaterial;

    [SerializeField]
    private Transform[] measurePoints;

    [SerializeField]
    private bool activated = false;

    [SerializeField]
    private bool moveForward;

    [SerializeField]
    private float syncUVMovementFactor;

    private float distance;

    private float finalVelocity;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
        distance = Vector3.Distance(measurePoints[0].position, measurePoints[1].position);
        conveyorBeltMaterial.mainTextureOffset = new Vector2(0, 0);

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!activated)
        {
            return;
        }

        if (activated)
        {
            //The animation of the conveyor belt running is made moving the uv map in the appropriate direction
            UpdateUVMap(); 

        }
        
    }

    private void UpdateUVMap()
    {
        if (moveForward)
        {
            finalVelocity = velocity * Time.deltaTime / distance;
        }
        else
        {
            finalVelocity = -velocity * Time.deltaTime / distance;

        }

        if (uVDistribution == UVDistribution.HORIZONTAL)
        {
            conveyorBeltMaterial.mainTextureOffset -= new Vector2(finalVelocity, 0);

            if (conveyorBeltMaterial.mainTextureOffset.x < -1)
            {
                conveyorBeltMaterial.mainTextureOffset = new Vector2(conveyorBeltMaterial.mainTextureOffset.x + 1, 0);
            }
        }
        else if (uVDistribution == UVDistribution.VERTICAL)
        {
            conveyorBeltMaterial.mainTextureOffset -= new Vector2(0, finalVelocity);

            if (conveyorBeltMaterial.mainTextureOffset.y < -1)
            {
                conveyorBeltMaterial.mainTextureOffset = new Vector2(0, conveyorBeltMaterial.mainTextureOffset.y + 1);
            }
        }

    }

    public void SetActive(bool b)
    {
        #region English Comments
        //This is used to turn the machine on and off
        //It's defined as public, because we access it from the wrmhlRead script.
        //See my video about communication between scripts to understand this https://www.youtube.com/watch?v=5GbyfBSZyaE (english subtitles available)
        //There is also an article: https://gamedevtraum.com/en/programming/basic-programming/communication-between-scripts-examples-in-unity/
        #endregion
        #region Spanish Comments
        //Esto se usa para encender y apagar la máquina
        //Está definido como público porque usaremos esta función desde el Script wrmhlRead.
        //Para entender mejor esto mira mi video sobre comunicación entre Scripts  https://www.youtube.com/watch?v=5GbyfBSZyaE
        //También hay un artículo: https://gamedevtraum.com/es/programacion-informatica/introduccion-a-la-programacion/comunicacion-entre-scripts-ejemplos-en-unity/
        #endregion

        activated = b;
    }

    private void OnTriggerStay(Collider other)
    {

        if (activated)
        {
            other.gameObject.transform.position += finalVelocity*distance*syncUVMovementFactor  * Vector3.Normalize(measurePoints[1].position - measurePoints[0].position);
        }
    }


}
