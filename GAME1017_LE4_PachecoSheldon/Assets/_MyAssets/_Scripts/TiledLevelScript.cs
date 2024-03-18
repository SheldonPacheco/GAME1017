using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TiledLevelScript : MonoBehaviour
{
    [SerializeField] private Tilemap[] tileMaps;
    // TODO: In the Inspector, remove all references in the tileBases array, set size to 0.
    [SerializeField] private TileBase[] tileBases; // Next time, loading this will be dynamic.
    [SerializeField] private char[] tileKeys;
    [SerializeField] private char[] tileObstacles;
    // TODO: Remove the initialized values for rows and cols.
    private int rows = 24; // Y-axis.
    private int cols = 32; // X-axis.

    // TODO: Add new fields for the color-changing background functionality.
    //
    //
    //
    // TODO: Also add the reference to the camera.

    void Start()
    {
        // TODO: Set the camera to the main one.
        // TODO: Start the color change coroutine.
        LoadLevel();
    }

    private void LoadLevel()
    {
        try
        {
            // TODO: Create the function for loading and sorting tile bases.
            // Load tile data first.
            using (StreamReader reader = new StreamReader("Assets/TileData.txt"))
            {
                // Read all tile chars and create an array from it.
                string line = reader.ReadLine();
                tileKeys = line.ToCharArray();
                // Next is the obstacle tiles.
                line = reader.ReadLine();
                tileObstacles = line.ToCharArray();
                // We can also do the hazards. Next time.
            }
            // Then load level data.
            using (StreamReader reader = new StreamReader("Assets/Level1.txt"))
            {
                // TODO: Add call to GetRowsAndColumns method.
                string line;
                for (int row = 1; row < rows+1; row++)
                {
                    line = reader.ReadLine();
                    for (int col = 0; col < cols; col++)
                    {
                        char c = line[col];
                        if (c == '*') continue; // Skip if sky tile.

                        int charIndex = Array.IndexOf(tileKeys, c);
                        if (charIndex == -1) throw new Exception("Index not found.");
                        // Check if tile is obstacle or normal.
                        if (Array.IndexOf(tileObstacles, c) > -1) // Tile is obstacle.
                        {
                            SetTile(0, charIndex, col, row);
                        }
                        else // Tile is normal.
                        {
                            SetTile(1, charIndex, col, row);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    // TODO: Add GetRowsAndColumns method here.

    private void SetTile(int tileMapIndex, int charIndex, int col, int row)
    {
        // Check all tilemaps to see if there's a manually-painted tile there.
        foreach (Tilemap tilemap in tileMaps)
        {
            if (tilemap.HasTile(new Vector3Int(col, -row, 0))) return;
        }
        // If no tile, then set the tile in the desired tilemap.
        tileMaps[tileMapIndex].SetTile(new Vector3Int(col, -row, 0), tileBases[charIndex]);
    }

    // TODO: Add LoadAndSortTileBases method here.

    // TODO: Add ExtractNumber helper method here.

    // TODO: Add ChangeBackgroundColor method here.
}
