using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//тест
public class AirPatrol : MonoBehaviour
{
    /// <summary>
    /// Первая точка патруля
    /// </summary>
    public Transform point1;

    /// <summary>
    /// Вторая точка патруля
    /// </summary>
    public Transform point2;

    /// <summary>
    /// Скорость, перемещения между точками
    /// </summary>
    public float speed = 2f;

    /// <summary>
    /// Время ожидания возле точек
    /// </summary>
    public float waintTime = 2f;

    /// <summary>
    /// Признак: должен ли двигаться
    /// </summary>
    private bool isMove = true;
    
    /// <summary>
    /// Тип патрулирования
    /// </summary>
    public enum TypePatrol
    {
        /// <summary>
        /// Патруль по вертикали
        /// </summary>
        VerticalPatrol,

        /// <summary>
        /// Патруль по горизонтали
        /// </summary>
        HorizontalPatrol,
    }

    /// <summary>
    /// Текущее тип патрулирования
    /// </summary>
    public TypePatrol typePatrol = TypePatrol.HorizontalPatrol;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(point1.transform.position.x, point1.transform.position.y, point1.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, point1.position, speed * Time.deltaTime);
        }

        if (transform.position == point1.position)
        {
            Transform swapVal = point1;
            point1 = point2;
            point2 = swapVal;
            isMove = false;
            StartCoroutine(Wainting());
            
        }
    }
    
    IEnumerator Wainting()
    {
        yield return new WaitForSeconds(waintTime);
        isMove = true;
        if(typePatrol == TypePatrol.HorizontalPatrol)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

    }
}
