
[System.Serializable]
public struct IntVector2
{
    public IntVector2(int x = 0, int y = 0)
    {
        this.x = x;
        this.y = y;
    }

    public int x;
    public int y;
}

[System.Serializable]
public struct IntVector3
{
    public IntVector3(int x = 0, int y = 0, int z = 0)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public int x;
    public int y;
    public int z;
}
