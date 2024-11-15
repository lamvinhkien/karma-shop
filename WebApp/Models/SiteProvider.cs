namespace WebApp.Models;

public class SiteProvider : BaseProvider
{
    public SiteProvider(IConfiguration configuration) : base(configuration)
    {
    }

    ProductRepository? product;
    public ProductRepository Product => product ??= new ProductRepository(Connection);

    CategoryRepository? category;
    public CategoryRepository Category => category ??= new CategoryRepository(Connection);

    BrandRepository? brand;
    public BrandRepository Brand => brand ??= new BrandRepository(Connection);

    BannerRepository? banner;
    public BannerRepository Banner => banner ??= new BannerRepository(Connection);
    BlogRepository? blog;
    public BlogRepository Blog => blog ??= new BlogRepository(Connection);

    CartRepository? cart;
    public CartRepository Cart => cart ??= new CartRepository(Connection);

    InvoiceRepository? invoice;
    public InvoiceRepository Invoice => invoice ??= new InvoiceRepository(Connection);

    InvoiceDetailRepository? invoiceDetail;
    public InvoiceDetailRepository InvoiceDetail => invoiceDetail ??= new InvoiceDetailRepository(Connection);

    VnPayRepository? vnPay;
    public VnPayRepository VnPay => vnPay ??= new VnPayRepository(Connection);

    MemberRepository? member;
    public MemberRepository Member => member ??= new MemberRepository(Connection);
}