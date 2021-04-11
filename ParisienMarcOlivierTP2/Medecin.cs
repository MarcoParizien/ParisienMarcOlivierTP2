using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace TP2
{
    /// <summary>
    /// Classe qui représente un médecin
    /// </summary>
    class Medecin : Personne
    {
        /// <summary>
        /// Constructeur avec paramètre de médecin.
        /// </summary>
        /// <param name="prenom">Prenom du médecin</param>
        /// <param name="nom">Nom du médecin</param>
        /// <param name="idMedecin">Identification du medecin</param>
        public Medecin(string prenom, string nom, int idMedecin) : base(prenom, nom)
        {
            _idMedecin = idMedecin;
        }
        /// <summary>
        ///  Constructeur avec paramètre de médecin retraité
        /// </summary>
        /// <param name="prenom">Prenom du médecin</param>
        /// <param name="nom">Nom du médecin</param>
        /// <param name="idMedecin">Identification du medecin</param>
        /// <param name="retraite">Date de la retraite du médecin</param>
        public Medecin(string prenom, string nom, int idMedecin, DateTime retraite) : base(prenom, nom)
        {
            _idMedecin = idMedecin;
            _retraite = retraite;
        }
        /// <summary>
        /// Constructeur d'un médecin
        /// </summary>
        public Medecin()
        {

            Console.Write("Code d'identification: ");
            _idMedecin = Convert.ToInt32(Console.ReadLine());

            while (_idMedecin < IDMIN || _idMedecin > IDMAX)
            {
                try
                {
                    Console.WriteLine("Valeur incorrecte. Doit être un entier entre {0} et {1}", IDMIN, IDMAX);
                    Console.Write("Code d'identification: ");
                    _idMedecin = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Le champ ne peut pas contenir de lettre.");
                }
            }

        }
        /// <summary>
        /// Indique si le médecin est retraité.
        /// <returns>Vrai si la date de retraite est différente que la Date par défaut</returns>
        /// <return>Faux si la date de retraite est égale à default(DateTime)</return>
        /// </summary>
        public bool Retraite
        {
            get { return _retraite != default(DateTime) ? true : false; }
        }
        /// <summary>
        /// Retourne la date de retraite du médecin
        /// </summary>
        public DateTime DateRetraite
        {
            get { return _retraite; }
        }
        /// <summary>
        /// Retourne l'identification du médecin
        /// </summary>
        public int Identification
        {
            get { return _idMedecin; }
        }
        /// <summary>
        /// Liste qui contient la liste de patient du médecin
        /// </summary>
        public List<Patient> PatientSuivi
        {
            get { return _sesPatients; }
        }

        protected List<Patient> _sesPatients = new List<Patient>();
        protected DateTime _retraite;
        private readonly int _idMedecin;
        private const int IDMIN = 100;
        private const int IDMAX = 999;
    }
}
