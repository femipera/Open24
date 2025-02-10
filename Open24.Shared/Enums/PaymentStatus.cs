using System.ComponentModel;

namespace Open24.Shared.Enums;

public enum PaymentStatus
{
    [Description("Aguardando pagamento")]
    Pending = 1,

    [Description("Pago")]
    Paid = 2,

    [Description("Pagamento falhou")]
    Failed = 3,

    [Description("Reembolsado")]
    Refunded = 4,

    [Description("Cancelado")]
    Canceled = 5
}