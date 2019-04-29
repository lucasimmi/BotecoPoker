using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Aplicacao.Validadores
{
    public class ValidadorTorneio
    {
        public string ValidaTorneio(Torneio torneio)
        {
            StringBuilder erros = new StringBuilder();

            if (torneio.Addon == null)
                erros.AppendLine("Add-On deve ser informado");
            if (torneio.BuyIn == null)
                erros.AppendLine("Buy-In deve ser informado");
            if (torneio.JackPot == null)
                erros.AppendLine("Jack-Pot deve ser informado");
            if (torneio.Jantar == null)
                erros.AppendLine("Jantar deve ser informado");
            if (torneio.Nome == null || string.IsNullOrWhiteSpace(torneio.Nome))
                erros.AppendLine("Nome do torneio deve ser informado");
            if (torneio.ReBuy == null)
                erros.AppendLine("Re-Buy deve ser informado");
            if (torneio.TaxaAdm == null)
                erros.AppendLine("Taxa-Adm deve ser informado");

            return erros.ToString();
        }
    }
}
