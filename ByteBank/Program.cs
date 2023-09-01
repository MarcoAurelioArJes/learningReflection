using ByteBank.Infraestrutura;

namespace ByteBank
{
    public class Program
    {
        static void Main(string[] args)
        {
            var prefixos = new string[] { "http://localhost:5134/" };
            var webApplication = new WebApplication(prefixos);
            webApplication.Iniciar();
        }
    }
}
