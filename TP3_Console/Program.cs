using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TP3_Console.Models.EntityFramework;

namespace TP3_Console
{
    internal class Program
    {

        static void Main(string[] args)
        {
            AjouterFilms();
            Console.ReadKey();
        }
        public static void Exo2Q1()
        {
            var ctx = new FilmsDbContext();
            foreach (var film in ctx.Films)
            {
                Console.WriteLine(film.ToString());
                Console.WriteLine("\n");
            }
        }


        public static void Exo2Q2()
        {
            var ctx = new FilmsDbContext();
            foreach (var user in ctx.Utilisateurs)
            {
                Console.WriteLine($"email de {user.Login} : {user.Email}");
                Console.WriteLine("\n");
            }
        }

        public static void Exo2Q3()
        {
            var ctx = new FilmsDbContext();
            var UtilisateursCroissant = ctx.Utilisateurs.OrderBy(Utilisateur => Utilisateur.Login);
            foreach (var user in UtilisateursCroissant)
            {
                Console.WriteLine($"{user.Login}");
                Console.WriteLine("\n");
            }
        }


        public static void Exo2Q4()
        {
            var ctx = new FilmsDbContext();
            Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
            Console.WriteLine("Categorie : " + categorieAction.Nom);
            Console.WriteLine("Films : ");
            //Chargement des films de la catégorie Action. 
            foreach (var film in ctx.Films.Where(f => f.IdcategorieNavigation.Nom ==
            categorieAction.Nom).ToList())
            {
                Console.WriteLine($"Film : {film.Nom}, Id : {film.Idfilm}");
            }
        }


        public static void Exo2Q5()
        {
            var ctx = new FilmsDbContext();
            Console.WriteLine($"Nombre de Catégorie : {ctx.Categories.Count()}");
        }



        public static void Exo2Q6()
        {
            var ctx = new FilmsDbContext();
            Console.WriteLine($"Note la plus basse : {ctx.Avis.Min(avi => avi.Note)}");
        }


        public static void Exo2Q7()
        {
            var ctx = new FilmsDbContext();
            var FilmsParLe = ctx.Films.Where(film => film.Nom.ToLower().StartsWith("le"));
            foreach (var film in FilmsParLe)
            {
                Console.WriteLine(film.ToString());
                Console.WriteLine("\n");
            }
        }


        public static void Exo2Q8()
        {
            var ctx = new FilmsDbContext();
            Film filmPulpFiction = ctx.Films.First(c => c.Nom.ToLower() == "pulp fiction");
            var AvisPulpFiction = ctx.Avis.Where(avi => avi.Idfilm == filmPulpFiction.Idfilm);
            Console.WriteLine($"Moyenne de Pulp Fiction : {AvisPulpFiction.Average(avi => avi.Note)}");
        }

        public static void Exo2Q9()
        {
            var ctx = new FilmsDbContext();
            // EN 2 LIGNES

            //Avi aviNoteMax = ctx.Avis.First(avi => avi.Note == ctx.Avis.Max(avi => avi.Note));
            //Utilisateur utilisateurAviNoteMax = ctx.Utilisateurs.First(user => user.Idutilisateur == aviNoteMax.Idutilisateur);

            // EN 1 LIGNE

            Utilisateur utilisateurAviNoteMax = ctx.Utilisateurs.First(user => user.Idutilisateur == (ctx.Avis.First(avi => avi.Note == ctx.Avis.Max(avi => avi.Note))).Idutilisateur);
            Console.WriteLine($"L'utilisateur avec la note la plus élevée est l'{utilisateurAviNoteMax.ToString()}");
        }



        public static void AjoutUtilisateur()
        {
            var ctx = new FilmsDbContext();
            var user = new Utilisateur()
            {
                Login = "bouvimae",
                Email = "mael.bouvier-sobrino@etu.univ-smb.fr",
                Pwd = "password"
            };

            ctx.Utilisateurs.Add(user);
            ctx.SaveChanges();
        }


