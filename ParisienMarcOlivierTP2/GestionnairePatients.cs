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
                    

                    while (!fichierLecture.EndOfStream)
                    {
                        string ligneLue = fichierLecture.ReadLine();

                        string[] elementsLu = ligneLue.Split(';');
                        int idPatient = Convert.ToInt32(elementsLu[0]);
                        string nom = elementsLu[1];
                        string prenom = elementsLu[2];

                        if (elementsLu[3] != "")
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
                        string dateCourte;
                        if (p.Deces != default(DateTime))
                        {
                            dateCourte = p.Deces.ToShortDateString();
                            sb.AppendFormat("{0};{1};{2};{3}", p.IdPatient, p.Prenom, p.Nom, dateCourte);
                            fichierEcriture.WriteLine(sb.ToString());
                        }
                        else
                        {
                            sb.AppendFormat("{0};{1};{2};", p.IdPatient, p.Prenom, p.Nom);
                            fichierEcriture.WriteLine(sb.ToString());
                        }

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
            Patient unPatient = new Patient();
            foreach (var p in _patients)
            {
                if (p.IdPatient == unPatient.IdPatient)
                {
                    Console.WriteLine("Impossible d'ajouter le patient. Ce numéro d'assurance maladie existe déjà.");
                    return;
                }
            }
            _patients.Add(unPatient);
            Console.WriteLine("Patient ajouté!");
        }
        public void IndiquerDeces()
        {
            try
            {
                Console.WriteLine("-----------------------------");
                Console.Write("Numéro d'assurance maladie : ");
                int id = Convert.ToInt32(Console.ReadLine());

                foreach (var p in _patients)
                {
                    if (id == p.IdPatient && !p.Mort)
                    {
                        Console.Write("Entrer la date du décès (A/M/J) : ");
                        DateTime laDate = Convert.ToDateTime(Console.ReadLine());
                        _patients.Add(new Patient(p.Prenom, p.Nom, p.IdPatient, laDate));
                    }
                    else
                    {
                        Console.WriteLine("Désolé de vous l'apprendre, mais il est déjà mort");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void AfficherStatistiques()
        {
            int patientMort = 0;
            int patientTotal = _patients.Count();
            foreach (var p in _patients)
            {
                if (p.Mort)
                {
                    patientMort++;
                }
            }
            if (patientMort > 0)
            {
                Console.WriteLine("Il y a {0} patient dont {1} mort(s).", patientTotal, patientMort);
            }
            else
            {
                Console.WriteLine("Il y a {0} patient(s).", patientTotal);
            }
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

