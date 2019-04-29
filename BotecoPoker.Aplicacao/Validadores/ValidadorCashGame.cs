using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Aplicacao.Validadores
{
    public class ValidadorCashGame
    {
        public string Validar(CashGame cashGame)
        {
            StringBuilder erros = new StringBuilder();
            if (cashGame.IdCliente == 0)
                erros.AppendLine("Favor selecionar um cliente! ");

            if (cashGame.Valor == 0)
                erros.AppendLine("Um valor deve ser informado!");

            if(cashGame.Situacao == Dominio.Enumeradores.SituacaoVenda.Pago && cashGame.Id > 0)
                erros.AppendLine("Impossivel alterar um valor no Cash Game pago!");

            return erros.ToString();
        }
    }
}
