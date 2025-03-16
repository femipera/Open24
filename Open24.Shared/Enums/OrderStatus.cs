using System.ComponentModel;

namespace Open24.Shared.Enums;

public enum OrderStatus
{
    [Description("Pendente")]
    Pending = 1,

    [Description("Processando")]
    Processing = 2,

    [Description("Enviado")]
    Shipped = 3,

    [Description("Concluído")]
    Completed = 4,

    [Description("Cancelado")]
    Canceled = 5,

    [Description("Devolvido")]
    Returned = 6,

    [Description("Falha")]
    Failed = 7

    [Description("Aguardando")]
    waiting = 8
}
