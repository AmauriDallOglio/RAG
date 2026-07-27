using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAG.Infraestrutura.Contexto
{
    public class CommandContexto : GenericoContexto
    {
        public CommandContexto(DbContextOptions<CommandContexto> options) : base(options)
        {

        }
    }
}
