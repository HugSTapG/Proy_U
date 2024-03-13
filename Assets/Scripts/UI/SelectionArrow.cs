using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField]private RectTransform[] options;
    private RectTransform rect;
    private int currentPosition;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    private void Update()
    {
        //Cambiar posicion de la flecha
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePosition(1);
        }
        //Interactuar con las opciones
        if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Q))
        {
            Interact();
        }
        
    }
    private void ChangePosition(int _change)
    {
        currentPosition += _change;
        if(currentPosition < 0)
        {
            currentPosition = options.Length - 1;
        }
        else if(currentPosition > options.Length - 1)
        {
            currentPosition = 0;
        }
        //Posicion Y de la opcion a la flecha
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }
    private void Interact()
    {
        //Uso del boton de cada opcion la funcion de este
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
