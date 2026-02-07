using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3_Console.Models.EntityFramework
{
    public partial class Utilisateur
    {
        public override string ToString()
        {
            // ATTENTION : Ne jamais afficher le mot de passe (Pwd) dans un ToString pour des raisons de sécurité
            return $"Utilisateur {Idutilisateur} : {Login} ({Email})";
        }
    }
}
