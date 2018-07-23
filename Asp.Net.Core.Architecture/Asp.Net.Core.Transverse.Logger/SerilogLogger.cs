using Asp.Net.Core.Transverse.Logger.Interface;
using Serilog;
using Serilog.Events;
using System;


namespace Asp.Net.Core.Transverse.Logger
{
    /// <summary xml:lang="fr">
    /// Logger se basant sur SeriLog
    /// </summary>
    public class SerilogLogger<T> :  IGenericLogger<T>
    {
        private string _callerName;

        public SerilogLogger()
        {


            if (typeof(T) == typeof(object))
                _callerName = String.Empty;
            else
                _callerName = "[" + typeof(T).FullName + "] ";
        }

        #region Write

        /// <summary xml:lang="fr">
        /// Méthode principale d'écriture de log
        /// </summary>
        /// <param xml:lang="fr" name="level">Niveau de log</param>
        /// <param xml:lang="fr" name="exception">Exception</param>
        /// <param xml:lang="fr" name="messageTemplate">Template de message</param>
        /// <param xml:lang="fr" name="propertyValues">objets à logger</param>
        private void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            if (String.IsNullOrWhiteSpace(messageTemplate))
            {
                throw new ArgumentException("the template message for Serilog cannot be empty or null", nameof(messageTemplate));
            }
            Log.Logger
                .ForContext("Caller", _callerName)
                .Write(level, exception, messageTemplate, propertyValues);
        }


        #endregion

        #region Verbose

        /// <summary xml:lang="fr">
        /// Log un message en mode Verbose
        /// </summary>
        /// <param xml:lang="fr" name="messageTemplate">Template de message</param>
        /// <param xml:lang="fr" name="propertyValues">objets à logger</param>
        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Verbose, null, messageTemplate, propertyValues);
        }


        #endregion

        #region Debug

        /// <summary xml:lang="fr">
        /// Log un message de debug
        /// </summary>
        /// <param xml:lang="fr" name="messageTemplate">Template de message</param>
        /// <param xml:lang="fr" name="propertyValues">objets à logger</param>
        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Debug, null, messageTemplate, propertyValues);
        }

        #endregion

        #region Information

        /// <summary xml:lang="fr">
        /// Log un message d'information
        /// </summary>
        /// <param xml:lang="fr" name="messageTemplate">Template de message</param>
        /// <param xml:lang="fr" name="propertyValues">objets à logger</param>
        public void Information(string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Information, null, messageTemplate, propertyValues);
        }

        #endregion

        #region Warning

        /// <summary xml:lang="fr">
        /// Log un message de warning
        /// </summary>
        /// <param xml:lang="fr" name="messageTemplate">Template de message</param>
        /// <param xml:lang="fr" name="propertyValues">objets à logger</param>
        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Warning, null, messageTemplate, propertyValues);
        }

        #endregion

        #region Error

        /// <summary xml:lang="fr">
        /// Log un message d'erreur
        /// </summary>
        /// <param xml:lang="fr" name="messageTemplate">Template de message</param>
        /// <param xml:lang="fr" name="propertyValues">objets à logger</param>
        public void Error(string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Error, null, messageTemplate, propertyValues);
        }

        /// <summary xml:lang="fr">
        /// Log un message d'erreur
        /// </summary>
        /// <param xml:lang="fr" name="messageTemplate">Template de message</param>
        /// <param xml:lang="fr" name="propertyValues">objets à logger</param>
        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Error, exception, messageTemplate, propertyValues);
        }

        #endregion

        #region Fatal

        /// <summary xml:lang="fr">
        /// Log un message d'erreur fatal
        /// </summary>
        /// <param xml:lang="fr" name="exception">Exception à logger</param>
        /// <param xml:lang="fr" name="messageTemplate">Template de message</param>
        /// <param xml:lang="fr" name="propertyValues">objets à logger</param>
        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValues);
        }

        #endregion

        public bool IsDebugEnabled()
        {
            return Log.Logger.IsEnabled(LogEventLevel.Debug);
        }

        public bool IsErrorEnabled()
        {
            return Log.Logger.IsEnabled(LogEventLevel.Error);
        }

        public bool IsFatalEnabled()
        {
            return Log.Logger.IsEnabled(LogEventLevel.Fatal);
        }

        public bool IsInformationEnabled()
        {
            return Log.Logger.IsEnabled(LogEventLevel.Information);
        }

        public bool IsVerboseEnabled()
        {
            return Log.Logger.IsEnabled(LogEventLevel.Verbose);
        }

        public bool IsWarningEnabled()
        {
            return Log.Logger.IsEnabled(LogEventLevel.Warning);
        }
    }
}
