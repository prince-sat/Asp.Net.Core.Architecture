using Asp.Net.Core.Transverse.Logger.Interface;
using log4net;
using log4net.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Asp.Net.Core.Transverse.Logger
{
    /// <summary xml:lang="fr">
    /// Logger se basant sur log4net
    /// </summary>
    public class Log4NetLogger<T> : IGenericLogger<T>
    {
        private string _callerName;
        private ILog _logger;

        public Log4NetLogger()
        {
            _logger = log4net.LogManager.GetLogger(typeof(string));

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
        private void Write(Level level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            if (String.IsNullOrWhiteSpace(messageTemplate))
            {
                throw new ArgumentException("the template message for log4net cannot be empty or null", nameof(messageTemplate));
            }
            if (propertyValues != null && propertyValues.Any())
            {
                List<object> properties = new List<object>();
                foreach (var propertyValue in propertyValues)
                {
                    if (propertyValue.GetType().GetTypeInfo().IsPrimitive)
                        properties.Add(propertyValue);
                    else
                        properties.Add(PropertyValueToString(propertyValue));
                }
                messageTemplate = string.Format(messageTemplate, properties);
            }

            Type tCaller = typeof(T);
            if (tCaller == typeof(object))
                _logger.Logger.Log(Assembly.GetEntryAssembly().GetType(), level, messageTemplate, exception);
            else
                _logger.Logger.Log(tCaller, level, messageTemplate, exception);
        }

        private string PropertyValueToString(object propertyValue)
        {
            StringBuilder sb = new StringBuilder();
            if (propertyValue != null)
            {
                sb.Append("Objet : ");
                try
                {
                    sb.Append(JsonConvert.SerializeObject(propertyValue));
                }
                catch (Exception e)
                {
                    sb.Append("Objet non sérializé");
                    Error(e, "Exception rencontrée lors de la serialization d'un objet");
                }
            }
            return sb.ToString();
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
            Write(Level.Verbose, null, messageTemplate, propertyValues);
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
            Write(Level.Debug, null, messageTemplate, propertyValues);
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
            Write(Level.Info, null, messageTemplate, propertyValues);
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
            Write(Level.Warn, null, messageTemplate, propertyValues);
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
            Write(Level.Error, null, messageTemplate, propertyValues);
        }

        /// <summary xml:lang="fr">
        /// Log un message d'erreur
        /// </summary>
        /// <param xml:lang="fr" name="messageTemplate">Template de message</param>
        /// <param xml:lang="fr" name="propertyValues">objets à logger</param>
        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Write(Level.Error, exception, messageTemplate, propertyValues);
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
            Write(Level.Fatal, exception, messageTemplate, propertyValues);
        }

        #endregion

        public bool IsDebugEnabled()
        {
            return _logger.Logger.IsEnabledFor(Level.Debug);
        }

        public bool IsErrorEnabled()
        {
            return _logger.Logger.IsEnabledFor(Level.Error);
        }

        public bool IsFatalEnabled()
        {
            return _logger.Logger.IsEnabledFor(Level.Fatal);
        }

        public bool IsInformationEnabled()
        {
            return _logger.Logger.IsEnabledFor(Level.Info);
        }

        public bool IsVerboseEnabled()
        {
            return _logger.Logger.IsEnabledFor(Level.Verbose);
        }

        public bool IsWarningEnabled()
        {
            return _logger.Logger.IsEnabledFor(Level.Warn);
        }
    }
}
