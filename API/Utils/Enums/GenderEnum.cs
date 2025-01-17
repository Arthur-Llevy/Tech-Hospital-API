using System.Runtime.Serialization;

namespace Api.Utils.Enums;

public enum Gender
{
    [EnumMember(Value="Masculino")]
    Masculine,

    [EnumMember(Value="Feminino")]
    Feminino
}