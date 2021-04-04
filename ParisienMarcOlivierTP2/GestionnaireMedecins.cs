﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace TP2
{
    class GestionnaireMedecins
    {
        public GestionnaireMedecins()
        {
            try
            {
                using (StreamReader fichierLecture = new StreamReader(nomFichierMedecin))
                {
                    string ligneLue = fichierLecture.ReadLine();

                    while (ligneLue != null)
                    {
                        string[] elementsLu = ligneLue.Split(';');
                        int idMedecin = Convert.ToInt32(elementsLu[0]);
                        string nom = elementsLu[1];
                        string prenom = elementsLu[2];
                        if (elementsLu[3] != null)
                        {
                            DateTime _retraite = Convert.ToDateTime(elementsLu[3]);
                        }
                        else
                        {

                        }
                        _medecins.Add(new Medecin(prenom, nom, idMedecin));

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

        }
        public void Ajouter()
        {
            _medecins.Add(new Medecin());
            Console.WriteLine("Medecin ajouté!");
            Console.ReadKey(true);

        }
        public void IndiquerRetraite()
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

        private const string nomFichierMedecin = "medecins.txt";
        List<Medecin> _medecins = new List<Medecin>();
    }
}