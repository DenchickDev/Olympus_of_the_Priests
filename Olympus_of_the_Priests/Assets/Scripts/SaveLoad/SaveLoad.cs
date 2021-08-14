using UnityEngine;
using System.IO; //��������� ��� ������ � �������
using System.Runtime.Serialization.Formatters.Binary; //���������� ��� ������ �������� �������������

/// <summary>
/// ���������� � �������� ������
/// </summary>
public static class SaveLoad //�������� ���������� ������ �������� ������������ ������ ��� ���������� ��� �����������
{

	private static string path = Application.persistentDataPath + "/gamesave.camilgames"; //���� � ����������. �� ������ ������������ ����� ����������
	private static BinaryFormatter formatter = new BinaryFormatter(); //�������� ������������� 

	/// <summary>
	/// ��������� ���� � ����
	/// </summary>
	/// <param name="data"></param>
	public static void SaveGame(SaveData data) //����� ��� ����������
	{

		FileStream fs = new FileStream(path, FileMode.Create); //�������� ��������� ������

		//SaveData data = new SaveData(character); //��������� ������

		formatter.Serialize(fs, data); //������������ ������

		fs.Close(); //�������� ������

	}

	/// <summary>
	/// ��������� ���� �� �����
	/// </summary>
	/// <returns>����������� ������ �� ����</returns>
	public static SaveData LoadGame() //����� ��������
	{
		if (File.Exists(path))
		{ //�������� ������������� ����� ����������
			FileStream fs = new FileStream(path, FileMode.Open); //�������� ������

			SaveData data = formatter.Deserialize(fs) as SaveData; //��������� ������

			fs.Close(); //�������� ������

			return data; //����������� ������
		}
		else
		{
			return null; //���� ���� �� ����������, ����� ���������� null
		}

	}
}
