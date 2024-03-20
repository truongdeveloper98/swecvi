/* eslint-disable import/no-extraneous-dependencies */
import i18n from "i18next";
import i18nBackend from "i18next-http-backend";
import LanguageDetector from "i18next-browser-languagedetector";
import { initReactI18next } from "react-i18next";

i18n
  .use(i18nBackend)
  .use(LanguageDetector)
  .use(initReactI18next)
  .init({
    resources: {
      en: {
        translation: {
          PatientsFilter: "Patients Filter",
          Today: "Today",
          Yesterday: "Yesterday",
          TwoDayAgo: "2 Day Ago",
          OneWeekAgo: "One Week Ago",
          OneMonthAgo: "One Month Ago",
          ThisYear: "This Year",
          NumberOfExams: "Number of exams",
          ExamTypes: "Exam Types",
          ParametersStatic: "Parameters static",
          Parameter: "Parameter",
          Findings: "Findings",
          Search: "Search",
          UserID: "User ID",
          FirstName: "First Name",
          LastName: "Last Name",
          PhoneNumber: "Phone Number",
          Active: "Active",
          AddNew: "Add New",
          EntriesPerPage: "entries per page",
          NoRowsToShow: "No Rows To Show",
          User: "User",
          Cancel: "Cancel",
          SaveChanged: "Save Changed",
          EditUser: "Edit User",
          CreateUser: "Create User",
          Admin: "Admin",
          ForceSync: "Force Sync",
        },
      },
      sw: {
        translation: {
          PatientsFilter: "Patientfilter",
          Today: "I dag",
          Yesterday: "I går",
          TwoDayAgo: "2 dagar sedan",
          OneWeekAgo: "En Vecka Sedan",
          OneMonthAgo: "En månad sedan",
          ThisYear: "Det Här året",
          NumberOfExams: "Antal tentor",
          ExamTypes: "Examinationstyper",
          ParametersStatic: "Parametrar statiska",
          Parameter: "Parameter",
          Findings: "Fynd",
          Search: "Sök",
          UserID: "användar ID",
          FirstName: "Förnamn",
          LastName: "Efternamn",
          PhoneNumber: "Telefonnummer",
          Active: "Aktiva",
          AddNew: "Lägg till ny",
          EntriesPerPage: "Poster per sida",
          NoRowsToShow: "Inga rader att visa",
          User: "Användare",
          Cancel: "Annullera",
          SaveChanged: "Spara ändrad",
          EditUser: "Redigera användare",
          CreateUser: "Skapa användare",
          Admin: "Admin",
          ForceSync: "Tvinga fram synkronisering",
        },
      },
    },
    lng: "en", // default language
    fallbackLng: "en", // fallback language
    interpolation: {
      escapeValue: false, // not needed for React as it escapes by default
    },
    debug: true,
  });

export default i18n;
