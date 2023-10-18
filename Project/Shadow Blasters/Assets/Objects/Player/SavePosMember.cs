using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
	/// <summary>
	/// Componente respons�vel pela mec�nica de salvar a posi��o do jogador e voltar no tempo at� essa posi��o
	/// </summary>
	public class SavePosMember : MonoBehaviour
	{
		/// <summary>
		/// Dist�ncia m�xima permitida para que o jogador possa voltar � posi��o salva
		/// </summary>
		[SerializeField] private float loadDistance;

		/// <summary>
		/// Posi��o atualmente salva pelo jogador
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
