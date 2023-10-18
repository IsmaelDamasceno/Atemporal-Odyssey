using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
	/// <summary>
	/// Componente responsável pela mecânica de salvar a posição do jogador e voltar no tempo até essa posição
	/// </summary>
	public class SavePosMember : MonoBehaviour
	{
		/// <summary>
		/// Distância máxima permitida para que o jogador possa voltar à posição salva
		/// </summary>
		[SerializeField] private float loadDistance;

		/// <summary>
		/// Posição atualmente salva pelo jogador
		/// </summary>
		private Vector2? savePos;

		/// <summary>
		/// Executado quando o jogador usa o input action "Load Pos"
		/// </summary>
		/// <param name="ctx"></param>
		public void Load(InputAction.CallbackContext ctx)
		{
			if (!savePos.HasValue)
			{
				return;
			}

			if (Vector2.Distance(transform.position, savePos.Value) < loadDistance)
			{
				transform.position = savePos.Value;
			}
		}

		/// <summary>
		/// Executado quando o jogador usa o input action "Save Pos"
		/// </summary>
		/// <param name="ctx"></param>
		public void Save(InputAction.CallbackContext ctx)
		{
			savePos = transform.position;
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			if (savePos.HasValue)
			{
				Gizmos.color = Vector2.Distance(savePos.Value, transform.position) < loadDistance ? Color.yellow : Color.red;
				Gizmos.DrawWireSphere(savePos.Value, loadDistance);

				Gizmos.color = Color.blue;
				Gizmos.DrawSphere(savePos.Value, .65f);

			}
		}
#endif
	}
}
