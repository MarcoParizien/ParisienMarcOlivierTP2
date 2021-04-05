using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2
{
    class Patient : Personne
    {
        /// <summary>
        /// Constructeur de patient avec paramètres. Sera appelé lors de la lecture du fichier (s'il existe) "patients.txt"
        /// </summary>
        /// <param name="prenom"^>Prenom du patient</param>
        /// <param name="nom">Nom du patient</param>
        /// <param name="idPatient">Numéro d'assurance maladie du patient</param>
        /// <param name="deces">Date de son décés</param>
        public Patient(string prenom, string nom, int idPatient, DateTime deces) : base(prenom, nom)
        {
            _deces = deces;
        }
        /// <summary>
        /// Constructeur de patient avec paramètre, mais sans la date de décés. Sera appelé lors de la lecture du fichier
        /// (s'il existe) "patients.txt" et lorsque le patient n'a pas de date de décès à son dossier
        /// </summary>
        /// <param name="prenom"^>Prenom du patient</param>
        /// <param name="nom">Nom du patient</param>
        /// <param name="idPatient">Numéro d'assurance maladie du patient</param>
        public Patient(string prenom, string nom, int idPatient) : base(prenom, nom)
        {
            _idPatient = idPatient;
        }
        /// <summary>
        /// Constructeur sans paramètre. Sera appelé lorsque l'utilisateur veut entrer un nouveau patient manuellement.
        /// </summary>
        public Patient()
        {
            Console.Write("Numéro d'assurance maladie: ");
            _nam = Convert.ToInt32(Console.ReadLine());

            while (_nam < 1000 || _nam > 9999)
            {
                Console.WriteLine("Valeur incorrecte. Le numéro d'assurance maladie doit être entre {0} et {1}", NAM_MIN, NAM_MAX);
                Console.Write("Numéro d'assurance maladie: ");
                _nam = Convert.ToInt32(Console.ReadLine());
            }
        }
        /// <summary>
        /// Attribut pour stocker la date du décès d'un patient
        /// </summary>
        public DateTime Deces
        {
            get { return _deces; }
        }
        public bool Mort
        {
            get { return _deces != default(DateTime) ? true : false; }
        }
        /// <summary>
        /// Stocke le numéro d'assurance maladie du patient
        /// </summary>
        public int IdPatient
        {
            get { return _idPatient; }
        }

        private readonly DateTime _deces;
        private const string nomFichierPatients = "patients.txt";
        private readonly int _idPatient;
        private readonly int _nam; //numéro d'assurance maladie
        private const int NAM_MIN = 1000;
        private const int NAM_MAX = 9999;
    }
}
