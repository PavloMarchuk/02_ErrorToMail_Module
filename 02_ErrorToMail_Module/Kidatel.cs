using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace _02_ErrorToMail_Module
{
	public class Kidatel : IHttpModule
	{
		public void Dispose() { }

		public void Init(HttpApplication context)
		{
			context.Error += Context_Error;
			context.EndRequest += Context_EndRequest;
		}

		private void Context_EndRequest(object sender, EventArgs e)
		{
			HttpApplication app = (HttpApplication)sender;
			app.Response.Write("$$$$$$$$$$$$$$$ тест Context_EndRequest$$$$$$$$$$$$$$$$");
		}

		private void Context_Error(object sender, EventArgs e)
		{
			//Logging(sender);

			var cfg = ConfigurationManager.AppSettings;
			MailMessage pismo0 = new MailMessage(
				"smert_okupantam@ukr.net",
				cfg["SMTPUser"],
				"DZ_From_PavloMArchuk",
				"Error" + " " + DateTime.Now.ToLongTimeString() + HttpContext.Current.Server.GetLastError().Message);









			//==========================
			//стоворення листа з чотирма string-параметрами
			MailMessage pismo1 = new MailMessage("smert_okupantam@ukr.net", "rommel3@ukr.net", "DZ_From_PavloMArchuk", "Error" + " " + DateTime.Now.ToLongTimeString() + HttpContext.Current.Server.GetLastError().Message)
			{
				// міняю отправітєля (получатєля змінити неможливо) 
				//From = new MailAddress("rommel3@ukr.net", "PavloMarchuk")
			};

			//тсворення смтпКлієнта з сервером в портом ukr.net
			SmtpClient smtpClient = new SmtpClient("smtp.ukr.net", 465)
			{
				// Add credentials if the SMTP server requires them.			
				Host = "mail.ukr.net",
				Credentials = new System.Net.NetworkCredential("rommel3@ukr.net", "170882")
			};

			try
			{
				smtpClient.Send(pismo1);
			}
			catch
			{
				// файл на диску F
				string path = @"F:\ERRORRRRdata.txt";
				string massaga ="помилка відправки листа";
				using
				(StreamWriter outputFile = new StreamWriter(path, true))
				
				{
					outputFile.WriteLine("Error" + " " + DateTime.Now.ToLongTimeString() + " " + massaga);
				}
			}
		}

		private void Logging(object sender)
		{
			//як записати у папку "мої документи" (далі не використовую)
			//string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

			// достаємо меcагу помилки (варіант 1)
			string str_variant_teacher = HttpContext.Current.Server.GetLastError().Message;
			HttpApplication app = (HttpApplication)sender;
			// достаємо меcагу помилки (варіант 2)
			//string variant_2 = ((HttpApplication)sender).Context.Error.Message;

			// файл на диску F
			string path = @"F:\ERRORRRRdata.txt";

			using
				(StreamWriter outputFile = new StreamWriter(path, true))
			//(StreamWriter outputFile = new StreamWriter(mydocpath + @"\ERRORRRRdata.txt", true)) //для запису в папку мої документи
			{
				outputFile.WriteLine("Error" + " " + DateTime.Now.ToLongTimeString() + " " + str_variant_teacher);
			}
		}
	}
}