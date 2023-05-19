using System;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    /// <summary>
    /// Данный метод вызывается автоматически при клике на кнопки с изображениями тайлов.
    /// В качестве параметра передается префаб тайла, изображенный на кнопке.
    /// Вы можете использовать префаб tilePrefab внутри данного метода.
    /// </summary>
    private Transform _position;

    private Camera _camera;

    [SerializeField] private Tile _tile;
    [SerializeField] private Map _map;

    public void StartPlacingTile(GameObject tilePrefab)
    {
        var tile = Instantiate(tilePrefab);
        _tile = tile.GetComponent<Tile>();
        _tile.transform.SetParent(_map.transform);
    }

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo) && _tile != null)
        {
            var pos = hitInfo.point;
            var x = Mathf.FloorToInt(pos.x);
            var z = Mathf.FloorToInt(pos.z);
            var halfMapSize = _map.Size / 2; ;
            Vector2Int position = new Vector2Int(x, z) + halfMapSize;
            Vector2Int index = position - halfMapSize + Vector2Int.one;
            Vector3 tilePosition = new Vector3(index.x, 0, index.y);
            _tile.transform.position = tilePosition;
            _tile.SetColor(_map.IsCellAvailable(index));
            
            
            if (Input.GetMouseButtonDown(0))
            {
                if (_map.IsCellAvailable(index))
                {
                    _map.SetTile(index, _tile);
                    _tile.ResetColor();
                    _tile = null;
                }
            }
        }
    }
}