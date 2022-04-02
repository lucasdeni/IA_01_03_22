using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    // Criando as variáveis para o array dos waypoints e o index para conta-los
    public GameObject[] Waypoints;
    int currentWP = 0;

    // Variáveis para velocidade do objeto, velocidade de rotação e precisão de chegada ao ponto
    float speed = 1.5f;
    float accuracy = 0.6f;
    float rotSpeed = 0.2f;

    private void Start()
    {
        Waypoints = GameObject.FindGameObjectsWithTag("waypoint"); // Procura objetos com a tag waypoints
    }

    private void LateUpdate()
    {
        // Se não há nenhum waypoint, retorna
        if(Waypoints.Length == 0)
        {
            return;
        }

        // Localiza os waypoints para poder se mover até eles
        Vector3 lookAtGoal = new Vector3(Waypoints[currentWP].transform.position.x, this.transform.position.y, Waypoints[currentWP].transform.position.z);

        // É responsável pela rotação do objeto para os waypoints
        Vector3 direction = lookAtGoal - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

        // Se a física for menor que a precisão do objeto, aumenta o index
        if(direction.magnitude < accuracy)
        {
            // Index aumentado
            currentWP++;

            // Caso o index seja maior que array
            if(currentWP >= Waypoints.Length)
            {
                // Index resetado
                currentWP = 0;
            }
        }

        // Responsável pela movimentação até os Waypoints
        this.transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}
