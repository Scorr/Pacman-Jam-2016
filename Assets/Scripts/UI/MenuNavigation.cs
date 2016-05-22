using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour {

	public void MainMenu()
	{
		SceneManager.LoadScene("menu");
	}

	public void Quit()
	{
		Application.Quit();
	}
	
	public void Play()
	{
        SceneManager.LoadScene("game");
	}

	public void SourceCode()
	{
		Application.OpenURL("https://github.com/Scorr/pacman-jam2016");
	}
}
