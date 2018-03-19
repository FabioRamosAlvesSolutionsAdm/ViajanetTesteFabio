using System;
using System.Collections.Generic;
using System.Text;

namespace MyTestViajaNet.DomainService.Entities
{
    public class Cotacao
    {
        public string NomeMoeda { get; set; }
        public DateTime DtUltimaCarga { get; set; }
        public double ValorCompra { get; set; }
        public double ValorVenda { get; set; }
        public string Variacao { get; set; }
    }
}
