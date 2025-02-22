﻿using System;
using System.Linq;
using MySql.Data.MySqlClient;
using QS.Dialog;
using QS.DomainModel.Entity;
using QS.ErrorReporting;
using QS.Project.DB;
using QS.Project.Domain;
using QS.Project.Versioning;
using QS.Services;
using Vodovoz.Tools;

namespace Vodovoz
{
	public static class ErrorHandlers
	{
		public static bool NHibernateStaleObjectStateExceptionHandler(Exception exception, IApplicationInfo application, UserBase user, IInteractiveMessage interactiveMessage)
		{
			var staleObjectStateException = ExceptionHelper.FindExceptionTypeInInner<NHibernate.StaleObjectStateException>(exception);
			if(staleObjectStateException != null) {
				var type = OrmConfig.FindMappingByFullClassName(staleObjectStateException.EntityName).MappedClass;
				var objectName = DomainHelper.GetSubjectNames(type);

				string message;

				switch(objectName?.Gender) {
					case GrammaticalGender.Feminine:
						message = "Сохраняемая <b>{0}</b> c номером <b>{1}</b> была кем то изменена.";
						break;
					case GrammaticalGender.Neuter:
						message = "Сохраняемое <b>{0}</b> c номером <b>{1}</b> было кем то изменено.";
						break;
					case GrammaticalGender.Masculine:
					default:
						message = "Сохраняемый <b>{0}</b> c номером <b>{1}</b> был кем то изменен.";
						break;
				}
				message = String.Format(message + "\nВаши изменения не будут записаны, чтобы не потерять чужие изменения. \nПереоткройте вкладку.", objectName?.Nominative ?? type.Name, staleObjectStateException.Identifier);

				interactiveMessage.ShowMessage(QS.Dialog.ImportanceLevel.Warning, message);
				return true;
			}
			return false;
		}

		public static bool MySqlExceptionConnectionTimeoutHandler(Exception exception, IApplicationInfo application, UserBase user, IInteractiveMessage interactiveMessage)
		{
			var mysqlEx = ExceptionHelper.FindExceptionTypeInInner<MySqlException>(exception);
			var exceptions = new[] { 1159, 1161 };
			if(mysqlEx != null && exceptions.Contains(mysqlEx.Number)) {
				interactiveMessage.ShowMessage(ImportanceLevel.Error, "Возникла проблема с подключением к серверу, попробуйте снова.");
				return true;
			}
			return false;
		}
		
		public static bool MySqlExceptionAuthHandler(Exception exception, IApplicationInfo application, UserBase user, IInteractiveService interactiveMessage)
		{
			var mysqlEx = ExceptionHelper.FindExceptionTypeInInner<MySqlException>(exception);
			
			if (mysqlEx != null && mysqlEx.Message.Contains("Authentication to host"))
			{
				interactiveMessage.ShowMessage(ImportanceLevel.Info, "Пароль вашего аккаунта был сброшен, для продолжения работы перезайдите в программу");
				return true;
			}
			return false;
		}

		public static bool SocketTimeoutException(Exception exception, IApplicationInfo application, UserBase user, IInteractiveService interactiveService)
        {
			var nhibernateEx = ExceptionHelper.FindExceptionTypeInInner<NHibernate.Exceptions.GenericADOException>(exception);
			var timeOutEx = ExceptionHelper.FindExceptionTypeInInner<System.Net.Sockets.SocketException>(exception);

			if (nhibernateEx != null && timeOutEx != null && timeOutEx.SocketErrorCode == System.Net.Sockets.SocketError.TimedOut)
            {
				interactiveService.ShowMessage(ImportanceLevel.Warning, "Программа не смогла обработать запрос вовремя, переоткройте вкладку");
				return true;
            }
			return false;
        }

		public static bool GeoGroupVersionNotFoundException(Exception exception, IApplicationInfo application, UserBase user, IInteractiveService interactiveService)
		{
			var geoGroupExceprion = ExceptionHelper.FindExceptionTypeInInner<GeoGroupVersionNotFoundException>(exception);

			if(geoGroupExceprion == null)
			{
				return false;
			}

			interactiveService.ShowMessage(ImportanceLevel.Warning, $"{geoGroupExceprion.Message}\nВыберите актуальную часть города или обратитесь в техподдержку для проверки актуальности данных указанной части города.");
			return true;
		}
	}
}
