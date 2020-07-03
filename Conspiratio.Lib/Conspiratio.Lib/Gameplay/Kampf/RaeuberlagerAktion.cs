﻿using System;
using System.Collections.Generic;
using Conspiratio.Kampf;
using Conspiratio.Lib.Gameplay.Kampf.Einheiten;
using Conspiratio.Lib.Gameplay.Spielwelt;

namespace Conspiratio.Lib.Gameplay.Kampf
{
    /// <summary>
    /// Stellt eine Aktion eines Räuberlagers dar
    /// </summary>
    [Serializable]
    public class RaeuberlagerAktion : StuetzpunktAktion
    {
        #region Variablen und Properties

        /// <summary>
        /// Liste aller Einheiten von dieser Aktion
        /// </summary>
        public EnumAktionsartRaeuberlager Aktionsart { get; set; }

        #endregion

        #region Public Funktionen

        #region Konstruktor
        /// <summary>
        /// Initialisiert das aktuelle Objekt
        /// </summary>
        /// <param name="aktionsart">Gewünschte Aktionsart der Aktion (z.B. Truppen schicken)</param>
        /// <param name="zielLandID">Gewünschte LandID des Ziellandes, in dem die Aktion stattfinden soll (z.B. Überwachung von [Zielland], Plündern von [Zielland] usw.)</param>
        /// <param name="zielStuetzpunktID">Gewünschte StuetzpunktID des Zielstuetzpunktes, bei dem die Aktion stattfinden soll (z.B. Schicken von Truppen nach [Zielstuetzpunkt], Überfallen von [Zielstuetzpunkt] usw.)</param>
        /// <param name="stuetzpunktID">ID des Stützpunktes, zu dem diese Aktion gehört.</param>
        /// <param name="aktionIndexStuetzpunkt">Index (Nummer) der Aktion im Array des Stützpunktes (normalerweise 0 oder 1)</param>
        /// <param name="einheiten">OPTIONAL: Gewünschte Einheiten der Aktion</param>
        public RaeuberlagerAktion(EnumAktionsartRaeuberlager aktionsart, int zielLandID, int zielStuetzpunktID, int stuetzpunktID, int aktionIndexStuetzpunkt, List<Einheit> einheiten = null) :
                             base(zielLandID, zielStuetzpunktID, stuetzpunktID, aktionIndexStuetzpunkt, einheiten)
        {
            Aktionsart = aktionsart;
        }
        #endregion

        #region GetAktionText
        public string GetAktionText()
        {
            if (Aktionsart == EnumAktionsartRaeuberlager.Truppen_schicken)
                return "Schickt |{Truppen} Truppen an |{ZielStuetzpunkt}";
            else if (Aktionsart == EnumAktionsartRaeuberlager.Plündern)
                return "Plündert |{ZielLand} mit |{Truppen} Truppen";

            return "";
        }
        #endregion

