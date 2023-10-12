using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Componente responsável pela mecânica de salvar a posição do jogador e voltar no tempo até essa posição
/// </summary>
public class SavePosMember : MonoBehaviour
{
    /// <summary>
    /// Distância máxima permitida para que o jogador possa voltar à posição salva
    /// </summary>
    [SerializeField] private float saveReloadDistance;

    /// <summary>
    /// Posição atualmente salva pelo jogador
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
