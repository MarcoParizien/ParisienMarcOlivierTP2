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
        public GestionnairePatients(GestionnaireMedecins gestionMedecin)
        {
            try
            {
                _gestionMedecin = gestionMedecin;

                using (StreamReader fichierLecture = new StreamReader(_nomFichierPatient))
                {

                    while (!fichierLecture.EndOfStream)
                    {
                        string ligneLue = fichierLecture.ReadLine();

                        string[] elementsLu = ligneLue.Split(';');
                        int idPatient = Convert.ToInt32(elementsLu[0]);
                        string nom = elementsLu[1];
                        string prenom = elementsLu[2];

                        if (elementsLu.Length == 5 && elementsLu[3] == "")
                        {
                            int numeroMedecin = Convert.ToInt32(elementsLu[4]);

                            foreach (var m in _gestionMedecin._medecins)
                            {
                                if (numeroMedecin == m.Identification)
                                {
                                    Medecin sonMedecin = m;
                                    _patients.Add(new Patient(prenom, nom, idPatient, m));
                                    m.PatientSuivi.Add(_patients[_patients.Count - 1]);
                                    break;
                                }
                            }

                        }

                        if (elementsLu[3] != "")
                        {
                            DateTime deces = Convert.ToDateTime(elementsLu[3]);
                            _patients.Add(new Patient(prenom, nom, idPatient, deces));
                        }

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
                            sb.AppendFormat("{0};{1};{2};;{3}", p.IdPatient, p.Prenom, p.Nom, p.SonMedecin.Identification);
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

            if (_gestionMedecin._medecins.Count > 0)
            {
                Medecin medecinMinPatient = _gestionMedecin._medecins[0];

                foreach (var m in _gestionMedecin._medecins)
                {
                    if (m.PatientSuivi.Count < medecinMinPatient.PatientSuivi.Count && !m.Retraite)
                    {
                        medecinMinPatient = m;
                    }
                }
                medecinMinPatient.PatientSuivi.Add(_patients[_patients.Count - 1]); //Ajoute le dernier patient ajouté à la liste de patient du medecin
            }
            Console.WriteLine("Patient ajouté!");
            Console.ReadKey(true);
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
                        _patients.Remove(p);
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
            Console.WriteLine("---------------------");
            Console.WriteLine("Liste des patients");

            foreach (var p in _patients)
            {

                if (!p.Mort)
                {
                    Console.Write("{0} {1} {2}", p.IdPatient, p.Prenom, p.Nom);
                    Console.Write(" - Medecin - {0} {1} {2}", p.SonMedecin.Identification, p.SonMedecin.Prenom, p.SonMedecin.Nom);
                    Console.WriteLine();
                }
                else
                {
                    Console.Write("{0} {1} {2}", p.IdPatient, p.Prenom, p.Nom);
                    Console.Write(" Décédé.");
                    Console.WriteLine();
                }

            }
            Console.ReadKey(true);
        }
        public void AfficherUnique()
        {


            if (_patients.Count > 0)
            {

                Console.WriteLine("-----------------------------");
                Console.Write("Numéro d'assurance maladie : ");
                int id = Convert.ToInt32(Console.ReadLine());

                try
                {
                    while (id < 1000 || id > 9999)
                    {
                        Console.WriteLine("Le numéro d'assurance maladie doit être entre {0} et {1} ", 1000, 9999);
                        Console.Write("Numéro d'assurance maladie : ");
                        id = Convert.ToInt32(Console.ReadLine());
                    }
                    foreach (var p in _patients)
                    {
                        if (id == p.IdPatient && !p.Mort)
                        {
                            Console.WriteLine("---------------------");
                            Console.WriteLine("Patient");
                            Console.WriteLine("Numéro d'assurance maladie : " + p.IdPatient );
                            Console.WriteLine("Nom : {0} {1} ", p.Prenom, p.Nom);
                            Console.WriteLine("Medecin : {0} {1} {2}", p.SonMedecin.Identification, p.SonMedecin.Prenom, p.SonMedecin.Nom);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("---------------------");
                            Console.WriteLine("Patient");
                            Console.WriteLine("Nom : {0} {1} ", p.Prenom, p.Nom);
                            Console.WriteLine("Décédé le {0}", p.Deces.ToShortDateString());
                            break;
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Le champ ne peut être vide ou contenir des lettres");
                }
            }
            else
            {
                Console.WriteLine("Il n'y a aucun patient dans le système.");
            }
            Console.ReadKey(true);
        }

        private GestionnaireMedecins _gestionMedecin;
        private const string _nomFichierPatient = "patients.txt";
        List<Patient> _patients = new List<Patient>();
    }
}

