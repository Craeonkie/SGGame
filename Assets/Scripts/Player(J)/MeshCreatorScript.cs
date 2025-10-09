using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MeshCreatorScript : MonoBehaviour
{
   [Header("Add in the model / collider to make a mesh of:")]
    public Tilemap obj;
    public GameObject collisionPrefab;
    private void Start()
    {
        createCollision();
    }
    public void createCollision()
    {
        //allPositionsWithin will return all the pos of the tiles in the tilemap boundary
        foreach (var cellpos in obj.cellBounds.allPositionsWithin)
        {
            var worldPos = obj.CellToWorld(cellpos);
            Debug.Log("cellWORLD pos: " + obj.CellToWorld(cellpos));
            if (!obj.HasTile(cellpos))
            {
                continue;
            }
            var box = Instantiate(collisionPrefab, cellpos, Quaternion.identity);
            box.transform.SetParent(transform);
            box.transform.position = worldPos + new Vector3(1.5f, 0, 1.5f);
        }
    }
}
