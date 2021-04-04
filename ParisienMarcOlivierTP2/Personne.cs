using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2
{
    class Personne
    {
        public Personne(string prenom, string nom)
        {
            _prenom = prenom;
            _nom = nom;
        }
        public Personne()
        {

            Console.Write("Prénom: ");
            _prenom = Console.ReadLine().Trim();
            Console.WriteLine();

            while (Prenom.Length == 0)
            {
                Console.WriteLine("Ne peut pas être vide");
                Console.Write("Prénom: ");
                _prenom = Console.ReadLine().Trim();
            }

            Console.Write("Nom: ");
            _nom = Console.ReadLine().Trim();
            Console.WriteLine();

            while (_nom.Length == 0)
            {
                Console.WriteLine("Ne peut pas être vide");
                Console.Write("Nom: ");
                _nom = Console.ReadLine().Trim();
            }



        }
        public string Prenom
        {
            get { return _prenom; }
        }
        public string Nom
        {
            get { return _nom; }
        }

        private readonly string _prenom;
        private readonly string _nom;
    }
}
