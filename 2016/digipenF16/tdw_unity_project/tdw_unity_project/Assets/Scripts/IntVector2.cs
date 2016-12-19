/*////////////////////////////////////////////////////////////////////////
//STRUCT: IntVector2.cs
//AUTHOR: Travis Moore
////////////////////////////////////////////////////////////////////////*/

[System.Serializable] //allows custom struct to show up as seriazable field in other scripts
public struct IntVector2
{

    public int x;
    public int z;

    //constructor
    public IntVector2(int x_, int z_)
    {
        this.x = x_;
        this.z = z_;
    }

    //adding operator method to allow addition of IntVector via the "+" operator
    public static IntVector2 operator + (IntVector2 a_, IntVector2 b_)
    {
        a_.x += b_.x;
        a_.z += b_.z;
        return a_;
    }
}
