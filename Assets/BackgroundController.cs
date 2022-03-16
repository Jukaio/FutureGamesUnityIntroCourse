using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundController : MonoBehaviour
{
    // We only scroll down
    [SerializeField] private float scrollSpeed;
    [SerializeField] private Tilemap environmentPrefab;
    List<Tilemap> tilemaps = new List<Tilemap>();

    const int INITIAL_TILEMAP_COUNT = 2;

    void Start()
    {
        // cache 3 tilemaps
        var offset = environmentPrefab.size.y * environmentPrefab.cellSize.y;
        for(int i = 0; i < INITIAL_TILEMAP_COUNT; i++) {
            var tilemap = Instantiate(environmentPrefab, transform);
            tilemap.transform.position = transform.position;
            tilemap.transform.position += Vector3.down * offset * i;
            tilemaps.Add(tilemap);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Tracking
        List<Tilemap> removeTilemaps = new List<Tilemap>();
        float offset = environmentPrefab.size.y * environmentPrefab.cellSize.y;
        float endPoint = transform.position.y - (offset * INITIAL_TILEMAP_COUNT);
        foreach (var tilemap in tilemaps) {
            if (tilemap.transform.position.y < endPoint) {
                removeTilemaps.Add(tilemap);
            }
        }

        // Checkup
        foreach (var tilemap in removeTilemaps) {
            tilemap.transform.position = transform.position;
        }

        removeTilemaps.Clear();
        // Scrolling
        foreach(var tilemap in tilemaps) {
            tilemap.transform.position += Vector3.down * Time.deltaTime * scrollSpeed;
        }

    }
}
