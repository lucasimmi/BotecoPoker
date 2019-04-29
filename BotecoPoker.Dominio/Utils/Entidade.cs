namespace BotecoPoker.Dominio.Utils
{
    public class Entidade<T> : IEntidade<T>
    {
      public T Id { get; set; }
    }
}