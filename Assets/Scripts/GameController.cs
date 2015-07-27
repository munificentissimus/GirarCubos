using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	//Texto de feedback para testes
	public TextMesh feedback;
	//Distancia minima do movimento
	public float distanciaMinimaMovimento;
	//Mapa de dados na tela
	private Dictionary<int,Vector3> dedosNaTela = new Dictionary<int,Vector3>();
	//Camera principal utilizada no raycasting
	private Camera cameraPrincipal;

	// Use this for initialization
	void Start () {
		feedback.text = "Inicio de jogo!";
		cameraPrincipal = Camera.main;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Varre todos os toques presentes na tela no fame anterior
		for (int i = 0 ; i < Input.touchCount ; i ++){
			Touch touch = Input.GetTouch(i);
			//Inicio do toque na tela
			if (touch.phase == TouchPhase.Began)
			{
				//Debug.Log("Inicio do Toque");
				feedback.text = "Inicio do Toque";

				//Registra o dedo no mapa de dedos
				dedosNaTela.Add(touch.fingerId, touch.position);
			}

			//Tratar final do toque na tela
			if (touch.phase == TouchPhase.Ended)
			{
				//A posicao final e a ultima posicao do toque
				Vector3 posicaoFinal = touch.position;

				//feedback.text = "Posicao final: " + posicaoFinal;
				//Debug.Log ( "Posicao final: " + posicaoFinal );

				//Recupera a posicao inicial do mapa de toques 
				Vector3 posicaoInicial = Vector3.zero;
				if( dedosNaTela.TryGetValue( touch.fingerId, out posicaoInicial ) )
				{
					//Apurar a distancia entre um toque e outro e somente ativar o movimento se distancia minima movimento ultrapassado
					Ray raio = cameraPrincipal.ScreenPointToRay( posicaoInicial );
					RaycastHit atingido;
					if (Physics.Raycast(raio, out atingido )){
						//Se o objeto atingido e um objeto giravel
						ObjetoGiravel objetoGiravel = atingido.transform.GetComponent<ObjetoGiravel>();

						//Se eh um objeto giravel e a distancia entre o movimento inical e final respeitou a distancia minima
						if (objetoGiravel != null && Vector3.Distance( posicaoFinal , posicaoInicial ) >= distanciaMinimaMovimento){
							feedback.text = "Distancia: " + Vector3.Distance(posicaoInicial , posicaoFinal );

							//Executa o metodo de girar na direcao especificada
							objetoGiravel.GirarNaDirecao( DirecaoService.GetDirecao( posicaoInicial , posicaoFinal ) );
						}
					}
				}
				//Remove o dedo do mapa de toques
				dedosNaTela.Remove(touch.fingerId);
			}
		}
	}
}
