using UnityEngine;
using System.IO; //Библиотек для работы с файлами
using System.Runtime.Serialization.Formatters.Binary; //Библиотека для работы бинарной сериализацией

/// <summary>
/// Сохранение и загрузка данных
/// </summary>
public static class SaveLoad //Создание статичного класса позволит использовать методы без объявления его экземпляров
{

	private static string path = Application.persistentDataPath + "/gamesave.camilgames"; //Путь к сохранению. Вы можете использовать любое расширение
	private static BinaryFormatter formatter = new BinaryFormatter(); //Создание сериализатора 

	/// <summary>
	/// Сохранить игру в файл
	/// </summary>
	/// <param name="data"></param>
	public static void SaveGame(SaveData data) //Метод для сохранения
	{

		FileStream fs = new FileStream(path, FileMode.Create); //Создание файлового потока

		//SaveData data = new SaveData(character); //Получение данных

		formatter.Serialize(fs, data); //Сериализация данных

		fs.Close(); //Закрытие потока

	}

	/// <summary>
	/// Загрузить игру из файла
	/// </summary>
	/// <returns>Сохраненные данные по игре</returns>
	public static SaveData LoadGame() //Метод загрузки
	{
		if (File.Exists(path))
		{ //Проверка существования файла сохранения
			FileStream fs = new FileStream(path, FileMode.Open); //Открытие потока

			SaveData data = formatter.Deserialize(fs) as SaveData; //Получение данных

			fs.Close(); //Закрытие потока

			return data; //Возвращение данных
		}
		else
		{
			return null; //Если файл не существует, будет возвращено null
		}

	}
}
