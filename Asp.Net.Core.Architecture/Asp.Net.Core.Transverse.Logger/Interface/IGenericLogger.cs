using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Transverse.Logger.Interface
{
    /// <summary xml:lang="fr">
    /// Logger qui permet de créer des messages destructurés avec un contexte d'exécution spécifié par le 
    /// type générique fourni en paramètre.
    /// Voir la documentation de SeriLog pour plus d'informations sur le format des messages à adopter :
    /// https://github.com/serilog/serilog/wiki/Structured-Data
    /// </summary>
    public interface IGenericLogger<T> : IGenericLogger
    {

    }

    /// <summary xml:lang="fr">
    /// Logger qui permet de créer des messages destructurés.
    /// Voir la documentation de SeriLog pour plus d'informations sur le format des messages à adopter :
    /// https://github.com/serilog/serilog/wiki/Structured-Data
    /// </summary>
    public interface IGenericLogger
    {
        bool IsDebugEnabled();
        bool IsErrorEnabled();
        bool IsFatalEnabled();
        bool IsInformationEnabled();
        bool IsVerboseEnabled();
        bool IsWarningEnabled();


        void Debug(string messageTemplate, params object[] propertyValues);
        void Error(string messageTemplate, params object[] propertyValues);
        void Error(Exception exception, string messageTemplate, params object[] propertyValues);
        void Fatal(Exception exception, string messageTemplate, params object[] propertyValues);
        void Information(string messageTemplate, params object[] propertyValues);
        void Verbose(string messageTemplate, params object[] propertyValues);
        void Warning(string messageTemplate, params object[] propertyValues);
    }
}
