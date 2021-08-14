using UnityEngine.SceneManagement;

/// <summary>
/// ƒанные дл€ сохранени€
/// </summary>
[System.Serializable] //ќб€зательно нужно указать, что класс должен сериализоватьс€
public class SaveData
{
	//—оздание полей с игровыми параметрами
	public int life;

	public int soulsCount;

	public float[] positionPlayer; //¬ Unity позици€ игрока записана с помощью класса Vector3, но его нельз€ сериализовать. „тобы обойти эту проблему, данные о позиции будут помещены в массив типа float.

	public string sceneName;
	//public State statePlayer;
	//public bool enableActionsPlayer;

	public SaveData(Player character) // онструктор класса
	{
		//ѕолучение данных, которые нужно сохранить
		life = character.life;
		soulsCount = character.soulsCount;
		sceneName = SceneManager.GetActiveScene().name;
		//statePlayer = character.state;
		//enableActionsPlayer = character.actionButtons.enableAllHard;

		positionPlayer = new float[3] //ѕолучение позиции
		{
			character.transform.position.x,
			character.transform.position.y,
			character.transform.position.z
		};
	}

}