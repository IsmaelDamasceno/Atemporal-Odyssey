using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Componente respons�vel pela mec�nica de salvar a posi��o do jogador e voltar no tempo at� essa posi��o
/// </summary>
public class SavePosMember : MonoBehaviour
{
    /// <summary>
    /// Dist�ncia m�xima permitida para que o jogador possa voltar � posi��o salva
    /// </summary>
    [SerializeField] private float saveReloadDistance;

    /// <summary>
    /// Posi��o atualmente salva pelo jogador
    /// </summary>
    private Vector2? savePos;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Executado quando o jogador usa o input action "Save Pos"
    /// </summary>
    /// <param name="ctx"></param>
    public void Save(InputAction.CallbackContext ctx)
    {
		if (!savePos.HasValue)
        {
			savePos = transform.position;
		}
        else if (Vector2.Distance(transform.position, savePos.Value) < saveReloadDistance)
        {
            transform.position = savePos.Value;
            savePos = null;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (savePos.HasValue)
        {
			Gizmos.color = Vector2.Distance(savePos.Value, transform.position) < saveReloadDistance ? Color.yellow : Color.red;
			Gizmos.DrawWireSphere(savePos.Value, saveReloadDistance);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(savePos.Value, .65f);

		}
    }
#endif
}
