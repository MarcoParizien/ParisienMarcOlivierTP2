using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2
{
    class GestionnairePatients
    {
        public GestionnairePatients(GestionnaireMedecins unMedecin)
        {

        }
        public void Sauvegarder()
        {

        }
        public void Ajouter()
        {
            _patients.Add(new Patient());
            Console.WriteLine("Patient ajouté!");
        }
        public void IndiquerDeces()
        {

        }
        public void AfficherStatistiques()
        {

        }
        public void AfficherListe()
        {

        }
        public void AfficherUnique()
        {

        }

        List<Patient> _patients = new List<Patient>();
    }
}