        public static void ModifierFilm()
        {
            var ctx = new FilmsDbContext();
            Film filmDouzesSinges = ctx.Films.First(c => c.Nom.ToLower() == "l'armee des douze singes");
            filmDouzesSinges.Description = "Un film de science fiction réalisé par Terry Gilliam, sorti en 1995. Date de modification : " + DateTime.Now;
            filmDouzesSinges.Idcategorie = ctx.Categories.First(c => c.Nom.ToLower() == "drame").Idcategorie;

            ctx.SaveChanges();
        }

        public static void DeleteFilm()
        {
            var ctx = new FilmsDbContext();
            Film filmDouzesSinges = ctx.Films.First(c => c.Nom.ToLower() == "l'armee des douze singes");
            var avisFilmsDouzesSinges = ctx.Avis.Where(avi => avi.Idfilm == filmDouzesSinges.Idfilm);
            if (avisFilmsDouzesSinges != null)
            {
                foreach (var avi in avisFilmsDouzesSinges)
                {
                    ctx.Avis.Remove(avi);
                }
            }
            ctx.Films.Remove(filmDouzesSinges);
            ctx.SaveChanges();
        }

        public static void AjouterAvis()
        {
            var ctx = new FilmsDbContext();

            Film FilmPrefere = ctx.Films.First(c => c.Nom.ToLower() == "matrix");
            Utilisateur moi = ctx.Utilisateurs.First(u => u.Login.ToLower() == "bouvimae");

            var avi = new Avi()
            {
                Idfilm = FilmPrefere.Idfilm,
                Idutilisateur = moi.Idutilisateur,
                Note = 5,
                Commentaire = "Un chef d'oeuvre du cinéma !"
            };

            ctx.Avis.Add(avi);
            ctx.SaveChanges();
        }

        public static void AjouterFilms()
        {
            var ctx = new FilmsDbContext();

            var film1 = new Film()
            {
                Nom = "Film 1",
                Description = "Description du film 1",
                Idcategorie = ctx.Categories.First(c => c.Nom.ToLower() == "drame").Idcategorie
            };

            var film2 = new Film()
            {
                Nom = "Film 2",
                Description = "Description du film 2",
                Idcategorie = ctx.Categories.First(c => c.Nom.ToLower() == "drame").Idcategorie
            };

            ctx.Films.Add(film1);
            ctx.Films.Add(film2);

            //  ATTENTION IL FAUT BIEN PENSER A REMETTRE LA SEQUENCE A JOUR DANS LA BASE

            //  ALTER TABLE film ENABLE TRIGGER ALL; 
            //  SELECT pg_catalog.setval(pg_get_serial_sequence('film', 'idfilm'), MAX(idfilm)) FROM
            //  film;

            ctx.SaveChanges();
        }
    }
}













//    //Leçon à décommenter au fur et à mesure ATTENTION C'EST UN MAIN ENTIER

//static void Main(string[] args)
//{
















//    //            Console.WriteLine("\n--------------------------------------------------------------");
//    //            Console.WriteLine("Modification des données : ");
//    //            Console.WriteLine("--------------------------------------------------------------\n");
//    //            using (var ctx = new FilmsDbContext())
//    //            {
//    //                //Désactivation du suivi des modifications (pour les requêtes de lecture seule, cela améliore les performances)

//    //                //ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;



//    //                //Requête SELECT
//    //                Film titanic = ctx.Films.First(f => f.Nom.Contains("Titanic"));

//    //                //    ou alors
//    //                //Film titanic = (from f in ctx.Films
//    //                //                where f.Nom == "Titanic"
//    //                //                select f).First();



//    //                //Modification de l'entité (dans le contexte seulement)
//    //                titanic.Description = "Un bateau échoué. Date : " + DateTime.Now;




