using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.modelos
{
    public class PaginacaoModel<Model, Filter>
    {
        public List<Model> ListaModel { get; set; }
        public List<int> QtdPaginas { get; set; }
        public int Pagina { get; set; }
        public Filter Filtro { get; set; }
        public string Parametro1 { get; set; }
        public string Parametro12 { get; set; }
        public string Parametro13 { get; set; }
        public double? Parametro2 { get; set; }
        public short? Parametro3 { get; set; }
        public DateTime? Parametro4 { get; set; }
        public bool? ParameterBool { get; set; }
        public VendaModel VendaModel { get; set; }
        public string Letra { get; set; }
        public int? TotalJogadores { get; set; }

        public PaginacaoModel()
        {
            ListaModel = new List<Model>();
            QtdPaginas = new List<int>();
            Pagina = (Pagina == 0) ? 1 : Pagina;
        }

        public PaginacaoModel(PaginacaoModel<Model, Filter> Model, int pagina)
        {
            ListaModel = Model.ListaModel;
            QtdPaginas = Model.QtdPaginas;
            Pagina = pagina;
            Parametro1 = Model.Parametro1;
            Parametro12 = Model.Parametro12;
            Parametro13 = Model.Parametro13;
            ParameterBool = Model.ParameterBool;
            Parametro2 = Model.Parametro2;
            Parametro3 = Model.Parametro3;
            Parametro4 = Model.Parametro4;
        }

        public string PrintaPaginaSelecionada(int paginaAtual, int paginaSelecionada)
        {
            if (paginaAtual == paginaSelecionada)
               return "alert-dark";
            else
                return "";
        }
    }

    public class PaginacaoModel2<Model, Model2, Filter>
    {
        public List<Model> ListaModel { get; set; }
        public List<Model2> ListaModel2 { get; set; }

        public List<int> QtdPaginas { get; set; }
        public int Pagina { get; set; }
        public Filter Filtro { get; set; }
        public string Parametro1 { get; set; }
        public string Parametro12 { get; set; }
        public string Parametro13 { get; set; }
        public double? Parametro2 { get; set; }
        public short? Parametro3 { get; set; }
        public string NomeCliente { get; set; }
        public PaginacaoModel2()
        {
            ListaModel = new List<Model>();
            ListaModel2 = new List<Model2>();
            QtdPaginas = new List<int>();
            Pagina = (Pagina == 0) ? 1 : Pagina;
        }

        public PaginacaoModel2(PaginacaoModel2<Model,Model2, Filter> Model, int pagina)
        {
            ListaModel = Model.ListaModel;
            QtdPaginas = Model.QtdPaginas;
            Pagina = pagina;
            Parametro1 = Model.Parametro1;
            Parametro12 = Model.Parametro12;
            Parametro13 = Model.Parametro13;
        }

        public string PrintaPaginaSelecionada(int paginaAtual, int paginaSelecionada)
        {
            if (paginaAtual == paginaSelecionada)
                return "alert-dark";
            else
                return "";
        }
    }
}


