using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dumping = 1.5f; // сглаживание камеры.
    public Vector2 offset = new Vector2(2f, 1f); // смещение камеры относительно персонажа.
    public bool isLeft; // ппроверка повората персонажа, то, куда он смотрит.
    private Transform player; // определение уровня персонажа.
    private int lastX; //куда смтрит персонаж, последнее положение камеры.
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y); //математичекое вычисление положения камеры, камера будет справа и сверху.
        FindPlayer(isLeft);
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            int currentX = Mathf.RoundToInt(player.position.x); //считывание положение камеры по оси "х", относительно персонажа.
            if (currentX > lastX) //больше последнего положения по иксу.
            {
                isLeft = false; //значит мы не смотрим влево
            }
            else
            {
                if (currentX < lastX) // меньше последнего положения по иксу.
                {
                    isLeft = true; //персонаж смотрит влево, или же не смотрит вправо, что логично.
                }
            }
            lastX = Mathf.RoundToInt(player.position.x); //математическое вычисленине положения игрока.

            Vector3 target;
            if (isLeft) // если влево
            {
                target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z); //то камера перемещается влево.
            }
            else
            {
                target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z); //иначе вправо.
            }

            Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }
    public void FindPlayer(bool playerIsLeft) //метод проверки поворота.
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //находим персонажа с тегом "player";
        lastX = Mathf.RoundToInt(player.position.x); //работа по оси "х".
        if (playerIsLeft)
        {
            transform.position = new Vector3(player.position.x - offset.x, player.position.y - offset.y, transform.position.z); /* если персонаж
            смотрит влево, то и камера смещается относительно персонажа в сторону куда он смотрит и немного вверх. 
            */
        }
        else
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);/* если враво,
            то камера смещается вправо*/
        }


    }
}
