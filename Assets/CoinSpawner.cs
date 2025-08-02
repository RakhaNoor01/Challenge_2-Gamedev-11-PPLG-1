using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    // The coin prefab to spawn
    public GameObject coinPrefab;
    
    // The plane where coins will be placed
    public Transform plane;
    
    // How many coins to create
    public int numberOfCoins = 20;

    void Start()
    {
        // Get the size of the plane from its mesh renderer
        Vector3 planeSize = plane.GetComponent<MeshRenderer>().bounds.size;
        float planeX = planeSize.x / 2f;
        float planeZ = planeSize.z / 2f;

        // Loop to create the specified number of coins
        for (int i = 0; i < numberOfCoins; i++)
        {
            // Generate a random position on the plane
            float randomX = Random.Range(-planeX, planeX);
            float randomZ = Random.Range(-planeZ, planeZ);

            // The spawn position is the random X/Z plus the plane's center position
            Vector3 spawnPosition = new Vector3(randomX, 0.5f, randomZ) + plane.position;
            
            // Create the coin at the random position
            Instantiate(coinPrefab, spawnPosition, Quaternion.Euler(-90f, 0f, 0f));
        }
    }
}