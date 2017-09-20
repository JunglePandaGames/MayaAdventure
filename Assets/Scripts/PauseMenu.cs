using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool JanelaDosGraficos, botoesPrincipais, paused = false, mainMenu = false, back = false, gameManualBool = false;
    private float VOLUME=0.5f, SENSIBILIDADE;
    public float minimoVOLUME = 0.1f, maximoVOLUME = 1f, minimoSENS = 1, maximoSENS = 15;
    public GUISkin minhaSkin;
    public Texture RESOLUCOES, SensibilidadeMouse, QUALIDADE, Volumes, ModoJanela;
    public GUIStyle style;
    public int fonteNormal;
	public Texture gameManual;
    void Update()
    {
		AudioListener.volume = VOLUME;
        if (Input.GetKeyDown("escape"))
        {
            paused = !paused;
        }
        if(paused){

            Time.timeScale = 0;
            Cursor.visible = true;
            botoesPrincipais = true;
        }
        else
        {
            if(back == true)
            {
                back = false;
                //paused = false;
                Time.timeScale = 1;
                Cursor.visible = false;
                botoesPrincipais = false;
            }
            
            if (mainMenu)
            {
                JanelaDosGraficos = false;
                botoesPrincipais = false;
                Cursor.visible = true;
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
            }

			if (gameManualBool) {
				botoesPrincipais = false;
			} else {
				botoesPrincipais = false;
			}
        }
    }
        
    
    void Start()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1.0f;
		PlayerPrefs.SetFloat("VOLUME", VOLUME);
		Cursor.visible = false;
        botoesPrincipais = false;
        
        //==================== PREFERENCIAS =====================//
        if (PlayerPrefs.HasKey("VOLUME"))
        {
            VOLUME = PlayerPrefs.GetFloat("VOLUME");
		}
        else
        {
            PlayerPrefs.SetFloat("VOLUME", VOLUME);
			Debug.Log (PlayerPrefs.HasKey ("VOLUME"));
        }
        if (PlayerPrefs.HasKey("SENSIBILIDADE"))
        {
            SENSIBILIDADE = PlayerPrefs.GetFloat("SENSIBILIDADE");
        }
        else
        {
            PlayerPrefs.SetFloat("SENSIBILIDADE", SENSIBILIDADE);
        }
    }
    void OnGUI()
    {
        if (Cursor.visible == true)
        {
            GUI.skin = minhaSkin;

			//====================== PARTE DO MANUAL ============================//
			if (gameManualBool == true) {
				botoesPrincipais = false;
				GUI.DrawTexture(new Rect(Screen.width / 2 - Screen.width / 5, Screen.height / 2 - Screen.height / 2, Screen.width / 2, Screen.height), gameManual, ScaleMode.StretchToFill);
				if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 2.2f, Screen.height / 2 + Screen.height / 3, Screen.width / 8, Screen.height / 14), "VOLTAR"))
				{
					gameManualBool = false;
				}
			}

            //====================== PARTE PRINCIPAL DO MENU ============================//
            if (botoesPrincipais == true)
            {
				
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 16, Screen.height / 2 - Screen.height / 8, Screen.width / 8, Screen.height / 14), "BACK"))
                {

                    paused = false;
                    back = true;
                }

                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 16, Screen.height / 2 - Screen.height / 3.2f, Screen.width / 8, Screen.height / 14), "MAIN MENU"))
                {
                    //botoesPrincipais = false;
                    //JanelaDosGraficos = false;
                    mainMenu = true;
                    paused = !paused;

                    
                }
				if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 16, Screen.height / 2 - Screen.height / 4.65f, Screen.width / 8, Screen.height / 14), "INSTRUCTION"))
				{
					gameManualBool = true;
				}
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 16, Screen.height / 2 - Screen.height / 28, Screen.width / 8, Screen.height / 14), "SETTINGS"))
                {
                    if (JanelaDosGraficos == false)
                    {
                        JanelaDosGraficos = true;
                    }
                    else
                    {
                        JanelaDosGraficos = false;
                    }
                }
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 16, Screen.height / 2 + Screen.height / 18, Screen.width / 8, Screen.height / 14), "QUIT"))
                {

                    #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
                    #else
			            Application.Quit();
                    #endif
                }
            }
            //====================== PARTE GRAFICA DO MENU ============================//
            if (JanelaDosGraficos == true)
            {
                botoesPrincipais = false;
                //BOTOES
                //style.fontSize = Font.ge;
                style.normal.textColor = Color.white;
                
                GUI.Label(new Rect((Screen.width / 2 + Screen.width / 5), Screen.height / 2.25F, Screen.width / 8, Screen.height / 14), "VOLUME", style);
                GUI.Label(new Rect(Screen.width / 2 + Screen.width / 5, Screen.height / 1.8f + Screen.height / 10, Screen.width / 8, Screen.height / 14), "SENSIVILIDADE MOUSE", style);


                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 2.2f, Screen.height / 2 + Screen.height / 3, Screen.width / 8, Screen.height / 14), "VOLTAR"))
                {
                    JanelaDosGraficos = false;
                    botoesPrincipais = true;
                }
                if (GUI.Button(new Rect(Screen.width / 2 + Screen.width / 5, Screen.height / 2 - Screen.height / 4, Screen.width / 8, Screen.height / 14), "JANELA"))
                {
                    Screen.fullScreen = !Screen.fullScreen;
                }
                // SALVAR PREFERENCIAS
                if (GUI.Button(new Rect(Screen.width / 2 + Screen.width / 5, Screen.height / 2 + Screen.height / 3, Screen.width / 8, Screen.height / 14), "SALVAR PREF."))
                {
                    PlayerPrefs.SetFloat("VOLUME", VOLUME);
                    PlayerPrefs.SetFloat("SENSIBILIDADE", SENSIBILIDADE);
                }
                // BARRAS HORIZONTAIS
				VOLUME = GUI.HorizontalSlider(new Rect(Screen.width / 2 + Screen.width / 5, Screen.height / 2, Screen.width / 8, Screen.height / 14), VOLUME, minimoVOLUME, maximoVOLUME);
                SENSIBILIDADE = GUI.HorizontalSlider(new Rect(Screen.width / 2 + Screen.width / 5, Screen.height / 2 + Screen.height / 5, Screen.width / 8, Screen.height / 14), SENSIBILIDADE, minimoSENS, maximoSENS);
                //=============================== PARTE GRAFICA QUALIDADES ============================//
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 3, Screen.height / 2 - Screen.height / 4, Screen.width / 8, Screen.height / 14), "PESSIMO"))
                {
                    QualitySettings.SetQualityLevel(0);
                }
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 3, Screen.height / 2 - Screen.height / 6, Screen.width / 8, Screen.height / 14), "RUIM"))
                {
                    QualitySettings.SetQualityLevel(1);
                }
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 3, Screen.height / 2 - Screen.height / 12, Screen.width / 8, Screen.height / 14), "SIMPLES"))
                {
                    QualitySettings.SetQualityLevel(2);
                }
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 3, Screen.height / 2, Screen.width / 8, Screen.height / 14), "BOM"))
                {
                    QualitySettings.SetQualityLevel(3);
                }
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 3, Screen.height / 2 + Screen.height / 12, Screen.width / 8, Screen.height / 14), "BONITO"))
                {
                    QualitySettings.SetQualityLevel(4);
                }
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 3, Screen.height / 2 + Screen.height / 6, Screen.width / 8, Screen.height / 14), "FANTASTICO"))
                {
                    QualitySettings.SetQualityLevel(5);
                }
                //=============================== PARTE GRAFICA RESOLUÇOES ============================//
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 16, Screen.height / 2 - Screen.height / 4, Screen.width / 8, Screen.height / 14), "640x480"))
                {
                    Screen.SetResolution(640, 480, true);
                    if (minhaSkin.button.fontSize != 10 && minhaSkin.button.fontSize != 8)
                    {
                        fonteNormal = minhaSkin.button.fontSize;
                    }
                    minhaSkin.button.fontSize = 8;

                }
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 16, Screen.height / 2 - Screen.height / 6, Screen.width / 8, Screen.height / 14), "800x600"))
                {
                    Screen.SetResolution(800, 600, true);
                    if (minhaSkin.button.fontSize != 10 && minhaSkin.button.fontSize != 8)
                    {
                        fonteNormal = minhaSkin.button.fontSize;
                    }
                    minhaSkin.button.fontSize = 10;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 16, Screen.height / 2 - Screen.height / 12, Screen.width / 8, Screen.height / 14), "1024x768"))
                {
                    Screen.SetResolution(1024, 768, true);
                    minhaSkin.button.fontSize = fonteNormal;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 16, Screen.height / 2, Screen.width / 8, Screen.height / 14), "1280x720"))
                {
                    Screen.SetResolution(1280, 720, true);
                    minhaSkin.button.fontSize = fonteNormal;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 16, Screen.height / 2 + Screen.height / 12, Screen.width / 8, Screen.height / 14), "1280x800"))
                {
                    Screen.SetResolution(1280, 800, true);
                    minhaSkin.button.fontSize = fonteNormal;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 16, Screen.height / 2 + Screen.height / 6, Screen.width / 8, Screen.height / 14), "1366x768"))
                {
                    Screen.SetResolution(1366, 768, true);
                    minhaSkin.button.fontSize = fonteNormal;
                }
                //GUI.DrawTexture(new Rect(Screen.width / 2 - Screen.width / 3, Screen.height / 2 - Screen.height / 2.5f, Screen.width / 8, Screen.height / 14), QUALIDADE);
                //GUI.DrawTexture(new Rect(Screen.width / 2 - Screen.width / 16, Screen.height / 2 - Screen.height / 2.5f, Screen.width / 8, Screen.height / 14), RESOLUCOES);
                //GUI.DrawTexture(new Rect(Screen.width / 2 + Screen.width / 5, Screen.height / 2 - Screen.height / 2.5f, Screen.width / 8, Screen.height / 14), ModoJanela);
                //GUI.DrawTexture(new Rect(Screen.width / 2 + Screen.width / 5, Screen.height / 2 - Screen.height / 10, Screen.width / 8, Screen.height / 14), Volumes);
                //GUI.DrawTexture(new Rect(Screen.width / 2 + Screen.width / 5, Screen.height / 2 + Screen.height / 10, Screen.width / 8, Screen.height / 14), SensibilidadeMouse);
            }
        }
    }
}