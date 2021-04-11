using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2
{
    /// <summary>
    /// Classe commune aux médecins et aux patients
    /// </summary>
    class Personne
    {
        /// <summary>
        /// Constructeur de personne lors de la lecteur dans les fichiers .txt
        /// </summary>
        /// <param name="prenom">Prenom de la personne; medecin ou patient </param>
        /// <param name="nom">Nom de la personne; medecin ou patient</param>
        public Personne(string prenom, string nom)
        {
            _prenom = prenom;
            _nom = nom;
        }
        /// <summary>
        /// Constructeur de personne
        /// </summary>
        public Personne()
        {

            Console.Write("Prénom: ");
            _prenom = Console.ReadLine().Trim();
 
            while (Prenom.Length == 0)
            {
                Console.WriteLine("Ne peut pas être vide");
                Console.Write("Prénom: ");
                _prenom = Console.ReadLine().Trim();
            }

            Console.Write("Nom: ");
            _nom = Console.ReadLine().Trim();

            while (_nom.Length == 0)
            {
                Console.WriteLine("Ne peut pas être vide");
                Console.Write("Nom: ");
                _nom = Console.ReadLine().Trim();
            }
        }
        /// <summary>
        /// Prenom de la personne
        /// <return>Le prenom de la personne</return>
        /// </summary>
        public string Prenom
        {
            get { return _prenom; }
        }
        /// <summary>
        /// Nom de la personne
        /// <return>Le nom de la personne</return>
        /// </summary>
        public string Nom
        {
            get { return _nom; }
        }

        private readonly string _prenom;
        private readonly string _nom;
    }
}
