using UnityEngine.SceneManagement;

/// <summary>
/// ������ ��� ����������
/// </summary>
[System.Serializable] //����������� ����� �������, ��� ����� ������ ���������������
public class SaveData
{
	//�������� ����� � �������� �����������
	public int life;

	public int soulsCount;

	public float[] positionPlayer; //� Unity ������� ������ �������� � ������� ������ Vector3, �� ��� ������ �������������. ����� ������ ��� ��������, ������ � ������� ����� �������� � ������ ���� float.

	public string sceneName;
	//public State statePlayer;
	//public bool enableActionsPlayer;

	public SaveData(Player character) //����������� ������
	{
		//��������� ������, ������� ����� ���������
		life = character.life;
		soulsCount = character.soulsCount;
		sceneName = SceneManager.GetActiveScene().name;
		//statePlayer = character.state;
		//enableActionsPlayer = character.actionButtons.enableAllHard;

		positionPlayer = new float[3] //��������� �������
		{
			character.transform.position.x,
			character.transform.position.y,
			character.transform.position.z
		};
	}

}