// https://youtu.be/iGL4cgwtrY0          aula
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




namespace Wxo.WebApi.Models
{
    public class Projeto
    {
        public int Id { get; set; }
        public string NomeDoProjeto { get; set; }
        public string Area { get; set; }
        public bool Status { get; set; }
    }
}