using UnityEngine;

public class MenuNavigation : MonoBehaviour {

	public void MainMenu()
	{
		Application.LoadLevel("menu");
	}

	public void Quit()
	{
		Application.Quit();
	}
	
	public void Play()
	{
		Application.LoadLevel("game");
	}

	public void SourceCode()
	{
		Application.OpenURL("https://github.com/Scorr/pacman-jam2016");
	}
}
