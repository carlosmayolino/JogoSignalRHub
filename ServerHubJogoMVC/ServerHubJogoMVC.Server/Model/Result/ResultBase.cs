using System;
using System.Collections.Generic;
using System.Text;

namespace ServerHubJogoMVC.Server.Model.Result
{
    public class ResultBase
    {
        public List<string> Mensagem { get; set; } = new List<string>();
        public bool Sucesso { get; set; }
    }
}