        #region AktionAusfuehren
        public override string AktionAusfuehren(int StuetzpunktID)
        {
            string sResult = "";
            string sText = "";
            string sTextFehler = "";
            int iTruppenSumme = 0;
            int iAnzahlEinheit1 = 0;
            int iAnzahlEinheit2 = 0;
            int iAnzahlEinheit3 = 0;
            int iAnzahlEinheit4 = 0;
            Type TypeEinheit1 = typeof(RaubRaeuber);
            Type TypeEinheit2 = typeof(RaubBombenleger);
            Type TypeEinheit3 = typeof(RaubKanonier);
            Type TypeEinheit4 = typeof(RaubSchuetze);

            Einheit Truppeneinheit = null;

            if (Aktionsart == EnumAktionsartRaeuberlager.Kein_Auftrag)
                return null;

            iAnzahlEinheit1 = GetAnzahlTruppen(TypeEinheit1);
            iAnzahlEinheit2 = GetAnzahlTruppen(TypeEinheit2);
            iAnzahlEinheit3 = GetAnzahlTruppen(TypeEinheit3);
            iAnzahlEinheit4 = GetAnzahlTruppen(TypeEinheit4);

            if ((iAnzahlEinheit1 == 0) && (iAnzahlEinheit2 == 0) && (iAnzahlEinheit3 == 0) && (iAnzahlEinheit4 == 0))
                return null;

            switch (Aktionsart)
            {
                case EnumAktionsartRaeuberlager.Truppen_schicken:
                    {
                        if (iAnzahlEinheit1 > 0)
                        {
                            sResult = SW.Dynamisch.GetStuetzpunkte()[ZielStuetzpunktID - 1].ErhoeheTruppen(iAnzahlEinheit1, TypeEinheit1);

                            if (string.IsNullOrEmpty(sResult))
                            {
                                iTruppenSumme += iAnzahlEinheit1;
                                VerringereTruppen(iAnzahlEinheit1, TypeEinheit1);  // Truppen in der Aktion verringern
                                SW.Dynamisch.GetStuetzpunkte()[StuetzpunktID - 1].VerringereTruppen(iAnzahlEinheit1, TypeEinheit1);
                            }
                            else
                            {
                                Truppeneinheit = (Einheit)Activator.CreateInstance(TypeEinheit1);
                                sTextFehler += Truppeneinheit.NamePlural + ": " + sResult + " ";
                            }
                        }

                        if (iAnzahlEinheit2 > 0)
                        {
                            sResult = SW.Dynamisch.GetStuetzpunkte()[ZielStuetzpunktID - 1].ErhoeheTruppen(iAnzahlEinheit2, TypeEinheit2);

                            if (string.IsNullOrEmpty(sResult))
                            {
                                iTruppenSumme += iAnzahlEinheit2;
                                VerringereTruppen(iAnzahlEinheit2, TypeEinheit2);  // Truppen in der Aktion verringern
                                SW.Dynamisch.GetStuetzpunkte()[StuetzpunktID - 1].VerringereTruppen(iAnzahlEinheit2, TypeEinheit2);
                            }
                            else
                            {
                                Truppeneinheit = (Einheit)Activator.CreateInstance(TypeEinheit2);
                                sTextFehler += Truppeneinheit.NamePlural + ": " + sResult + " ";
                            }
                        }

                        if (iAnzahlEinheit3 > 0)
                        {
                            sResult = SW.Dynamisch.GetStuetzpunkte()[ZielStuetzpunktID - 1].ErhoeheTruppen(iAnzahlEinheit3, TypeEinheit3);

                            if (string.IsNullOrEmpty(sResult))
                            {
                                iTruppenSumme += iAnzahlEinheit3;
                                VerringereTruppen(iAnzahlEinheit3, TypeEinheit3);  // Truppen in der Aktion verringern
                                SW.Dynamisch.GetStuetzpunkte()[StuetzpunktID - 1].VerringereTruppen(iAnzahlEinheit3, TypeEinheit3);
                            }
                            else
                            {
                                Truppeneinheit = (Einheit)Activator.CreateInstance(TypeEinheit3);
                                sTextFehler += Truppeneinheit.NamePlural + ": " + sResult + " ";
                            }
                        }

                        if (iAnzahlEinheit4 > 0)
                        {
                            sResult = SW.Dynamisch.GetStuetzpunkte()[ZielStuetzpunktID - 1].ErhoeheTruppen(iAnzahlEinheit4, TypeEinheit4);

                            if (string.IsNullOrEmpty(sResult))
                            {
                                iTruppenSumme += iAnzahlEinheit4;
                                VerringereTruppen(iAnzahlEinheit4, TypeEinheit4);  // Truppen in der Aktion verringern
                                SW.Dynamisch.GetStuetzpunkte()[StuetzpunktID - 1].VerringereTruppen(iAnzahlEinheit4, TypeEinheit4);
                            }
                            else
                            {
                                Truppeneinheit = (Einheit)Activator.CreateInstance(TypeEinheit4);
                                sTextFehler += Truppeneinheit.NamePlural + ": " + sResult + " ";
                            }
                        }

                        if (iTruppenSumme > 0)
                        {
                            sText = SW.Dynamisch.GetSpWithID(SW.Dynamisch.GetStuetzpunkte()[StuetzpunktID - 1].Besitzer).GetKompletterName() + " ";
                            sText += GetAktionText().Replace("|{Truppen}", $" {iTruppenSumme} ").Replace("|{ZielStuetzpunkt}", $" {SW.Dynamisch.GetStuetzpunkte()[ZielStuetzpunktID - 1].Name}.").Replace("Schickt", "schickt");

                            if (sTextFehler != "")
                                sText += " " + sTextFehler;
                        }
                        else if (sTextFehler != "")
                        {
                            sText = SW.Dynamisch.GetSpWithID(SW.Dynamisch.GetStuetzpunkte()[StuetzpunktID - 1].Besitzer).GetKompletterName() + " ";
                            sText += GetAktionText().Replace("|{Truppen}", " keine ").Replace("|{ZielStuetzpunkt}", $" {SW.Dynamisch.GetStuetzpunkte()[ZielStuetzpunktID - 1].Name}.").Replace("Schickt", "schickt");
                            sText += " " + sTextFehler;
                        }

                        break;
                    }

                case EnumAktionsartRaeuberlager.Plündern:
                    {
                        sText = SW.Dynamisch.GetSpWithID(SW.Dynamisch.GetStuetzpunkte()[StuetzpunktID - 1].Besitzer).GetKompletterName() + " ";
                        sText += GetAktionText().Replace("|{ZielLand}", $" {SW.Dynamisch.GetLandWithID(ZielLandID).GetGebietsName()} ").Replace("|{Truppen}", $" {iAnzahlEinheit1 + iAnzahlEinheit2 + iAnzahlEinheit3 + iAnzahlEinheit4}.").Replace("Plündert", "plündert");

                        SW.Dynamisch.Landsicherheiten[ZielLandID - 1].AngriffsrisikoInProzent += 15;  // Eine erhöhte Angriffswahrscheinlichkeit der Karawanen für diese Region
                        break;
                    }
            }

            return sText;
        }
        #endregion

        #endregion
    }
}
