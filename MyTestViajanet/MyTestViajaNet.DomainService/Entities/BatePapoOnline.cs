using System;

namespace MyTestViajaNet.DomainService.Entities
{
    public class BatePapoOnline
    {
        public int Id { get; set; }
        public string LugaresDisponiveis { get; set; }
        public string PessoasOnline { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataRegistro { get; set; }
        
    }
}
