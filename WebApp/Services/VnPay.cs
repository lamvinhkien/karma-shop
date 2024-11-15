namespace WebApp.Services;

public class VnPay
{
    [ConfigurationKeyName("vnp_Version")]
    public string Version { get; set; } = null!;
    [ConfigurationKeyName("vnp_Command")]
    public string Command { get; set; } = null!;
    [ConfigurationKeyName("vnp_TmnCode")]
    public string TmnCode { get; set; } = null!;
    [ConfigurationKeyName("vnp_CurrCode")]
    public string CurrCode { get; set; } = null!;
    [ConfigurationKeyName("vnp_Locale")]
    public string Locale { get; set; } = null!;
    [ConfigurationKeyName("vnp_OrderType")]
    public string OrderType { get; set; } = null!;
    [ConfigurationKeyName("vnp_ReturnUrl")]
    public string ReturnUrl { get; set; } = null!;
    [ConfigurationKeyName("vnp_Amount")]
    public decimal Amount { get; set; }
    [ConfigurationKeyName("vnp_OrderInfo")]
    public string OrderInfo { get; set; } = null!;
    [ConfigurationKeyName("vnp_IpAddr")]
    public string IpAddr { get; set; } = null!;
    [ConfigurationKeyName("vnp_TxnRef")]
    public string TxnRef { get; set; } = null!;
    [ConfigurationKeyName("vnp_HashSecret")]
    public string HashSecret { get; set; } = null!;
    [ConfigurationKeyName("vnp_BaseUrl")]
    public string BaseUrl { get; set; } = null!;
}