//    //                //Sauvegarde du contexte => Application de la modification dans la BD           EN GROS C'EST UN COMMIT
//    //                int nbchanges = ctx.SaveChanges();

//    //                Console.WriteLine("Nombre d'enregistrements modifiés ou ajoutés : " + nbchanges);
//    //            }

//    //            Console.WriteLine("\n--------------------------------------------------------------");
//    //            Console.WriteLine("Lecture des données : ");
//    //            Console.WriteLine("--------------------------------------------------------------\n");

//    //            using (var ctx = new FilmsDbContext())
//    //{
//    //                //Chargement de la catégorie Action 
//    //                Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
//    //                Console.WriteLine("Categorie : " + categorieAction.Nom);
//    //                Console.WriteLine("Films : ");
//    //                //Chargement des films de la catégorie Action. 
//    //                foreach (var film in ctx.Films.Where(f => f.IdcategorieNavigation.Nom ==
//    //                categorieAction.Nom).ToList())
//    //                {
//    //                    Console.WriteLine(film.Nom);
//    //                }
//    //            }

//    //            Console.WriteLine("\n--------------------------------------------------------------");
//    //            Console.WriteLine("Lecture des données Chargement explicite (collection) : ");
//    //            Console.WriteLine("--------------------------------------------------------------\n");

//    //            using (var ctx = new FilmsDbContext())
//    //{
//    //                Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
//    //                Console.WriteLine("Categorie : " + categorieAction.Nom);
//    //                //Chargement des films dans categorieAction 
//    //                ctx.Entry(categorieAction).Collection(c => c.Films).Load();
//    //                Console.WriteLine("Films : ");
//    //                foreach (var film in categorieAction.Films)
//    //                {
//    //                    Console.WriteLine(film.Nom);
//    //                }
//    //            }


//    //            Console.WriteLine("\n--------------------------------------------------------------");
//    //            Console.WriteLine("Lecture des données Chargement hatif (Include) : ");
//    //            Console.WriteLine("--------------------------------------------------------------\n");

//    //            using (var ctx = new FilmsDbContext())
//    //            {
//    //                //Chargement de la catégorie Action et des films de cette catégorie 
//    //                Categorie categorieAction = ctx.Categories
//    //                .Include(c => c.Films)
//    //                .First(c => c.Nom == "Action");
//    //                Console.WriteLine("Categorie : " + categorieAction.Nom);
//    //                Console.WriteLine("Films : ");
//    //                foreach (var film in categorieAction.Films)
//    //                {
//    //                    Console.WriteLine(film.Nom);
//    //                }
//    //            }



//    //            Console.WriteLine("\n--------------------------------------------------------------");
//    //            Console.WriteLine("Lecture des données Chargement hatif (Plusieurs Includes) : ");
//    //            Console.WriteLine("--------------------------------------------------------------\n");

//    //            using (var ctx = new FilmsDbContext())
//    //            {
//    //                //Chargement de la catégorie Action, des films de cette catégorie et des avis 
//    //                Categorie categorieAction = ctx.Categories
//    //                .Include(c => c.Films)
//    //                .ThenInclude(f => f.Avis)
//    //                .First(c => c.Nom == "Action");
//    //            }



//    //            Console.WriteLine("\n--------------------------------------------------------------");
//    //            Console.WriteLine("Lecture des données Chargement différé (Avec loading sur categorie, Pas forcément conseillé) : ");
//    //            Console.WriteLine("--------------------------------------------------------------\n");


//    //            using(var ctx = new FilmsDbContext())
//    //{
//    //                //Chargement de la catégorie Action 
//    //                Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
//    //                Console.WriteLine("Categorie : " + categorieAction.Nom);
//    //                Console.WriteLine("Films : ");
//    //                //Chargement des films de la catégorie Action. 
//    //                foreach (var film in categorieAction.Films) // lazy loading initiated 
//    //                {
//    //                    Console.WriteLine(film.Nom);
//    //                }
//    //            }
//}