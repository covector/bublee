// https://github.com/kchapelier/fast-2d-poisson-disk-sampling/blob/master/src/fast-poisson-disk-sampling.js translated to C#
using System.Collections.Generic;
using UnityEngine;

public class FastPoissonDiskSampling
{
    const float PIDIV3 = Mathf.PI / 3;
    static float SQRT1_2 = Mathf.Sqrt(0.5f);
    struct Point
    {
        public float x;
        public float y;
        public float angle;
        public int tries;
        public Point(float x, float y, float angle, int tries)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;
            this.tries = tries;
        }
    }
    struct Grid
    {
        public int strideX;
        public int[] data;
        public Grid(Vector2Int shape)
        {
            this.strideX = shape[1];
            this.data = new int[shape[0] * shape[1]];
            for (int i = 0; i < data.Length; i++)
            {
                this.data[i] = 0;
            }
        }
    }

    static Vector2Int[] neighbourhood = new Vector2Int[] {
        new Vector2Int(0, 0), new Vector2Int(0, -1),new Vector2Int(-1, 0),
        new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(-1, -1),
        new Vector2Int(1, -1),new Vector2Int(-1, 1),new Vector2Int(1, 1),
        new Vector2Int(0, -2),new Vector2Int(-2, 0),new Vector2Int(2, 0),
        new Vector2Int(0, 2), new Vector2Int(-1, -2), new Vector2Int(1, -2),
        new Vector2Int(-2, -1), new Vector2Int(2, -1),new Vector2Int(-2, 1),
        new Vector2Int(2, 1), new Vector2Int(-1, 2),new Vector2Int(1, 2)
    };

    static int neighbourhoodLength = neighbourhood.Length;

    protected float width;
    protected float height;
    protected float radius;
    int maxTries;
    float floatPrecisionMitigation;
    float epsilonRadius;
    float epsilonAngle;
    float radiusPlusEpsilon;
    float squaredRadius;
    float cellSize;
    float angleIncrement;
    float angleIncrementOnSuccess;
    int triesIncrementOnSuccess;
    protected System.Random random;
    List<Point> processList;
    List<Vector2> samplePoints;
    Vector2Int gridShape;
    Grid grid;
    public FastPoissonDiskSampling(float width, float height, float radius, int maxTries = 30, int seed = 0)
    {
        this.width = width;
        this.height = height;
        this.radius = radius;
        this.maxTries = maxTries;
        this.floatPrecisionMitigation = Mathf.Max(1, Mathf.FloorToInt(Mathf.Max(this.width, this.height) / 64));
        this.epsilonRadius = 1e-14f * floatPrecisionMitigation;
        this.epsilonAngle = 2e-14f;
        this.squaredRadius = this.radius * this.radius;
        this.radiusPlusEpsilon = this.radius + epsilonRadius;
        this.cellSize = this.radius * SQRT1_2;
        this.angleIncrement = Mathf.PI * 2 / this.maxTries;
        this.angleIncrementOnSuccess = PIDIV3 + epsilonAngle;
        this.triesIncrementOnSuccess = Mathf.CeilToInt(this.angleIncrementOnSuccess / this.angleIncrement);
        this.processList = new List<Point>();
        this.samplePoints = new List<Vector2>();
        this.gridShape = new Vector2Int(
            Mathf.CeilToInt(this.width / this.cellSize),
            Mathf.CeilToInt(this.height / this.cellSize)
        );
        this.grid = new Grid(gridShape);
        this.random = new System.Random(seed);
    }

    float rng()
    {
        return (float)this.random.NextDouble();
    }

    Vector2 addRandomPoint()
    {
        return this.directAddPoint(new Point(
            this.rng() * this.width,
            this.rng() * this.height,
            this.rng() * Mathf.PI * 2,
            0
        ));
    }

    Vector2 directAddPoint(Point point)
    {
        Vector2 coordsOnly = new Vector2(point.x, point.y);
        this.processList.Add(point);
        this.samplePoints.Add(coordsOnly);

        int internalArrayIndex = Mathf.FloorToInt(point.x / this.cellSize) * this.grid.strideX + Mathf.FloorToInt(point.y / this.cellSize);

        this.grid.data[internalArrayIndex] = this.samplePoints.Count; // store the point reference

        return coordsOnly;
    }

    bool inNeighbourhood(Point point)
    {
        int strideX = this.grid.strideX;
        int boundX = this.gridShape[0];
        int boundY = this.gridShape[1];
        int cellX = Mathf.FloorToInt(point.x / this.cellSize);
        int cellY = Mathf.FloorToInt(point.y / this.cellSize);
        int neighbourIndex;
        int internalArrayIndex;
        int currentDimensionX;
        int currentDimensionY;
        Vector2 existingPoint;

        for (neighbourIndex = 0; neighbourIndex < neighbourhoodLength; neighbourIndex++)
        {
            currentDimensionX = cellX + neighbourhood[neighbourIndex].x;
            currentDimensionY = cellY + neighbourhood[neighbourIndex].y;

            internalArrayIndex = (
                currentDimensionX < 0 || currentDimensionY < 0 || currentDimensionX >= boundX || currentDimensionY >= boundY ?
                -1 :
                currentDimensionX * strideX + currentDimensionY
            );

            if (internalArrayIndex != -1 && this.grid.data[internalArrayIndex] != 0)
            {
                existingPoint = this.samplePoints[this.grid.data[internalArrayIndex] - 1];

                if (Mathf.Pow(point.x - existingPoint.x, 2) + Mathf.Pow(point.y - existingPoint.y, 2) < this.squaredRadius)
                {
                    return true;
                }
            }
        }

        return false;
    }

    bool next()
    {
        int tries;
        Point currentPoint;
        float currentAngle;
        Point newPoint;

        for (int i = 0; this.processList.Count > 0 && i < 1000; i++)
        {
            int index = Mathf.FloorToInt(this.processList.Count * this.rng());

            currentPoint = this.processList[index];
            currentAngle = currentPoint.angle;
            tries = currentPoint.tries;

            if (tries == 0)
            {
                currentAngle = currentAngle + (float)(this.rng() - 0.5) * PIDIV3 * 4;
            }

            for (; tries < this.maxTries; tries++)
            {
                newPoint = new Point(
                    currentPoint.x + Mathf.Cos(currentAngle) * this.radiusPlusEpsilon,
                    currentPoint.y + Mathf.Sin(currentAngle) * this.radiusPlusEpsilon,
                    currentAngle,
                    0
                );

                if (
                    (newPoint.x >= 0 && newPoint.x < this.width) &&
                    (newPoint.y >= 0 && newPoint.y < this.height) &&
                    !this.inNeighbourhood(newPoint)
                )
                {
                    currentPoint.angle = currentAngle + this.angleIncrementOnSuccess + this.rng() * this.angleIncrement;
                    currentPoint.tries = tries + this.triesIncrementOnSuccess;
                    this.directAddPoint(newPoint);
                    return true;
                }

                currentAngle = currentAngle + this.angleIncrement;
            }

            if (tries >= this.maxTries)
            {
                Point r = this.processList[this.processList.Count - 1];
                this.processList.RemoveAt(this.processList.Count - 1);
                if (index < this.processList.Count)
                {
                    this.processList[index] = r;
                }
            }
        }

        return false;
    }

    public List<Vector2> fill()
    {
        if (this.samplePoints.Count == 0)
        {
            this.addRandomPoint();
        }

        for (int i = 0; i < 1000; i++)
        {
            if (!this.next()) { break; }
        }

        return this.samplePoints;
    }

    public List<Vector2> getAllPoints()
    {
        return this.samplePoints;
    }

    public void reset()
    {
        this.grid = new Grid(this.gridShape);
        this.samplePoints.Clear();
        this.processList.Clear();
    }
}