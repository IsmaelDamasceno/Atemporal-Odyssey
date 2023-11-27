using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public bool Interact()
    {
		DialogueSystem.InitDialogue(new string[] {
			"Prazer pessoa.. dourada?, meu nome � Anhanguera, o Espectro da tribo. ",
			"Eu era cacique de minha aldeia at� pouco tempo atr�s, por�m ap�s a chegada daqueles homens brancos, minha vila foi reduzida a quase nada.",
			"A maioria dos moradores come�aram a contrair maldi��es que acompanhavam tais homens, e foram morrendo aos poucos...",
			"Fui um dos �nicos sobreviventes, os outros foram levados para algum lugar, desconhe�o as inten��es daqueles homens, mas sei que voc� n�o est� com eles, Jaci o acompanha, e lhe protege, assim como faz conosco.",
			"Se quiser sair desta caverna, fique a vontade, por�m, recentemente Guarac� vem me avisando de um desequil�brio acima de n�s, talvez os homens brancos fizeram algo com Boitat�, um dos nossos protetores..",
			"Tenha cuidado! estarei aqui pedindo aos deuses para lhe protegerem e te guiarem na sua jornada."
		});
		return true;
	}
}
