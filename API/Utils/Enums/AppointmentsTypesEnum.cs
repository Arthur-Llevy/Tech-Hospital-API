using System.Runtime.Serialization;

namespace Api.Utils.Enums;

public enum AppointmentsTypes 
{
    [EnumMember(Value="Consulta")]
    Consulta, 
    [EnumMember(Value="Exame laboratorial")]
    Exame_Laboratorial,
    [EnumMember(Value="Exame de imagem")]
    Exame_de_Imagem,
    [EnumMember(Value="Procedimento cirúrgico")]
    Procedimento_Cirurgico
}