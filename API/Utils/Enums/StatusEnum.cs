using System.Runtime.Serialization;

namespace Api.Utils.Enums;

public enum Status 
{
    [EnumMember(Value="Marcado")]
    Marcado,

    [EnumMember(Value="Indeferido")]
    Indeferido
}