using UnityEngine;

public class ConstantsManager : MonoBehaviour
{
    [SerializeField] Constant[] ConstantsList = new Constant[1];

    public void AlterConstant(int index, string name, string value, ConstantType type)
    {
        ConstantsList[index].Key = name;
        ConstantsList[index].Value = value;
        ConstantsList[index].Type = type;
    }
}

