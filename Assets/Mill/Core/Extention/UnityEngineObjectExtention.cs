//针对UnityObject判断是否为空的扩展方法
/// <summary>
/// 供LUA调用判断是否为空 请不要直接使用==nil lua中有问题
/// </summary>
public static class UnityEngineObjectExtention
{
    public static bool IsNull(this UnityEngine.Object o)
    {
        return o == null;
    }
}