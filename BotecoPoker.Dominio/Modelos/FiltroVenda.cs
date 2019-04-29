namespace BotecoPoker.Dominio.modelos
{
    public class FiltroVenda : FiltroBase
    {
        public string NomeCliente { get; set; }
        public string CodigoCliente { get; set; }
        public string ApelidoCliente { get; set; }

        public FiltroVenda(string param1, string param2, string param3)
        {
            NomeCliente = param1;
            CodigoCliente = param2;
            ApelidoCliente = param3;
        }

        public FiltroVenda()
        {
        }
    }
}
