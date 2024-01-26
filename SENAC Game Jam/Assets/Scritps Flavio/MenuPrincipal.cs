using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuPrincipal : MonoBehaviour {
    public GameObject mensagemFinal;
    
    [Header("Paineis")]
    public GameObject menu;
    public GameObject config, credits;

    [Header("Botoes")]
    public GameObject menuPri;
    public GameObject configPri, creditPri;

    private void Start()
    {
        ShowMensagemFinal(GameManager.instance.hasWonGame);
    }

    void ShowMensagemFinal(bool i)
    {
        mensagemFinal.gameObject.SetActive(i);
    }

    public void IniciarJogo()
    {
        Debug.LogWarning("Necessária lógica de carregar jogo");
        SlotManager.currentGansoSlotIndex = 0;
        SlotManager.currentPataSlotIndex = 0;
        SlotManager.turnosGansoEspera = 0;
        GameManager.instance.hasWonMinigame = true;
        SceneManager.LoadScene(SceneManagement.tabuleiroSceneIndex);

    }

    public void Configuracoes()
    {
        TrocaPainel(config);
        TrocaPrioridade(configPri);
    }

    public void Creditos()
    {
        TrocaPainel(credits);
        TrocaPrioridade(creditPri);
    }

    public void Voltar()
    {
        TrocaPainel(menu);
        TrocaPrioridade(menuPri);
    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneManagement.mainMenuSceneIndex);

    }

    public void Sair()
    {
        Debug.LogWarning("Fechando Jogo");
        Application.Quit();
    }

    private void TrocaPainel(GameObject go)
    {
        menu.SetActive(false);
        config.SetActive(false);
        credits.SetActive(false);

        go.SetActive(true);
    }

    private void TrocaPrioridade(GameObject go)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(go);
    }
}
