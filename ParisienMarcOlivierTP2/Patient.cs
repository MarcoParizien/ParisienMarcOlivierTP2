using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2
{
    class Patient : Personne
    {
        public Patient(string prenom, string nom, int idPatient) : base(prenom, nom)
        {

        }
        public Patient()
        {
            Console.Write("Numéro d'assurance maladie: ");
            _nam = Convert.ToInt32(Console.ReadLine());

            while (_nam < 1000 || _nam > 9999)
            {
                Console.WriteLine("Valeur incorrecte. Le numéro d'assurance maladie doit être entre {0} et {1}", NAM_MIN,NAM_MAX);
                Console.Write("Numéro d'assurance maladie: ");
                _nam = Convert.ToInt32(Console.ReadLine());
            }
        }

        private const string nomFichierPatients = "patients.txt";
        private readonly int _idPatient;
        private readonly int _nam; //numéro d'assurance maladie
        private const int NAM_MIN = 1000;
        private const int NAM_MAX = 9999;
    }
}
