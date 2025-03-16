using System.ComponentModel;

namespace Open24.Shared.Enums;

public enum DeliveryStatus
{
    [Description("Aguardando envio")]
    Pending = 1,

    [Description("Enviado")]
    Shipped = 2,

    [Description("Em transporte")]
    InTransit = 3,

    [Description("Entregue")]
    Delivered = 4,

    [Description("Entrega cancelada")]
    Canceled = 5,

    [Description("Devolvido")]
    Returned = 6

    [Description("Devolvido")]
    Error = 7
}
