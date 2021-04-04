using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace TP2
{
    class GestionnairePatients
    {
        public GestionnairePatients(GestionnaireMedecins unMedecin)
        {
            try
            {
                using (StreamReader fichierLecture = new StreamReader(_nomFichierPatient))
                {
                    string ligneLue = fichierLecture.ReadLine();

                    while (ligneLue != null)
                    {
                        string[] elementsLu = ligneLue.Split(';');
                        int idPatient = Convert.ToInt32(elementsLu[0]);
                        string nom = elementsLu[1];
                        string prenom = elementsLu[2];

                        if (elementsLu[3] != null)
                        {
                            DateTime deces = Convert.ToDateTime(elementsLu[3]);
                            _patients.Add(new Patient(prenom, nom, idPatient, deces));
                        }
                        else
                        {
                            _patients.Add(new Patient(prenom, nom, idPatient));
                        }

                        ligneLue = fichierLecture.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
        public void Sauvegarder()
        {
            try
            {
                using (StreamWriter fichierEcriture = new StreamWriter(_nomFichierPatient))
                {
                    foreach (Patient p in _patients)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("{0};{1};{2};{3}", p.IdPatient, p.Prenom, p.Nom, p.Deces);
                        Console.WriteLine(sb.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

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
        private const string _nomFichierPatient = "patients.txt";
        List<Patient> _patients = new List<Patient>();
    }
}

