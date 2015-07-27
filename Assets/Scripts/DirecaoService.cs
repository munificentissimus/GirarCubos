using UnityEngine;
using System.Collections;

/**
Servico de obtencao de direcao de um movimento
 */
public class DirecaoService : MonoBehaviour
{
	/*
	A partir de uma posicao inicial e final define a direcao do movimento.
	 */
	public static Direcao GetDirecao(Vector3 posicaoInicial, Vector3 posicaoFinal )
	{
		//Apura a tendencia horizontal (distancia percorrida no eixo x)
		float tendenciaHorizontal = Mathf.Abs(posicaoFinal.x - posicaoInicial.x);
		//Apura a tendencia vertical (distancia percorrida no eixo y)
		float tendenciaVertical = Mathf.Abs(posicaoFinal.y - posicaoInicial.y);

		//Movimento horizontal desligado
		bool movimentoHorizontal = false;

		//Movimento vertical desligado
		bool movimentoVertical = false;

		//Escolhe qual movimento prevalecel a partir da tendencia
		if (tendenciaHorizontal > tendenciaVertical)
		{
			movimentoHorizontal = true;
		} else {
			movimentoVertical = true;
		}

		if (movimentoHorizontal && ( posicaoFinal.x > posicaoInicial.x) )
		{//Se movimento horizontal e eixo x aumentou seu valor >> direita
			return Direcao.DIREITA;
		} else if (movimentoHorizontal && ( posicaoFinal.x < posicaoInicial.x ) )
		{ //Se movimento horizontal e eixo x diminuiu se valor >> esquerda
			return Direcao.ESQUERDA;
		} else if (movimentoVertical && ( posicaoFinal.y > posicaoInicial.y) )
		{ //Se movimento vertical e eixo y aumentou seu valor >> cima
			return Direcao.CIMA;
		} else // if (movimentoVertical && ( posicaoFinal.y < posicaoInicial.y) )
		{ //Se movimento vertical e eixo y diminuiu seu valor >> baixo
			return Direcao.BAIXO;
		} 
	}
}
