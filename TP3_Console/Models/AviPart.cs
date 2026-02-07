using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3_Console.Models.EntityFramework
{
    public partial class Avi
    {
        public override string ToString()
        {
            return $"Avis [FilmID: {Idfilm}, UserID: {Idutilisateur}] Note: {Note}/5 - \"{Commentaire}\"";
        }
    }
}
