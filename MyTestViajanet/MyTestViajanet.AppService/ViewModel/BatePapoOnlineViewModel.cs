using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyTestViajanet.AppService.ViewModel
{
    public class BatePapoOnlineViewModel 
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Lugares Disponíveis")]
        public string LugaresDisponiveis { get; set; }
        [DisplayName("Pessoas Online")]
        public string PessoasOnline { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [DisplayName("Data do Registro")]
        public DateTime DataRegistro { get; set; }
    }
}
