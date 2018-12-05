using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class MapGenerator : Singleton<MapGenerator>
{
    [SerializeField] private Camera _camera = null;
    public Camera Camera
    {
        get
        {
            if (_camera == null)
                _camera = Camera.main;
            return _camera;
        }
    }

    public bool GameCanBeContinued
    {
        get { return _mapStatus != null && _mapStatus.BlockStatus.Count == (BlockGrid.x * BlockGrid.y); }
    }

    [SerializeField] private Bounds _bounds = new Bounds();

    [Space]
    [SerializeField] private Vector2Int BlockGrid = new Vector2Int(4, 3);
    [SerializeField] private List<Block> Blocks = new List<Block>();
    [SerializeField] private float _yOffset = 2f;

    [Space]
    [SerializeField] private BoxCollider2D _borderLeft = null;
    [SerializeField] private BoxCollider2D _borderRight = null;
    [SerializeField] private BoxCollider2D _borderDown = null;
    [SerializeField] private BoxCollider2D _borderUp = null;
    [SerializeField] private BoxCollider2D _borderUpRight = null;
    [SerializeField] private BoxCollider2D _borderUpLeft = null;


    [Space]
    [SerializeField] private List<Block> _generatedBlocks = new List<Block>();
    public List<Block> GeneratedBlocks { get { return _generatedBlocks; } }


    [SerializeField] private MapStatus _mapStatus = null;
    public MapStatus MapStatus { get { return _mapStatus; } }

    [Space]
    public UnityEvent MapGenerationStart = new UnityEvent();
    public UnityEvent MapGenerated = new UnityEvent();
    public UpdateBaunds UpdateBaundsCallBack = new UpdateBaunds();
    public UnityEvent AllBlocksDisabledCallback = new UnityEvent();

    private void CreateBorderCollider(BoxCollider2D collider, Vector2 position, Vector2 colliderSize, Quaternion rotation, bool ignoreOffset = false)
    {
        if (collider == null)
            return;

        collider.gameObject.transform.SetParent(this.transform);
        collider.gameObject.transform.localPosition = new Vector2(position.x, position.y);

        collider.size = colliderSize;

        Vector2 offest = Vector2.zero;
        offest = collider.offset;

        if(!ignoreOffset)
        {
            if (Math.Abs(collider.size.x) < Math.Abs(collider.size.y))
                offest.x = (collider.size.x / 2);
            else
                offest.y = (collider.size.y / 2);

            offest.x = position.x > 0 ? offest.x : offest.x * -1;
            offest.y = position.y > 0 ? offest.y : offest.y * -1;
        }

        collider.offset = offest;
        collider.gameObject.transform.rotation = rotation;
    }

    protected override void Awake()
    {
        base.Awake();

        transform.position = Camera.transform.position;
        _bounds = new Bounds(Camera.transform.position, CalculateCameraBounds());

        UpdateBaundsCallBack.Invoke(_bounds.Position, _bounds.Size);

        CreateBorderCollider(_borderLeft, new Vector2(_bounds.MaxX, 0), new Vector2(.2f, _bounds.Dimensions.y), Quaternion.identity);
        CreateBorderCollider(_borderRight, new Vector2(_bounds.MinX, 0), new Vector2(.2f, _bounds.Dimensions.y), Quaternion.identity);
        CreateBorderCollider(_borderDown, new Vector2(0, _bounds.MinY), new Vector2(_bounds.Dimensions.x, .2f), Quaternion.identity);
        CreateBorderCollider(_borderUp, new Vector2(0, _bounds.MaxY), new Vector2(_bounds.Dimensions.x, .2f), Quaternion.identity);
        CreateBorderCollider(_borderUpRight, new Vector2(_bounds.MaxX, _bounds.MaxY), Vector2.one * .5f, Quaternion.Euler(0, 0, 45), true);
        CreateBorderCollider(_borderUpLeft, new Vector2(_bounds.MinX, _bounds.MaxY), Vector2.one * .5f, Quaternion.Euler(0, 0, -45), true);
    }

    public void GenerateNewSeed()
    {
        if (_mapStatus != null)
            _mapStatus.GenerateSeed();
    }

    private Vector2 CalculateCameraBounds()
    {
        Vector2 size = Vector2.zero;
        size.y = Camera.orthographicSize;
        size.x = size.y * Screen.width / Screen.height;
        return size;
    }

    public void GenerateMap()
    {
        if (_mapStatus != null)
            MapGeneration();
    }

    private void MapGeneration()
    {
        MapGenerationStart.Invoke();

        UnityEngine.Random.InitState(_mapStatus.Seed);

        for (int i = 0; i < BlockGrid.y; i++)
        {
            float x = -((float)BlockGrid.x / 2f);
            float y = 0;
            for (int j = 0; j < BlockGrid.x; j++)
            {
                int index = UnityEngine.Random.Range(0, Blocks.Count);
                Vector2 size = Blocks[index].Size;
                if (size.y > y)
                    y = size.y;
                GameObject instance = Instantiate(Blocks[index].gameObject);
                instance.transform.SetParent(this.transform);
                GeneratedBlocks.Add(instance.GetComponent<Block>());
                instance.transform.position = new Vector3(x, (y * i) + _yOffset, 0);
                x += size.x;
            }
        }

        MapGenerated.Invoke();
    }

    public void ClearMap()
    {
        for (int i = 0; i < GeneratedBlocks.Count; i++)
            Destroy(GeneratedBlocks[i].gameObject);

        GeneratedBlocks.Clear();
    }

    public void SaveMapStatus()
    {
        if(_mapStatus != null)
        {
            _mapStatus.BlockStatus.Clear();
            foreach (var item in GeneratedBlocks)
                _mapStatus.BlockStatus.Add(item.gameObject.activeSelf);
        }
    }
    
    public void RestoreMapStatus()
    {
        if(_mapStatus != null)
            for (int i = 0; i < _mapStatus.BlockStatus.Count; i++)
                GeneratedBlocks[i].gameObject.SetActive(_mapStatus.BlockStatus[i]);
    }

    public void ClearMapStatus()
    {
        if (_mapStatus != null)
            _mapStatus.BlockStatus.Clear();
    }

    public void BlockDisabled(GameObject blockGameObject)
    {
        Block block = blockGameObject.GetComponent<Block>();

        if (block == null || _mapStatus == null)
            return;

        int index = GeneratedBlocks.IndexOf(block);
        _mapStatus.BlockStatus[index] = block.gameObject.activeSelf;

        if (AllBlocksDisabled())
            AllBlocksDisabledCallback.Invoke();
    }

    public bool AllBlocksDisabled()
    {
        foreach (var item in _generatedBlocks)
            if (item.gameObject.activeSelf)
                return false;

        return true;
    }
}