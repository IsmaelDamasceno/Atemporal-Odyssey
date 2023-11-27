using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public bool Interact()
    {
		DialogueSystem.InitDialogue(new string[] {
			"Prazer pessoa.. dourada?, meu nome é Anhanguera, o Espectro da tribo. ",
			"Eu era cacique de minha aldeia até pouco tempo atrás, porém após a chegada daqueles homens brancos, minha vila foi reduzida a quase nada.",
			"A maioria dos moradores começaram a contrair maldições que acompanhavam tais homens, e foram morrendo aos poucos...",
			"Fui um dos únicos sobreviventes, os outros foram levados para algum lugar, desconheço as intenções daqueles homens, mas sei que você não está com eles, Jaci o acompanha, e lhe protege, assim como faz conosco.",
			"Se quiser sair desta caverna, fique a vontade, porém, recentemente Guarací vem me avisando de um desequilíbrio acima de nós, talvez os homens brancos fizeram algo com Boitatá, um dos nossos protetores..",
			"Tenha cuidado! estarei aqui pedindo aos deuses para lhe protegerem e te guiarem na sua jornada."
		});
		return true;
	}
}
