using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public TextMesh feedback;
	private Vector3[] posicoesIniciais = new Vector3[20];
	private Vector3[] posicoesFinais = new Vector3[20];
	private Camera cameraPrincipal;

	// Use this for initialization
	void Start () {
		feedback.text = "Jogo começou";
		cameraPrincipal = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("atualizando quadros");
		for (int i = 0 ; i < Input.touchCount ; i ++){
			Debug.Log("Toques detectados");
			Touch touch = Input.GetTouch(i);
			if (touch.phase == TouchPhase.Began)
			{
				feedback.text = "Inicio de Toque: " + i;
				posicoesIniciais[i] = touch.position;
			}

			if (touch.phase == TouchPhase.Ended)
			{
				feedback.text = "Final de Toque: " + i;
				posicoesFinais[i] = touch.position;

				Ray raio = cameraPrincipal.ScreenPointToRay(posicoesIniciais[i]);
				RaycastHit atingido;
				if (Physics.Raycast(raio, out atingido )){
					//Se o objeto atingido e um objeto giravel
					ObjetoGiravel objetoGiravel = atingido.transform.GetComponent<ObjetoGiravel>();
					if (objetoGiravel != null){
						feedback.text = "Girar objeto";
						objetoGiravel.GirarNaDirecao(DirecaoService.GetDirecao(posicoesIniciais[i] , posicoesFinais[i]));
					}
				}
			}
		}
	}
}
